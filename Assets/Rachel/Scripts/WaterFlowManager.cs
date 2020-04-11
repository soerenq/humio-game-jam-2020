using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFlowManager : MonoBehaviour
{
    public GameObject BackgroundTile;
    public GameObject HorizontalPipe;
    public GameObject RightDownPipe;
    public GameObject RightUpPipe;
    public GameObject LeftUpPipe;
    public GameObject LeftDownPipe;
    public GameObject VerticalPipe;

    private List<Pipe> pipeList;

    public List<GameObject> pipeBarContent;

    public GameObject SelectedPipe = null;

    private bool testFilled = false;

    private int gridHeight = 5;
    private int gridWidth = 8;
    // Initialize our scene and other scripts and objects from here
    private void Awake() {
        pipeList = new List<Pipe>();
        generateBackground();
        Camera.main.transform.position = new Vector3((gridWidth / 2) - 0.5f, (gridHeight / 2) - 1.5f, -10);
    }

    private void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (!testFilled) {
                testFilled = true;
                StartCoroutine(testFlow());
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hitData = Physics2D.Raycast(new Vector2(worldPosition.x, worldPosition.y), Vector2.zero, 0);
            if (hitData && hitData.collider.GetComponent<BgTile>() != null) {
                if (hitData.collider.GetComponent<BgTile>().IsPipeBar) {
                    SelectedPipe = pipeBarContent[hitData.collider.GetComponent<BgTile>().pipeBarID];
                }
                else {
                    if (SelectedPipe != null)
                    Instantiate(SelectedPipe, hitData.transform.position, Quaternion.identity);
                    SelectedPipe = null;
                }
            }
            
        }
    }

    private void generateBackground() {
        
        for (int x = 0; x < gridWidth; x++) {
            for (int y = 0; y < gridHeight; y++) {
                Instantiate(BackgroundTile, new Vector3(x, y, 0), Quaternion.identity);
            }
        }
        generatePipeBar();
    }

    private void generatePipeBar() {
        for (int i = 0; i < 6; i++) {
            BgTile temp =  Instantiate(BackgroundTile, new Vector3(i + 0.8f + (i * 0.1f), -1.5f,0), Quaternion.identity).GetComponent<BgTile>();
            temp.IsPipeBar = true;
            temp.pipeBarID = i;
            Instantiate(pipeBarContent[i], new Vector3(i + 0.8f + (i * 0.1f), -1.5f,0), Quaternion.identity);
        }
    }

    private void PlacePipes() {
    /*  
        pipeList.Add(Instantiate(HorizontalPipe, new Vector3(0,1,0), Quaternion.identity).GetComponent<Pipe>());
        pipeList.Add(Instantiate(RightUpPipe, new Vector3(1,1,0), Quaternion.identity).GetComponent<Pipe>());
        pipeList.Add(Instantiate(RightDownPipe, new Vector3(1,2,0), Quaternion.identity).GetComponent<Pipe>());
        pipeList.Add(Instantiate(LeftDownPipe, new Vector3(0,2,0), Quaternion.identity).GetComponent<Pipe>());
        pipeList.Add(Instantiate(VerticalPipe, new Vector3(0,3,0), Quaternion.identity).GetComponent<Pipe>());
        pipeList.Add(Instantiate(LeftUpPipe, new Vector3(0,4,0), Quaternion.identity).GetComponent<Pipe>());
    */
    }

    private void ToggleWater(bool filled) {
        foreach (Pipe pipe in pipeList) {
            pipe.ToggleSprite(filled);
        }
        testFilled = false;
    }

    private IEnumerator testFlow() {
        for (int i = 0; i < pipeList.Count; i ++) {
            yield return new WaitForSeconds(0.25f);
            pipeList[i].ToggleSprite(true);
        }
        yield return new WaitForSeconds(0.5f);
        ToggleWater(false);
    }   
}
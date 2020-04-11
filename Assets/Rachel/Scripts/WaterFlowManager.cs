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

    private bool testFilled = false;

    private int gridHeight = 5;
    private int gridWidth = 8;
    // Initialize our scene and other scripts and objects from here
    private void Awake() {
        pipeList = new List<Pipe>();
        generateBackground();
    }

    private void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (!testFilled) {
                testFilled = true;
                StartCoroutine(testFlow());
            }
        }
    }

    private void generateBackground() {
        
        for (int x = 0; x < gridWidth; x++) {
            for (int y = 0; y < gridHeight; y++) {
                Instantiate(BackgroundTile, new Vector3(x, y, 0), Quaternion.identity);
            }
        }
        PlacePipes();
    }

    private void PlacePipes() {
        pipeList.Add(Instantiate(HorizontalPipe, new Vector3(0,1,0), Quaternion.identity).GetComponent<Pipe>());
        pipeList.Add(Instantiate(RightUpPipe, new Vector3(1,1,0), Quaternion.identity).GetComponent<Pipe>());
        pipeList.Add(Instantiate(RightDownPipe, new Vector3(1,2,0), Quaternion.identity).GetComponent<Pipe>());
        pipeList.Add(Instantiate(LeftDownPipe, new Vector3(0,2,0), Quaternion.identity).GetComponent<Pipe>());
        pipeList.Add(Instantiate(VerticalPipe, new Vector3(0,3,0), Quaternion.identity).GetComponent<Pipe>());
        pipeList.Add(Instantiate(LeftUpPipe, new Vector3(0,4,0), Quaternion.identity).GetComponent<Pipe>());
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
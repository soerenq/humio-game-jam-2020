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

    public GameObject endPipe;

    public List<GameObject> pipeGrid;

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
       /*if (Input.GetKeyDown(KeyCode.Space)) {
            if (!testFilled) {
                testFilled = true;
                StartCoroutine(testFlow());
            }
        }*/

        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hitData = Physics2D.Raycast(new Vector2(worldPosition.x, worldPosition.y), Vector2.zero, 0);
          
            handleClick(worldPosition, hitData);    
        }
    }

    private void handleClick(Vector3 worldPos, RaycastHit2D hitData){
        bool isBackgroundTile = hitData && hitData.collider.GetComponent<BgTile>() != null;
        if (isBackgroundTile) {
                        // player clicks on bar to select a pipe
                        if (hitData.collider.GetComponent<BgTile>().IsPipeBar) {
                            SelectedPipe = pipeBarContent[hitData.collider.GetComponent<BgTile>().pipeBarID];
                        }
                        // player has clicked on the grid
                        else {
                            // the player has a selected pipe that he wants to place
                            if (SelectedPipe != null) {         
                                if (isPipePlacementAllowed(SelectedPipe, hitData.collider.gameObject)){
                                    if (isPipeNextToLatestPipe(hitData.collider.gameObject)) {
                                        // placing pipe
                                        GameObject TempGo = Instantiate(SelectedPipe, hitData.transform.position, Quaternion.identity);
                                        // registering pipe on tile
                                        hitData.collider.gameObject.GetComponent<BgTile>().pipeOnBg = TempGo;
                                        // adding to pipelist for waterflow
                                        pipeList.Add(TempGo.GetComponent<Pipe>());
                                        // the player now has to select a new pipe
                                        SelectedPipe = null;
                                        
                                        // check if game won
                                        foreach (GameObject Go in pipeGrid)
                                        {
                                            BgTile bgt = Go.GetComponent<BgTile>();
                                            if (bgt.transform.position.x == endPipe.transform.position.x - 1 && bgt.transform.position.y == endPipe.transform.position.y) {
                                                if (bgt.pipeOnBg != null) {
                                                    if (bgt.pipeOnBg.GetComponent<Pipe>().pipeType == Pipe.PipeType.HorizontalPipe || bgt.pipeOnBg.GetComponent<Pipe>().pipeType == Pipe.PipeType.LeftUpPipe || bgt.pipeOnBg.GetComponent<Pipe>().pipeType == Pipe.PipeType.LeftDownPipe)
                                                    GameWon();
                                                }   
                                            }
                                        }   
                                    }                                 
                                }    
                            }                    
                        }
                    }
    }

    private bool isPipeNextToLatestPipe(GameObject clickedTile) {
        GameObject LastTile = pipeList[pipeList.Count - 1].gameObject;
       /* if (LastTile.transform.position.x - clickedTile.transform.position.x < 1 || LastTile.transform.position.x - clickedTile.transform.position.x > 1) {
            Debug.Log("1: " + (LastTile.transform.position.x - clickedTile.transform.position.x));
            return false;
        }
        else if (LastTile.transform.position.y - clickedTile.transform.position.y < 1 || LastTile.transform.position.y - clickedTile.transform.position.y > 1) {
            Debug.Log("2");
            return false;
        }*/
        if (LastTile.transform.position.x - clickedTile.transform.position.x == -1 || LastTile.transform.position.x - clickedTile.transform.position.x == 1 || LastTile.transform.position.x - clickedTile.transform.position.x == 0){
            if (LastTile.transform.position.y - clickedTile.transform.position.y == -1 || LastTile.transform.position.y - clickedTile.transform.position.y == 1 || LastTile.transform.position.y - clickedTile.transform.position.y == 0) {
                return true;
            }
        }
        return false;

    }

    private void GameWon() {
        // add end pipe to the pipelist to enable waterflow
        pipeList.Add(endPipe.GetComponent<Pipe>());
        // make water run
        StartCoroutine(testFlow());
        // call the reward thing here:

    }

    private bool isPipePlacementAllowed(GameObject SelectedPipe, GameObject clickedTile){
       switch (SelectedPipe.GetComponent<Pipe>().pipeType){
            case Pipe.PipeType.LeftDownPipe:
                return (getElementInPipeGrid(clickedTile, 1, 0) || getElementInPipeGrid(clickedTile, 0, 1)) && !doesGridHoldAPipe(clickedTile.transform.position.x, clickedTile.transform.position.y);   
                
            case Pipe.PipeType.RightDownPipe:
                return (getElementInPipeGrid(clickedTile, -1, 0) || getElementInPipeGrid(clickedTile, 0, -1)) && !doesGridHoldAPipe(clickedTile.transform.position.x, clickedTile.transform.position.y);

            case Pipe.PipeType.LeftUpPipe:
                return (getElementInPipeGrid(clickedTile, 1, 0) || getElementInPipeGrid(clickedTile, 0, -1)) && !doesGridHoldAPipe(clickedTile.transform.position.x, clickedTile.transform.position.y);

            case Pipe.PipeType.RightUpPipe:
                return (getElementInPipeGrid(clickedTile, -1, 0) || getElementInPipeGrid(clickedTile, 0, 1)) && !doesGridHoldAPipe(clickedTile.transform.position.x, clickedTile.transform.position.y);

            case Pipe.PipeType.VerticalPipe:
                return (getElementInPipeGrid(clickedTile, 0, -1) || getElementInPipeGrid(clickedTile, 0, 1)) && !doesGridHoldAPipe(clickedTile.transform.position.x, clickedTile.transform.position.y);

            case Pipe.PipeType.HorizontalPipe:
                return (getElementInPipeGrid(clickedTile, -1, 0) || getElementInPipeGrid(clickedTile, 1, 0)) && !doesGridHoldAPipe(clickedTile.transform.position.x, clickedTile.transform.position.y);

            default:
                return false;     

       }   
   }

   private bool doesGridHoldAPipe(float x, float y){
       foreach (GameObject Go in pipeGrid)
       {
           BgTile bgt = Go.GetComponent<BgTile>();
            if (bgt.transform.position.x == x && bgt.transform.position.y == y) {
                return bgt.pipeOnBg != null;
            }
       }
       return false;
   }

    public bool getElementInPipeGrid(GameObject clickedTile, int x, int y){
        foreach (GameObject bg in pipeGrid)
        {            
            if (bg.transform.position.x == (clickedTile.transform.position.x + x) && bg.transform.position.y == (clickedTile.transform.position.y + y)){     
                if(bg.GetComponent<BgTile>().pipeOnBg != null){
                    var pipe = bg.GetComponent<BgTile>().pipeOnBg;
                    
                    return isPipeConnectionLegal(pipe.GetComponent<Pipe>().pipeType, x, y); 
                                          
                }

            }
        }
        return false;
    }  

    public bool isPipeConnectionLegal(Pipe.PipeType pt, int x, int y){
        switch(pt){
             case Pipe.PipeType.LeftDownPipe:
                return x == -1 || y == -1;
            case Pipe.PipeType.RightDownPipe:
                return x == 1 || y == 1;
            case Pipe.PipeType.LeftUpPipe:
                 return x == -1 || y == 1;
            case Pipe.PipeType.RightUpPipe:
                 return x == 1 || y == -1;
            case Pipe.PipeType.VerticalPipe:
               return y == 1 || y == -1;
            case Pipe.PipeType.HorizontalPipe:
                 return x == 1 || x == -1;
            default:
                return false;     
        }
    }

    public bool isPipeConnectionEnd(float xToTest, float yToTest){
        return endPipe.transform.position.x == xToTest && endPipe.transform.position.y == yToTest; 
    }


    private void generateBackground() {
        
        for (int x = 0; x < gridWidth; x++) {
            for (int y = 0; y < gridHeight; y++) {
               pipeGrid.Add(Instantiate(BackgroundTile, new Vector3(x, y, 0), Quaternion.identity));
            }
        }
        generatePipeBar();
        PlaceStartAndEndPipe();
    }




    private void generatePipeBar() {
        for (int i = 0; i < 6; i++) {
            BgTile temp =  Instantiate(BackgroundTile, new Vector3(i + 0.8f + (i * 0.1f), -1.5f,0), Quaternion.identity).GetComponent<BgTile>();
            temp.IsPipeBar = true;
            temp.pipeBarID = i;
            Instantiate(pipeBarContent[i], new Vector3(i + 0.8f + (i * 0.1f), -1.5f,0), Quaternion.identity);
        }
    }

    private void PlaceStartAndEndPipe() {
        foreach (GameObject Go in pipeGrid)
        {
            BgTile bgt = Go.GetComponent<BgTile>();

            //Start pipe
            if (bgt.transform.position.x == 0 && bgt.transform.position.y == 2) {
                bgt.pipeOnBg = Instantiate(HorizontalPipe, new Vector3(0,2,0), Quaternion.identity);
                // add to pipelist to enable waterflow
                pipeList.Add(bgt.pipeOnBg.GetComponent<Pipe>());
            }

            //End pipe
            if (bgt.transform.position.x == gridWidth-1 && bgt.transform.position.y == 3) {
                endPipe = Instantiate(HorizontalPipe, new Vector3(gridWidth-1,3,0), Quaternion.identity);
                bgt.pipeOnBg = endPipe;
            }
        }
        
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
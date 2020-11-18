using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;
using System;

public class RTSController : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 cameraStartPosition;

    public  List<GameObject> selectedPlayerObjects;
    private List<GameObject> positionObjects;

    public RectTransform selectingBox;

    public GameObject RightClickPrefab;

    private KeyCode[] keyCodes = {
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8,
         KeyCode.Alpha9,
     };

    private int numberPressed = 0;

    public List<List<GameObject>> ControlGroups;

    public GameObject UIUnitPanel;

    public GameObject UIActionPanel;

    private Command command = null;

    public GameObject ControlGroupPanelObject;

    public bool buttonPressed = false;

    private GameObject rightClickTarget = null;

    private UIResource UIResource;

    public GameObject MenuObject;

    private bool gameOver;
    private float gameOverTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        selectedPlayerObjects = new List<GameObject>();
        //selectedPlayerObjects.Add(new GameObject());

        positionObjects = new List<GameObject>();
        selectingBox.gameObject.SetActive(false);

        ControlGroups = new List<List<GameObject>>();
        for(int i = 0; i < keyCodes.Length; i++)
        {
            ControlGroups.Add(new List<GameObject>());
        }

        UIResource = GameObject.Find("Resource").GetComponent<UIResource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver && gameOverTimer < Time.time) {
            MenuObject.GetComponent<Menu>().DisplayGameOverMenu();
        }

        checkNumbers();

        //Left Mouse Down
        if (Input.GetMouseButtonDown(0))
        {
            LeftMouseDown();
        }

        //Left Mouse Hold
        if (Input.GetMouseButton(0))
        {
            LeftMouseHold();
        }

        //Left Mouse Up
        if (Input.GetMouseButtonUp(0))
        {
            LeftMouseUp();
        }

        //Right Click
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if(hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Enemy" || hit.collider.gameObject.tag == "EnemyBuilding")
                {
                    command = new Attack();
                    (command as Attack).SetTarget(hit.collider.gameObject);
                }
            }
            
            if(selectedPlayerObjects.Count != 0)
            {
                if(selectedPlayerObjects[0].GetComponent<PlayerUnit>() != null || selectedPlayerObjects[0].GetComponent<PlayerBarracks>() != null)
                {
                    RightClickCommand(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                }
            }
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            CreateControlGroup();
        }

        if (numberPressed > -1)
        {
            SelectControlGroup();
        }

        //AttackMove
        if (Input.GetKeyDown(KeyCode.A) && selectedPlayerObjects.Count > 0)
        {
            if (selectedPlayerObjects[0].GetComponent<PlayerUnit>() != null)
                command = new AttackMove();
        }

        //Move
        if (Input.GetKeyDown(KeyCode.M) && selectedPlayerObjects.Count > 0)
        {
            if (selectedPlayerObjects[0].GetComponent<PlayerUnit>() != null)
                command = new Move();
        }


        //Stop
        if (Input.GetKeyDown(KeyCode.S) && selectedPlayerObjects.Count > 0)
        {
            if (selectedPlayerObjects[0].GetComponent<PlayerUnit>() != null)
            {
                command = new Stop();
                ExecuteCommand(null);
            }   
        }

        //Make PlayerUnit
        if (Input.GetKeyDown(KeyCode.P) && selectedPlayerObjects.Count > 0)
        {
            if (selectedPlayerObjects[0].GetComponent<PlayerBarracks>() != null)
            {
                selectedPlayerObjects[0].GetComponent<PlayerBarracks>().Produce();
            }
        }

        //Set Rally point
        if (Input.GetKeyDown(KeyCode.R) && selectedPlayerObjects.Count > 0)
        {
            if (selectedPlayerObjects[0].GetComponent<PlayerBarracks>() != null)
            {
                SetRallyCommand();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuObject.GetComponent<Menu>().Pause();
        }
    }
    
    //Move command for now
    void RightClickCommand(Vector3 mousePosition)
    {
        Vector3 modifiedPos = mousePosition;
        modifiedPos.z = 0;
        GameObject obj = Instantiate(RightClickPrefab, modifiedPos, Quaternion.identity);
        
        if(command == null || command is Move)
        {
            command = new Move();
            (command as Move).SetMousePosition(mousePosition);
        }
        ExecuteCommand(obj);
    }

    void LeftMouseDown()
    {
        selectingBox.gameObject.SetActive(true);
        startPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cameraStartPosition = Input.mousePosition;
    }

    void LeftMouseHold()
    {
        Vector3 currentMousePosition = Input.mousePosition;

        float width = currentMousePosition.x - cameraStartPosition.x;
        float height = currentMousePosition.y - cameraStartPosition.y;

        selectingBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        selectingBox.anchoredPosition = cameraStartPosition + new Vector3(width / 2, height / 2);
    }

    void LeftMouseUp()
    {
        if (buttonPressed)
        {
            buttonPressed = false;
            return;
        }
        UIUnitPanel.GetComponent<UnitPanel>().ClearUnitPanel();

        selectingBox.gameObject.SetActive(false);

        Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(startPosition, Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if(selectedPlayerObjects.Count != 0)
        {
            if (selectedPlayerObjects[0].GetComponent<PlayerUnit>() != null)
            {
                SelectPlayerUnits(false);
            }
            if(selectedPlayerObjects[0].GetComponent<PlayerBarracks>() != null)
            {
                SelectPlayerBuilding(false);
            }
        }
        
        selectedPlayerObjects.Clear();
        UIActionPanel.GetComponent<UIActionPanelController>().DisableActionPanel();

        //Check if the selected gameObjects is a PlayerUnit
        foreach (Collider2D collider2D in collider2DArray)
        {
            if (collider2D.tag == "PlayerUnit")
            {
                selectedPlayerObjects.Add(collider2D.gameObject);
            }
        }
        //Show that the unit is selected
        if(selectedPlayerObjects.Count != 0)
        {
            SelectPlayerUnits(true);
            UIActionPanel.GetComponent<UIActionPanelController>().EnablePlayerUnitPanel();
            return;
        }

        //Check if selected gameObjects is a PlayerBuilding
        foreach(Collider2D collider2D in collider2DArray)
        {
            if (collider2D.gameObject.tag == "PlayerBuilding")
            {
                selectedPlayerObjects.Clear();
                selectedPlayerObjects.Add(collider2D.gameObject);
            }
        }
        //Show building is selected
        if(selectedPlayerObjects.Count != 0)
        {
            SelectPlayerBuilding(true);
            UIActionPanel.GetComponent<UIActionPanelController>().EnablePlayerBarracksPanel();
        }
        //Check if selected gameObjects is a EnemyUnit

        //Check if selected gameObject is an EnemyBuilding
    }

    void checkNumbers()
    {
        numberPressed = -1;
        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                numberPressed = i + 1;
            }
        }
    }

    void SelectPlayerUnits(bool select)
    {
        foreach (GameObject playerUnit in selectedPlayerObjects)
        {
            playerUnit.GetComponent<PlayerUnit>().SetSelected(select);
        }
        if(select)
            UIUnitPanel.GetComponent<UnitPanel>().SelectPlayerUnits(selectedPlayerObjects);
    }

    void SelectPlayerBuilding(bool select)
    {
        selectedPlayerObjects[0].GetComponent<PlayerBarracks>().SetSelected(select);
        if(select)
            UIUnitPanel.GetComponent<UnitPanel>().SelectPlayerBuilding();
    }

    void CreateControlGroup()
    {
        if (numberPressed > -1 && selectedPlayerObjects.Count != 0)
        {
            ControlGroups[numberPressed].Clear();
            ControlGroups[numberPressed].AddRange(selectedPlayerObjects);

            bool unit;
            if(selectedPlayerObjects[0].GetComponent<PlayerUnit>() != null)
            {
                unit = true;
            } else
            {
                unit = false;
            }
            ControlGroupPanelObject.GetComponent<UIControlGroupPanel>().CreateControlGroup(numberPressed, unit);
        }
    }

    void SelectControlGroup()
    {
        if(ControlGroups[numberPressed].Count != 0)
        {
            if(selectedPlayerObjects.Count != 0)
            {
                if (selectedPlayerObjects[0].GetComponent<PlayerUnit>() != null)
                {
                    SelectPlayerUnits(false);
                }
                if (selectedPlayerObjects[0].GetComponent<PlayerBarracks>() != null)
                {
                    SelectPlayerBuilding(false);
                }
            }
            
            selectedPlayerObjects.Clear();
            selectedPlayerObjects.AddRange(ControlGroups[numberPressed]);
            if (selectedPlayerObjects.Count > 0)
            {
                if (selectedPlayerObjects[0].GetComponent<PlayerUnit>() != null)
                {
                    SelectPlayerUnits(true);
                    UIActionPanel.GetComponent<UIActionPanelController>().EnablePlayerUnitPanel();
                } else
                {
                    SelectPlayerBuilding(true);
                    UIActionPanel.GetComponent<UIActionPanelController>().EnablePlayerBarracksPanel();
                }
            }
            ControlGroupPanelObject.GetComponent<UIControlGroupPanel>().SelectGroup(numberPressed);
        }
    }

    public void SetCommand(Command newCommand)
    {
        command = newCommand;
    }

    public void ExecuteCommand(GameObject rightClickObject)
    {
        command.execute(selectedPlayerObjects, positionObjects, rightClickObject);
        command = null;
    }

    public void SetRallyCommand()
    {
        command = new Rally();
        (command as Rally).rally = selectedPlayerObjects[0].GetComponent<PlayerBarracks>().rally;
    }

    public void NotifyDeath(string name)
    {
        int index = selectedPlayerObjects.FindIndex(obj => obj.name == name);
        if(index != -1)
        {
            selectedPlayerObjects.RemoveAt(index);
            UIUnitPanel.GetComponent<UnitPanel>().SelectPlayerUnits(selectedPlayerObjects);
        }
    }

    public void Win()
    {
        gameOver = true;
        gameOverTimer = Time.time + 1;

        MenuObject.GetComponent<Menu>().SetGameOverText("You Win!");
    }

    public void Lose()
    {
        gameOver = true;
        gameOverTimer = Time.time + 1;

        MenuObject.GetComponent<Menu>().SetGameOverText("You Lose!");
    }
}

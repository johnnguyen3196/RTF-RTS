﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

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

    private Command command = null;

    public GameObject ControlGroupPanelObject;

    // Start is called before the first frame update
    void Start()
    {
        selectedPlayerObjects = new List<GameObject>();
        //selectedPlayerObjects.Add(new GameObject());

        positionObjects = new List<GameObject>();
        selectingBox.gameObject.SetActive(false);

        ControlGroups = new List<List<GameObject>>();
        for(int i = 0; i < keyCodes.Length; i++)
        {
            ControlGroups.Add(new List<GameObject>());
        }
    }

    // Update is called once per frame
    void Update()
    {
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
            RightClickCommand(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            CreateControlGroup();
        }

        if (numberPressed > -1)
        {
            SelectControlGroup();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            command = new AttackMove();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            command = new Move();
        }
    }
    
    //Move command for now
    void RightClickCommand(Vector3 mousePosition)
    {
        Vector3 modifiedPos = mousePosition;
        modifiedPos.z = 0;
        GameObject obj = Instantiate(RightClickPrefab, modifiedPos, Quaternion.identity);
        
        if(command == null)
        {
            command = new Move();
        }
        command.execute(mousePosition, selectedPlayerObjects, positionObjects, obj);
        command = null;
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
        selectingBox.gameObject.SetActive(false);

        Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(startPosition, Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if(selectedPlayerObjects.Count != 0)
        {
            if (selectedPlayerObjects[0].GetComponent<PlayerUnit>() != null)
            {
                SelectPlayerUnits(false);
            }
        }
        
        selectedPlayerObjects.Clear();

        //Check if the selected gameObjects is a PlayerUnit
        foreach (Collider2D collider2D in collider2DArray)
        {
            if (collider2D.tag == "PlayerUnit")
            {
                selectedPlayerObjects.Add(collider2D.gameObject);
            }
        }
        if(selectedPlayerObjects.Count != 0)
        {
            SelectPlayerUnits(true);
            return;
        }

        //Check if selected gameObjects is a PlayerBuilding

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
        UIUnitPanel.GetComponent<UnitPanel>().SelectPlayerUnits(selectedPlayerObjects.Count);
    }

    void CreateControlGroup()
    {
        if (numberPressed > -1 && selectedPlayerObjects.Count != 0)
        {
            ControlGroups[numberPressed].Clear();
            ControlGroups[numberPressed].AddRange(selectedPlayerObjects);

            ControlGroupPanelObject.GetComponent<UIControlGroupPanel>().CreateControlGroup(numberPressed);
        }
    }

    void SelectControlGroup()
    {
        if(ControlGroups[numberPressed].Count != 0)
        {
            SelectPlayerUnits(false);
            selectedPlayerObjects.Clear();
            selectedPlayerObjects.AddRange(ControlGroups[numberPressed]);
            if (selectedPlayerObjects.Count > 0)
            {
                if (selectedPlayerObjects[0].GetComponent<PlayerUnit>() != null)
                {
                    SelectPlayerUnits(true);
                }
            }
            ControlGroupPanelObject.GetComponent<UIControlGroupPanel>().SelectGroup(numberPressed);
        }
    }

    public void SetCommand(Command newCommand)
    {
        command = newCommand;
    }
}

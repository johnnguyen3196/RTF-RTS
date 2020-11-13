using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitPanel : MonoBehaviour
{
    public GameObject PlayerUnit;
    public GameObject PlayerBuilding;

    public Vector2 topLeft;

    public List<GameObject> UIElements;

    void Start()
    {
        UIElements = new List<GameObject>();
    }

    public void ClearUnitPanel()
    {
        foreach (GameObject element in UIElements)
        {
            if(element.GetComponent<UIUnit>() != null)
                element.GetComponent<UIUnit>().Deselect();

            Destroy(element);
        }
        UIElements.Clear();
    }

    public void SelectPlayerUnits(List<GameObject> playerUnits)
    {
        ClearUnitPanel();

        int number = playerUnits.Count;
        double rows = Math.Ceiling((double)number / 10d);
        int remainder = number % 10;
        int unitNumber = 0;
        for(int row = 0; row < (int)rows; row++)
        {
            int numberOfCol = 0;
            if(row == rows - 1)
            {
                if(number == 10)
                {
                    numberOfCol = 10;
                } else
                {
                    numberOfCol = remainder;
                }
            }
            else
            {
                numberOfCol = 10;
            }
            
            for(int col = 0; col < numberOfCol; col++)
            {
                GameObject go = Instantiate(PlayerUnit, gameObject.transform, false);
                //Starting from top left of panel, move the UI component based on column and row.
                go.transform.localPosition = new Vector3(topLeft.x + (col * 100), topLeft.y - (row * 100));
                go.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = unitNumber.ToString();
                UIElements.Add(go);
                UIUnit unit = go.GetComponent<UIUnit>();
                unit.unitObject = playerUnits[unitNumber];
                unit.SetMaxHealth(50);
                unit.SetHealth(playerUnits[unitNumber].GetComponent<PlayerUnit>().health);
                unitNumber++;
            }
        }
    }

    public void SelectPlayerBuilding()
    {
        ClearUnitPanel();

        GameObject go = Instantiate(PlayerBuilding, gameObject.transform, false);
        go.transform.localPosition = new Vector3(topLeft.x + 20, topLeft.y);
        UIElements.Add(go);
    }
}

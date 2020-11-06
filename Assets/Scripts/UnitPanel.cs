using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitPanel : MonoBehaviour
{
    public GameObject PlayerUnit;
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
            Destroy(element);
        }
        UIElements.Clear();
    }

    public void SelectPlayerUnits(int number)
    {
        foreach(GameObject element in UIElements)
        {
            Destroy(element);
        }
        UIElements.Clear();

        double rows = Math.Ceiling((double)number / 10d);
        int remainder = number % 10;
        for(int row = 0; row < (int)rows; row++)
        {
            int numberOfCol;
            if(row == rows - 1)
            {
                numberOfCol = remainder;
            }
            else
            {
                numberOfCol = 11;
            }
            
            for(int col = 0; col < numberOfCol; col++)
            {
                GameObject go = Instantiate(PlayerUnit, gameObject.transform, false);
                //Starting from top left of panel, move the UI component based on column and row.
                go.transform.localPosition = new Vector3(topLeft.x + (col * 100), topLeft.y - (row * 100));
                go.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>().text = (row * 11 + col).ToString();
                UIElements.Add(go);
            }
        }
    }
}

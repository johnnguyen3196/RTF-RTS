using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class UIControlGroupPanel : MonoBehaviour
{
    public GameObject[] ButtonObjects;
    public int currentToggle = -1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CreateControlGroup(int number)
    {
        DisableCurrentGroup();
        currentToggle = number;

        GameObject go = ButtonObjects[number];
        go.SetActive(true);

        Image image = go.transform.GetChild(3).GetComponent<Image>();
        Color color = image.color;
        color.a = 1f;
        image.color = color;
    }

    public void SelectGroup(int number)
    {
        DisableCurrentGroup();
        currentToggle = number;

        GameObject go = ButtonObjects[number];
        go.SetActive(true);

        Image image = go.transform.GetChild(3).GetComponent<Image>();
        Color color = image.color;
        color.a = 1f;
        image.color = color;
    }

    private void DisableCurrentGroup()
    {
        if(currentToggle > -1)
        {
            GameObject go = ButtonObjects[currentToggle];

            Image image = go.transform.GetChild(3).GetComponent<Image>();
            Color color = image.color;
            color.a = .59f;
            image.color = color;
        }
    }
}

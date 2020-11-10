using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductionBar : MonoBehaviour
{
    public GameObject target;
    public Slider slider;
    public float offset = 0;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(target.transform.position);
        gameObject.transform.position = screenPos;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(target.transform.position);
        screenPos.y += offset;
        gameObject.transform.position = screenPos;
    }

    public void SetMaxTime(float time)
    {
        slider.maxValue = time;
        slider.value = time;
    }

    public void SetTimer(float time)
    {
        slider.value = time;
    }
}

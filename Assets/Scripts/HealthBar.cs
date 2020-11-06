using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject target;
    public Slider slider;

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
        screenPos.y -= 40;
        gameObject.transform.position = screenPos;
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}

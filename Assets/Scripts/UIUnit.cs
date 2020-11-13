using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUnit : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public GameObject unitObject;

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(float health)
    {
        health = Mathf.Floor(health);
        slider.value = health;
        Color color = gradient.Evaluate(slider.normalizedValue);
        color.a = .4f;
        fill.color = color;
        if (health < 0)
        {
            health = 0;
        }
    }

    public void Deselect()
    {
        unitObject.GetComponent<PlayerUnit>().UIUnit = null;
    }
}

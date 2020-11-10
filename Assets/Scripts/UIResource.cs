using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIResource : MonoBehaviour
{
    public TextMeshProUGUI uINumber;
    public float money;
    // Start is called before the first frame update
    void Start()
    {
        uINumber.text = ((int)money).ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        money += (150f / 60f) * Time.fixedDeltaTime;
        uINumber.text = ((int)money).ToString();
    }
}

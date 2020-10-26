using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : MonoBehaviour
{
    //public AIPath aiPath;
    //public AIDestinationSetter aIDestinationSetter;
    private GameObject selectedGameObject;
    

    // Start is called before the first frame update
    void Start()
    {
        selectedGameObject = transform.Find("Selected").gameObject;
        SetSelected(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSelected(bool visible)
    {
        selectedGameObject.SetActive(visible);
    }

    public void SetTarget()
    {

    }
}

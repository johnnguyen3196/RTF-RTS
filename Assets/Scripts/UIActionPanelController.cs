using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActionPanelController : MonoBehaviour
{
    public GameObject RTSController;

    public void Move()
    {
        RTSController.GetComponent<RTSController>().SetCommand(new Move());
    }

    public void AttackMove()
    {
        RTSController.GetComponent<RTSController>().SetCommand(new AttackMove());
    }
}

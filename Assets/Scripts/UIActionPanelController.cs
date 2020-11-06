using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActionPanelController : MonoBehaviour
{
    public GameObject RTSController;

    public void Move()
    {
        RTSController rts = RTSController.GetComponent<RTSController>();
        rts.SetCommand(new Move());
        rts.buttonPressed = true;
    }

    public void AttackMove()
    {
        RTSController rts = RTSController.GetComponent<RTSController>();
        rts.SetCommand(new AttackMove());
        rts.buttonPressed = true;
    }
}

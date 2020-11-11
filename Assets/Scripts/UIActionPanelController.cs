using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActionPanelController : MonoBehaviour
{
    public GameObject RTSController;
    public GameObject PlayerUnitPanel;
    public GameObject PlayerBarracksPanel;

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

    public void Stop()
    {
        RTSController rts = RTSController.GetComponent<RTSController>();
        rts.SetCommand(new Stop());
        rts.buttonPressed = true;
        rts.ExecuteCommand(null);
    }

    public void Produce()
    {
        RTSController rts = RTSController.GetComponent<RTSController>();
        rts.selectedPlayerObjects[0].GetComponent<PlayerBarracks>().Produce();
        rts.buttonPressed = true;
    }

    public void Rally()
    {
        RTSController rts = RTSController.GetComponent<RTSController>();
        rts.SetRallyCommand();
        rts.buttonPressed = true;
    }

    public void EnablePlayerUnitPanel()
    {
        PlayerUnitPanel.SetActive(true);
        PlayerBarracksPanel.SetActive(false);
    }

    public void EnablePlayerBarracksPanel()
    {
        PlayerUnitPanel.SetActive(false);
        PlayerBarracksPanel.SetActive(true);
    }

    public void DisableActionPanel()
    {
        PlayerUnitPanel.SetActive(false);
        PlayerBarracksPanel.SetActive(false);
    }
}

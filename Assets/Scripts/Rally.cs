using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rally : Command
{
    public Transform rally;
    public void execute(List<GameObject> gameObjects, List<GameObject> moveObjects, GameObject rightClickObject)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        rally.position = mousePosition;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Command
{
    public GameObject target;
    public void execute(List<GameObject> gameObjects, List<GameObject> moveObjects, GameObject rightClickObject)
    {
        if (gameObjects.Count != 0)
        {
            if (gameObjects[0].GetComponent<PlayerUnit>() != null)
            {
                foreach (GameObject obj in moveObjects)
                {
                    GameObject.Destroy(obj);
                }
                moveObjects.Clear();

                foreach (GameObject unit in gameObjects)
                {
                    unit.GetComponent<PlayerUnit>().Attack(target);
                }
            }

        }
        rightClickObject.GetComponent<RightClickObject>().attack = true;
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }
}

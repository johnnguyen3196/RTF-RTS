using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : Command
{
    public Vector3 mousePosition;
    public void execute(List<GameObject> gameObjects, List<GameObject> moveObjects, GameObject rightClickObject)
    {
        //check if the objects the player selected is movable by player
        if (gameObjects.Count != 0)
        {
            if (gameObjects[0].GetComponent<PlayerUnit>() != null)
            {
                foreach (GameObject obj in moveObjects)
                {
                    GameObject.Destroy(obj);
                }
                moveObjects.Clear();

                float distance = 1.25f;
                int count = gameObjects.Count;
                for (int i = 0; i < count; i++)
                {
                    float angle = i * (360f / count);
                    Vector3 dir = Quaternion.Euler(0, 0, angle) * new Vector3(1, 0, 0);
                    Vector3 position = mousePosition + dir * distance;

                    GameObject obj = new GameObject("PosObj" + i);
                    obj.transform.position = position;
                    moveObjects.Add(obj);

                    gameObjects[i].GetComponent<PlayerUnit>().Move(obj.transform);
                }
            }
        }
        rightClickObject.GetComponent<RightClickObject>().attack = false;
    }

    public void SetMousePosition(Vector3 newMousePosition)
    {
        mousePosition = newMousePosition;
    }
}

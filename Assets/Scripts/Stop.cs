﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stop : Command
{
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
                    unit.GetComponent<PlayerUnit>().Stop();
                }
            }

        }
    }
}

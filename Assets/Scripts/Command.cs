using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Command
{
    void execute(Vector3 mousePosition, List<GameObject> gameObjects, List<GameObject> moveObjects, GameObject rightClickObject);
}

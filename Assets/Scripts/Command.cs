using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Command
{
    void execute(List<GameObject> gameObjects, List<GameObject> moveObjects, GameObject rightClickObject);
}

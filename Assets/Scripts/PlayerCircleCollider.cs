using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCircleCollider : MonoBehaviour
{
    public GameObject target = null;

    private PlayerUnit parent;

    void Start()
    {
        parent = transform.parent.GetComponent<PlayerUnit>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyBuilding")
        {
            parent.enemiesInRange.Add(collision.gameObject);
        }
        if (target != null)
        {
            if (collision.gameObject.name == target.name)
            {
                parent.inRangeOfTarget = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == target)
        {
            parent.DeAggro();
            parent.inRangeOfTarget = false;

            int index = parent.enemiesInRange.FindIndex(enemy => enemy.name == collision.gameObject.name);
            if(index != -1)
            {
                parent.enemiesInRange.RemoveAt(index);
            }
        }
    }

    public void SetTarget(GameObject o)
    {
        target = o;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCircleCollider : MonoBehaviour
{
    private GameObject target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == target.name)
        {
            transform.parent.GetComponent<EnemyUnit>().EnableAttack(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == target)
        {
            transform.parent.GetComponent<EnemyUnit>().EnableAttack(false);
        }
    }

    public void SetTarget(GameObject o)
    {
        target = o;
    }
}

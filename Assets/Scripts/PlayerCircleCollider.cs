using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCircleCollider : MonoBehaviour
{
    public GameObject target = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(target != null)
        {
            if (collision.gameObject.name == target.name)
            {
                transform.parent.GetComponent<PlayerUnit>().EnableAttack(true);
            }
        } else
        {
            if(collision.gameObject.tag == "Enemy")
            {
                transform.parent.GetComponent<PlayerUnit>().GetNewTarget();
                transform.parent.GetComponent<PlayerUnit>().EnableAttack(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == target)
        {
            transform.parent.GetComponent<PlayerUnit>().EnableAttack(false);
        }
    }

    public void SetTarget(GameObject o)
    {
        target = o;
    }
}

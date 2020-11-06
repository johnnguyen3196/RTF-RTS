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
        if(target != null)
        {
            if (collision.gameObject.name == target.name)
            {
                //transform.parent.GetComponent<PlayerUnit>().EnableAttack(true);
                parent.inRangeOfTarget = true;
            }
        } else
        {
            if(collision.gameObject.tag == "Enemy")
            {
                parent.AttackMove();
                //transform.parent.GetComponent<PlayerUnit>().EnableAttack(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == target)
        {
            parent.DeAggro();
            parent.inRangeOfTarget = false;
            //parent.AttackInRange();
        }
    }

    public void SetTarget(GameObject o)
    {
        target = o;
    }
}

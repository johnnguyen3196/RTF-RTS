using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Debug = UnityEngine.Debug;

public class EnemyUnit : MonoBehaviour
{
    public AIPath aiPath;
    public AIDestinationSetter aIDestinationSetter;
    //private GameObject selectedGameObject;
    public Animator animator;

    public GameObject gun;

    private GameObject target = null;

    private bool dying = false;

    private float switchTargetTimer;
    // Start is called before the first frame update
    void Start()
    {
        GetNewTarget();
        transform.GetChild(0).gameObject.GetComponent<EnemyUnitGun>().SetTarget(target);
        transform.GetChild(1).gameObject.GetComponent<EnemyCircleCollider>().SetTarget(target);
        switchTargetTimer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null || switchTargetTimer < Time.time)
        {
            GetNewTarget();
            switchTargetTimer += 1f;
        }
        if (!dying)
        {
            Vector2 movement = new Vector2(aiPath.desiredVelocity.x, aiPath.desiredVelocity.y);
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
    }

    void GetNewTarget()
    {
        List<GameObject> potentialTargets = new List<GameObject>(GameObject.FindGameObjectsWithTag("PlayerUnit"));
        //Add player builidng to list here;
        if(potentialTargets.Count > 0)
        {
            float closestDistance = 1000f;
            int closestIndex = 0;
            for(int i = 0; i < potentialTargets.Count; i++)
            {
                float distance = Vector3.Distance(transform.position, potentialTargets[i].transform.position);
                if(distance < closestDistance)
                {
                    closestDistance = distance;
                    closestIndex = i;
                }
            }
            target = potentialTargets[closestIndex];
            aIDestinationSetter.target = target.transform;
            transform.GetChild(0).gameObject.GetComponent<EnemyUnitGun>().SetTarget(target);
            transform.GetChild(1).gameObject.GetComponent<EnemyCircleCollider>().SetTarget(target);
        } else
        {
            //player has no more units, AI stops
            aIDestinationSetter.target = gameObject.transform;
        }
    }

    public void EnableAttack(bool attack)
    {
        gun.GetComponent<EnemyUnitGun>().EnableAttack(attack);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            //unit.TakeDamage(collision.gameObject.GetComponent<PlayerBullet>().damage);
        }
    }
}

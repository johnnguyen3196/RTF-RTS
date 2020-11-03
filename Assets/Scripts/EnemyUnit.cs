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

    private GameObject target = null;

    private bool dying = false;
    // Start is called before the first frame update
    void Start()
    {
        GetNewTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null)
        {
            GetNewTarget();
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
        } else
        {
            //player has no more units, AI stops
            aIDestinationSetter.target = gameObject.transform;
        }
    }
}

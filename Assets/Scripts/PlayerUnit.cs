using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class PlayerUnit : MonoBehaviour
{
    public AIPath aiPath;
    public AIDestinationSetter aIDestinationSetter;
    private GameObject selectedGameObject;
    public Animator animator;

    private bool dying = false;

    private GameObject target;

    public GameObject gun;

    // Start is called before the first frame update
    void Start()
    {
        selectedGameObject = transform.Find("Selected").gameObject;
        SetSelected(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!dying)
        {
            Vector2 movement = new Vector2(aiPath.desiredVelocity.x, aiPath.desiredVelocity.y);
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
    }

    public void SetSelected(bool visible)
    {
        selectedGameObject.SetActive(visible);
    }

    public void Move(Transform movementLocation)
    {
        aiPath.endReachedDistance = 0.2f;
        aIDestinationSetter.target = movementLocation;
    }

    public void GetNewTarget()
    {
        List<GameObject> potentialTargets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        //Add enemy building to list here;
        if (potentialTargets.Count > 0)
        {
            float closestDistance = 1000f;
            int closestIndex = 0;
            for (int i = 0; i < potentialTargets.Count; i++)
            {
                float distance = Vector3.Distance(transform.position, potentialTargets[i].transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestIndex = i;
                }
            }
            target = potentialTargets[closestIndex];
            aiPath.endReachedDistance = 5f;
            aIDestinationSetter.target = target.transform;
            transform.GetChild(0).gameObject.GetComponent<PlayerUnitGun>().SetTarget(target);
            transform.GetChild(1).gameObject.GetComponent<PlayerCircleCollider>().SetTarget(target);
        }
        else
        {
            //no more enemy targets stop
            aiPath.endReachedDistance = 0.2f;
            aIDestinationSetter.target = gameObject.transform;
        }
    }

    public void EnableAttack(bool attack)
    {
        gun.GetComponent<PlayerUnitGun>().EnableAttack(attack);
    }
}

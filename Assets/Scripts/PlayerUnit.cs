using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class PlayerUnit : MonoBehaviour
{
    public AIPath aiPath;
    public AIDestinationSetter aIDestinationSetter;
    private GameObject selectedGameObject;
    public Animator animator;

    private bool dying = false;

    public GameObject target;

    public GameObject gun;

    public int health;

    public Vector2 movement;

    public List<GameObject> enemiesInRange;
    public bool inRangeOfTarget = false;

    private float closestDistance = 1000f;

    public GameObject UIHealthBarPrefab;
    public GameObject UIHealthBar;
    public HealthBar healthBar;

    public GameObject UIUnit;

    // Start is called before the first frame update
    void Start()
    {
        enemiesInRange = new List<GameObject>();

        selectedGameObject = transform.Find("Selected").gameObject;
        SetSelected(false);
        movement = new Vector2(0, 0);

        UIHealthBar = Instantiate(UIHealthBarPrefab, transform.position, Quaternion.identity);
        UIHealthBar.transform.SetParent(GameObject.Find("Canvas").transform);
        healthBar = UIHealthBar.GetComponent<HealthBar>();
        healthBar.SetMaxHealth(health);
        healthBar.target = gameObject;
        healthBar.offset = -40f;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null && enemiesInRange.Count > 0)
        {
            target = enemiesInRange[Random.Range(0, enemiesInRange.Count)];
            inRangeOfTarget = true;
            SetChildrenTarget();
        }
        if (!dying)
        {
            movement = new Vector2(aiPath.desiredVelocity.x, aiPath.desiredVelocity.y);
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

    public void Stop()
    {
        aiPath.endReachedDistance = 0.2f;
        aIDestinationSetter.target = transform;
    }

    public GameObject GetNewTarget()
    {
        List<GameObject> potentialTargets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        //Add enemy building to list here;
        potentialTargets.Add(GameObject.Find("EnemyBarracks"));
        if (potentialTargets.Count > 0)
        {
            closestDistance = 1000f;
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
            return potentialTargets[closestIndex];
        }
        else
        {
            //no more enemy targets stop
            aiPath.endReachedDistance = 0.2f;
            aIDestinationSetter.target = gameObject.transform;
            return null;
        }
    }

    public void AttackMove()
    {
        target = GetNewTarget();
        aiPath.endReachedDistance = 5f;
        aIDestinationSetter.target = target.transform;

        if (closestDistance <= 7f)
        {
            inRangeOfTarget = true;
        }
        SetChildrenTarget();
    }

    public void Attack(GameObject attackTarget)
    {
        target = attackTarget;
        aiPath.endReachedDistance = 5f;
        aIDestinationSetter.target = target.transform;
        
        if(Vector3.Distance(transform.position, attackTarget.transform.position) <= 7f)
        {
            inRangeOfTarget = true;
        }
        SetChildrenTarget();
    }

    private void SetChildrenTarget()
    {
        transform.GetChild(0).gameObject.GetComponent<PlayerUnitGun>().SetTarget(target);
        transform.GetChild(1).gameObject.GetComponent<PlayerCircleCollider>().SetTarget(target);
    }

    public void AttackInRange()
    {
        GameObject potentialTarget = GetNewTarget();
        if(closestDistance <= 7f)
        {
            target = potentialTarget;
            aiPath.endReachedDistance = 5f;
            aIDestinationSetter.target = target.transform;
            inRangeOfTarget = true;
            SetChildrenTarget();
        }
    }

    public void DeAggro()
    {
        target = null;
        SetChildrenTarget();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
        animator.SetTrigger("Damage");
        if(UIUnit != null)
        {
            UIUnit.GetComponent<UIUnit>().SetHealth(health);
        }
        if(health <= 0)
        {
            FindObjectOfType<SoundManager>().Play("PlayerDeath");
            dying = true;
            Destroy(UIHealthBar);
            GameObject.Find("RTSController").GetComponent<RTSController>().NotifyDeath(gameObject.name);
            //temp
            Destroy(gameObject);
        }
    }
}

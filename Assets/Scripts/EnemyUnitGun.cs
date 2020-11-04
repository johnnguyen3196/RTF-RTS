using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitGun : MonoBehaviour
{
    public float GunRadius;

    public float attackSpeed;

    private float attackTimer;

    public GameObject EnemyBulletObject;

    public bool enableAttack;

    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        enableAttack = false;
        attackTimer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {
            Vector3 gunTargetVector = target.transform.position - transform.position;
            gunTargetVector.z = 0;
            Vector3 offset = gunTargetVector.normalized * GunRadius;
            transform.position = transform.parent.transform.position + offset;
            transform.right = gunTargetVector.normalized;

            if (enableAttack && attackTimer < Time.time)
            {
                //create bullet object
                GameObject go = Instantiate(EnemyBulletObject, transform.position, Quaternion.identity);
                EnemyBullet bullet = go.GetComponent<EnemyBullet>();
                bullet.targetVector = gunTargetVector;
                attackTimer += attackSpeed;
            }
        } else
        {
            Vector3 gunTargetVector = new Vector3(-1, 0, 0);
            Vector3 offset = gunTargetVector.normalized * GunRadius;
            transform.position = transform.parent.transform.position + offset;
            transform.right = gunTargetVector.normalized;
        }
    }

    public void EnableAttack(bool attack)
    {
        enableAttack = attack;
    }

    public void SetTarget(GameObject o)
    {
        target = o;
    }
}

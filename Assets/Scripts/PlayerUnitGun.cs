using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitGun : MonoBehaviour
{
    public float GunRadius;

    public float fireRate;
    private float fireTime = 0;
    public int damage;

    private GameObject target;

    private bool enableAttack = false;

    public GameObject playerBulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        fireTime = Time.time + fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 gunTargetVector = target.transform.position - transform.position;
            gunTargetVector.z = 0;
            Vector3 offset = gunTargetVector.normalized * GunRadius;
            transform.position = transform.parent.transform.position + offset;
            transform.right = gunTargetVector.normalized;

            if (enableAttack && fireTime < Time.time)
            {
                //create bullet object
                GameObject go = Instantiate(playerBulletPrefab, transform.position, Quaternion.identity);
                PlayerBullet bullet = go.GetComponent<PlayerBullet>();
                bullet.targetVector = gunTargetVector;
                fireTime += fireRate;
            }
        }
        else
        {
            Vector3 gunTargetVector = new Vector3(-1, 0, 0);
            Vector3 offset = gunTargetVector.normalized * GunRadius;
            transform.position = transform.parent.transform.position + offset;
            transform.right = gunTargetVector.normalized;
        }
    }

    public void SetTarget(GameObject o)
    {
        target = o;
    }

    public void EnableAttack(bool attack)
    {
        enableAttack = attack;
    }
}

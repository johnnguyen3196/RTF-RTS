using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnitGun : MonoBehaviour
{
    public float GunRadius;

    public float fireRate;
    private float fireTime = 0;
    public int damage;

    public GameObject target;

    //public GameObject enemyBulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        fireTime = Time.time + fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 gunTargetVector = new Vector3(1, 0, 0);
        gunTargetVector.z = 0;
        Vector3 offset = gunTargetVector.normalized * GunRadius;
        transform.position = transform.parent.transform.position + offset;
        transform.right = gunTargetVector.normalized;

        if (fireTime < Time.time)
        {
            fireTime += fireRate;
            //FindObjectOfType<SoundManager>().Play("EnemyGunShot");
        }
    }
}

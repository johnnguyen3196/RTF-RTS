using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public Vector3 targetVector;
    public float speed;
    public int damage;
    private Rigidbody2D rb;
    //Allow bullet to hit only ONE object
    private bool hit = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(targetVector * speed);
        transform.right = targetVector;
        gameObject.layer = 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hit)
        {
            if (collision.gameObject.tag == "Terrain")
            {
                hit = true;
                Destroy(gameObject);
            }

            if (collision.gameObject.tag == "Enemy")
            {
                hit = true;
                EnemyUnit enemy = collision.gameObject.GetComponent<EnemyUnit>();
                //bool direction = transform.position.x < collision.transform.position.x ? true : false;
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }

            if (collision.gameObject.tag == "EnemyBuilding")
            {
                hit = true;
                EnemyBarracks enemy = collision.gameObject.GetComponent<EnemyBarracks>();
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}

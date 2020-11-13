using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public Vector3 targetVector;
    public float speed;
    public int damage;
    private Rigidbody2D rb;
    private bool hit = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(targetVector * speed);
        transform.right = targetVector;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "Terrain")
        //{
        //    Destroy(gameObject);
        //}
        if (!hit)
        {
            if (collision.gameObject.tag == "PlayerUnit")
            {
                hit = true;
                PlayerUnit player = collision.gameObject.GetComponent<PlayerUnit>();
                player.TakeDamage(damage);

                Destroy(gameObject);
            }

            if (collision.gameObject.tag == "PlayerBuilding")
            {
                hit = true;
                PlayerBarracks player = collision.gameObject.GetComponent<PlayerBarracks>();
                player.TakeDamage(damage);

                Destroy(gameObject);
            }
        }
    }
}

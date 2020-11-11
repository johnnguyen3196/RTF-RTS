using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public Vector3 targetVector;
    public float speed;
    public int damage;
    private Rigidbody2D rb;
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

        if (collision.gameObject.tag == "PlayerUnit")
        {
            PlayerUnit player = collision.gameObject.GetComponent<PlayerUnit>();
            player.TakeDamage(damage);

            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "PlayerBuilding")
        {
            PlayerBarracks player = collision.gameObject.transform.parent.gameObject.GetComponent<PlayerBarracks>();
            player.TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}

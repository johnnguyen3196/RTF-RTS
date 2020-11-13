using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyBarracks : MonoBehaviour
{
    private int unitProduction = 0;

    public GameObject enemyUnitPrefab;

    public GameObject UIHealthBarPrefab;
    private GameObject UIHealthBar;
    private HealthBar healthBar;
    public int health;

    public GameObject UIProductionBarPrefab;
    private GameObject UIProductionBar;
    private ProductionBar productionBar;

    private float productionTimer = 0;
    private bool producing = false;

    private float timer;

    public GameObject ExplosionObject;
    // Start is called before the first frame update
    void Start()
    {
        UIHealthBar = Instantiate(UIHealthBarPrefab, transform.position, Quaternion.identity);
        UIHealthBar.transform.SetParent(GameObject.Find("Canvas").transform);
        healthBar = UIHealthBar.GetComponent<HealthBar>();
        healthBar.SetMaxHealth(health);
        healthBar.target = gameObject;
        healthBar.offset = -130f;
        RectTransform rt = UIHealthBar.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(350, 15);

        UIProductionBar = Instantiate(UIProductionBarPrefab, transform.position, Quaternion.identity);
        UIProductionBar.transform.SetParent(GameObject.Find("Canvas").transform);
        productionBar = UIProductionBar.GetComponent<ProductionBar>();
        productionBar.SetMaxTime(5);
        productionBar.SetTimer(0);
        productionBar.target = gameObject;
        productionBar.offset = -115f;

        timer = Time.time + Random.Range(2, 6);
    }

    // Update is called once per frame
    void Update()
    {
        if (producing)
        {
            productionTimer += Time.deltaTime;
            if (productionTimer >= 5)
            {
                spawnOneUnit();
                unitProduction--;
                if (unitProduction == 0)
                {
                    producing = false;
                }
                productionTimer = 0;
            }
        }
        else
        {
            if (unitProduction > 0)
            {
                producing = true;
                productionTimer = 0;
            }
        }
        productionBar.SetTimer(productionTimer);

        //AI produces a unit every 20 - 35 seconds
        if(timer < Time.time)
        {
            Produce();
            timer += Random.Range(20, 36);
        }
    }

    private void spawnOneUnit()
    {
        Vector3 spawnPosition = transform.position;
        spawnPosition.y -= 4;
        spawnPosition.z = -.1f;
        GameObject go = Instantiate(enemyUnitPrefab, spawnPosition, Quaternion.identity);
    }

    public void Produce()
    {
        unitProduction++; 
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        healthBar.SetHealth(health);
        if (health <= 0)
        {
            GameObject.Find("RTSController").GetComponent<RTSController>().Win();

            GameObject go = Instantiate(ExplosionObject, transform.position, Quaternion.identity);
            Destroy(go, go.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
            Destroy(UIHealthBar);
            Destroy(UIProductionBar);
            //temp
            Destroy(gameObject);
        }
    }
}

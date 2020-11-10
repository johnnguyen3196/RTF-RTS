using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBarracks : MonoBehaviour
{
    private UIResource UIResource;

    public GameObject selectedCircle;

    public Transform rally;

    private int unitProduction = 0;

    public GameObject playerUnitPrefab;

    public GameObject UIHealthBarPrefab;
    private HealthBar healthBar;
    public int health;

    public GameObject UIProductionBarPrefab;
    private ProductionBar productionBar;

    private float productionTimer = 0;
    private bool producing = false;
    // Start is called before the first frame update
    void Start()
    {
        UIResource = GameObject.Find("Resource").GetComponent<UIResource>();

        GameObject UIHealthBar = Instantiate(UIHealthBarPrefab, transform.position, Quaternion.identity);
        UIHealthBar.transform.SetParent(GameObject.Find("Canvas").transform);
        healthBar = UIHealthBar.GetComponent<HealthBar>();
        healthBar.SetMaxHealth(health);
        healthBar.target = gameObject;
        healthBar.offset = -130f;
        RectTransform rt = UIHealthBar.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(350, 15);

        GameObject UIProductionBar = Instantiate(UIProductionBarPrefab, transform.position, Quaternion.identity);
        UIProductionBar.transform.SetParent(GameObject.Find("Canvas").transform);
        productionBar = UIProductionBar.GetComponent<ProductionBar>();
        productionBar.SetMaxTime(5);
        productionBar.SetTimer(0);
        productionBar.target = gameObject;
        productionBar.offset = -115f;
    }

    // Update is called once per frame
    void Update()
    {
        if (producing)
        {
            productionTimer += Time.deltaTime;
            if(productionTimer >= 5)
            {
                spawnOneUnit();
                unitProduction--;
                if(unitProduction == 0)
                {
                    producing = false;
                }
                productionTimer = 0;
            }
        } else
        {
            if(unitProduction > 0)
            {
                producing = true;
                productionTimer = 0;
            }
        }
        productionBar.SetTimer(productionTimer);
    }

    public void SetSelected(bool select)
    {
        selectedCircle.SetActive(select);
    }

    private void spawnOneUnit()
    {
        Vector3 spawnPosition = transform.position;
        spawnPosition.y -= 4;
        spawnPosition.z = -.1f;
        GameObject go = Instantiate(playerUnitPrefab, spawnPosition, Quaternion.identity);

        PlayerUnit unit = go.GetComponent<PlayerUnit>();
        unit.Move(rally);
    }

    public void Produce()
    {
        if(UIResource.money >= 50)
        {
            unitProduction++;
            UIResource.money -= 50;
        }
    }
}

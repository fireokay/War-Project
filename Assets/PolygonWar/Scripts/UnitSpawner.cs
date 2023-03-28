using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class UnitSpawner : MonoBehaviour
{
    public GameObject unitPrefab;
    public GameObject unitMachineGunnerPrefab;
    public GameObject objectPrefab;
    ObjectPool<GameObject> objectPool;
    private bool usePool;
    public float spawnRemaining = 2;
    public int spawningZoneX = 0;
    public int spawningZoneZ = 0;
    public UnitManager unitManager;

    public int minDeathBorder;
    public int maxDeathBorder;

    // Start is called before the first frame update
    void Start()
    {
        objectPool = new ObjectPool<GameObject>(OnObjectCreate, OnTake, OnRelease, OnObjectDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawnRemaining > 0)
        {
            spawnRemaining -= Time.deltaTime;
        }
        else
        {
            Spawn();
            spawnRemaining = 2;
        }
    }
    GameObject OnObjectCreate()
    {
        GameObject newObject = Instantiate(objectPrefab);
        newObject.AddComponent<PoolObject>().myPool = objectPool;
        return newObject;
    }
    void OnTake(GameObject poolObject)
    {
        poolObject.SetActive(true);
    }
    void OnRelease(GameObject poolObject)
    {
        poolObject.SetActive(false);
    }
    void OnObjectDestroy(GameObject poolObject)
    {
        Destroy(poolObject);
    }

    public void Spawn()
    {
        GameObject unit;
        if (Random.Range(1, 10) <= 3)
        {
            unit = usePool ? objectPool.Get() : Instantiate(unitMachineGunnerPrefab);
        }
        else
        {
            unit = usePool ? objectPool.Get() : Instantiate(unitPrefab);
        }
        unit.transform.SetParent(transform);
        DecideLifeLongevity(unit);

        int x = Random.Range(-spawningZoneX, spawningZoneX);
        int z = Random.Range(-spawningZoneZ, spawningZoneZ);
        unit.transform.localPosition = new Vector3(0, 0, 0);
        unit.transform.localPosition = unit.transform.localPosition + new Vector3(x, 0, z);

        unitManager.AddUnit(unit.GetComponent<Unit>());
    }

    void DecideLifeLongevity(GameObject unit)
    {
        int dividerToGroups = Random.Range(0, 10);
        if (dividerToGroups <= minDeathBorder)
        {
            int timeToDestroy = Random.Range(3, 10);
            unit.GetComponent<Unit>().SetTimeToDestroy(timeToDestroy);
            unit.name = "Soldier " + timeToDestroy;
        }
        else if (dividerToGroups >= maxDeathBorder)
        {
            int timeToDestroy = Random.Range(30, 40);
            unit.GetComponent<Unit>().SetTimeToDestroy(timeToDestroy);
            unit.name = "Soldier " + timeToDestroy;
        }
        else
        {
            int timeToDestroy = Random.Range(10, 20);
            unit.GetComponent<Unit>().SetTimeToDestroy(timeToDestroy);
            unit.name = "Soldier " + timeToDestroy;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawningZoneX, 0, spawningZoneZ));
    }
}

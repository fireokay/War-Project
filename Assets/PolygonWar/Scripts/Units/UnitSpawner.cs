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
    public float spawnRemaining;
    private float progressTimeToSpawn;
    public int spawningZoneX = 0;
    public int spawningZoneZ = 0;
    public UnitManager unitManager;

    public int minDeathBorder;
    public int maxDeathBorder;

    void Start()
    {
        objectPool = new ObjectPool<GameObject>(OnObjectCreate, OnTake, OnRelease, OnObjectDestroy);
        progressTimeToSpawn = spawnRemaining;
    }

    void Update()
    {
        if (progressTimeToSpawn > 0)
        {
            progressTimeToSpawn -= Time.deltaTime;
        }
        else
        {
            Spawn();
            progressTimeToSpawn = Random.Range(spawnRemaining - 1, spawnRemaining + 1);
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
        int timeToDestroy;

        if (dividerToGroups <= minDeathBorder)
        {
            timeToDestroy = Random.Range(3, 5);
        }
        else if (dividerToGroups >= maxDeathBorder)
        {
            timeToDestroy = Random.Range(12, 20);
        }
        else
        {
            timeToDestroy = Random.Range(7, 12);
        }

        unit.GetComponent<Unit>().SetTimeToDestroy(timeToDestroy);
        unit.name = "Soldier " + timeToDestroy;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawningZoneX, 0, spawningZoneZ));
    }
}

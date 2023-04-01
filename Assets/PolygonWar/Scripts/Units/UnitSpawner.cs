using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using TMPro;

public class UnitSpawner : MonoBehaviour
{
    public List<Unit> units = new();
    public GameObject unitPrefab;
    public GameObject unitMachineGunnerPrefab;
    public GameObject unitSubmachineGunnerPrefab;
    public GameObject objectPrefab;
    ObjectPool<GameObject> objectPool;
    private bool usePool;
    public float spawnRemaining;
    private float progressTimeToSpawn;
    public int spawningZoneX = 0;
    public int spawningZoneZ = 0;
    public Vector3 aimCoordinates;

    public int minDeathBorder;
    public int maxDeathBorder;

    void Start()
    {
        objectPool = new ObjectPool<GameObject>(OnObjectCreate, OnTake, OnRelease, OnObjectDestroy);
        progressTimeToSpawn = spawnRemaining;
    }

    void Update()
    {
        progressTimeToSpawn -= Time.deltaTime;
        if (progressTimeToSpawn < 0)
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
        int random = Random.Range(1, 10);
        if (random <= 3)
        {
            unit = usePool ? objectPool.Get() : Instantiate(unitMachineGunnerPrefab);
        }
        else if (random > 3 && random <= 5)
        {

            unit = usePool ? objectPool.Get() : Instantiate(unitSubmachineGunnerPrefab);
        } 
        else 
        { 
            unit = usePool ? objectPool.Get() : Instantiate(unitPrefab);
        }

        unit.transform.SetParent(transform);
        unit.transform.localPosition = new Vector3(Random.Range(-spawningZoneX, spawningZoneX), 0, Random.Range(-spawningZoneZ, spawningZoneZ));

        DecideLifeLongevity(unit);
        units.Add(unit.GetComponent<Unit>());
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
        unit.GetComponent<Unit>().TimeToDestroy = timeToDestroy;
        unit.name = "Soldier " + timeToDestroy;
        unit.GetComponentInChildren<TMP_Text>().text = "" + timeToDestroy;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawningZoneX, 0, spawningZoneZ));


        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.localPosition + aimCoordinates, 0.3f);
    }
}

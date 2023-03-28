using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class UnitSpawner : MonoBehaviour
{
    public GameObject objectPrefab;
    ObjectPool<GameObject> objectPool;
    private bool usePool;
    public float spawnRemaining = 2;
    public int spawningZoneX = 0;
    public int spawningZoneZ = 0;
    public UnitManager unitManager;

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
        GameObject unit = usePool ? objectPool.Get() : Instantiate(objectPrefab);
        unit.transform.SetParent(transform);
        unit.name = "Soldier " + Random.Range(0, 1000);
        int dividerToGroups = Random.Range(0, 10);
        if (dividerToGroups <= 6)
        {
            unit.GetComponent<Unit>().SetTimeToDestroy(Random.Range(3, 10));
        } 
        else if (dividerToGroups >= 10)
        {
            unit.GetComponent<Unit>().SetTimeToDestroy(Random.Range(30, 40));
        }
        else
        {
            unit.GetComponent<Unit>().SetTimeToDestroy(Random.Range(10, 20));
        }
        int x = Random.Range(-spawningZoneX, spawningZoneX);
        int z = Random.Range(-spawningZoneX, spawningZoneZ);
        unit.transform.localPosition = new Vector3(0, 0, 0);
        unit.transform.localPosition = unit.transform.localPosition + new Vector3(x, 0, z);
        unitManager.AddUnit(unit.GetComponent<Unit>());
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawningZoneX, 0, spawningZoneZ));
    }
}

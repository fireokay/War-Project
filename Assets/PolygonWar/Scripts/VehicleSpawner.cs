using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class VehicleSpawner : MonoBehaviour
{
    public GameObject boatPrefab;
    private GameObject objectPrefab;
    ObjectPool<GameObject> objectPool;
    private bool usePool;
    public float spawnRemaining;
    public int spawningZoneX = 0;
    public int spawningZoneZ = 0;
    public VehicleManager vehicleManager;

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
            spawnRemaining = 1;
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
        GameObject vehicle = usePool ? objectPool.Get() : Instantiate(boatPrefab);
        vehicle.transform.SetParent(transform);

        int x = Random.Range(-spawningZoneX, spawningZoneX);
        int z = Random.Range(-spawningZoneZ, spawningZoneZ);
        vehicle.transform.localPosition = new Vector3(0, 0, 0);
        vehicle.transform.localPosition = vehicle.transform.localPosition + new Vector3(x, 0, z);

        vehicleManager.AddVehicle(vehicle.GetComponent<Vehicle>());
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawningZoneX, 0, spawningZoneZ));
    }
}

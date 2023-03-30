using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleManager : MonoBehaviour
{
    public List<Vehicle> vehicles = new List<Vehicle>();
    public float speed;
    public Vector3 aimCoordinates;
    public int aimZoneX = 0;
    public int aimZoneZ = 0;
    public Vector3 startCoordinates;
    private void Start()
    {
        startCoordinates = transform.localPosition;
    }
    public void AddVehicle(Vehicle vehicle)
    {
        vehicles.Add(vehicle);

        int x = Random.Range(-aimZoneX, aimZoneX);
        int z = Random.Range(-aimZoneZ, aimZoneZ);

        vehicle.StartMovement(transform.localPosition + aimCoordinates + new Vector3(x, 0, z), speed);
        StartCoroutine(Destroy(vehicle));
    }
    public IEnumerator Destroy(Vehicle vehicle)
    {
        yield return new WaitForSeconds(vehicle.TimeToFlee);
        vehicle.StartMovement(startCoordinates, speed);
        yield return new WaitForSeconds(20);
        if (vehicles.Count > 0)
        {
            vehicles.Remove(vehicle);
            Destroy(vehicle.gameObject);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.localPosition + aimCoordinates, 0.3f);

        Gizmos.DrawWireCube(transform.localPosition + aimCoordinates, new Vector3(aimZoneX, 0, aimZoneZ));
    }
}

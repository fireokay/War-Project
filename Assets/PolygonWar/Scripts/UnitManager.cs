using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public List<Unit> units = new List<Unit>();
    public float speed;
    public Vector3 aimCoordinates;
    public int aimZoneX = 0;
    public int aimZoneZ = 0;
    private void Start()
    {
    }
    public void AddUnit(Unit unit)
    {
        units.Add(unit);

        int x = Random.Range(-aimZoneX, aimZoneX);
        int z = Random.Range(-aimZoneZ, aimZoneZ);

        unit.StartMovement(transform.localPosition + aimCoordinates + new Vector3(x, 0, z), speed);
        StartCoroutine(RandomTimer(unit));
    }

    public IEnumerator RandomTimer(Unit unit)
    {
        yield return new WaitForSeconds(unit.timeToDestroy);
        if (units.Count > 0)
        {
            Unit unitToDestroy = units[Random.Range(0, units.Count - 1)];
            units.Remove(unitToDestroy);
            Destroy(unitToDestroy.gameObject);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.localPosition + aimCoordinates, 0.3f);

        Gizmos.DrawWireCube(transform.localPosition + aimCoordinates, new Vector3(aimZoneX, 0, aimZoneZ));
    }
}

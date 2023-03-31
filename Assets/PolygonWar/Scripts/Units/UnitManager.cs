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
        yield return new WaitForSeconds(unit.TimeToDestroy);
        if (units.Count > 0)
        {
            Debug.Log(unit.sniperTracerTriggerZone);
            if (unit.sniperTracerTriggerZone.InHearingZone)
            {
                unit.PlaySniperTracerSound();
            }

            unit.GetComponent<ParticleSystem>().Stop();
            unit.deathParticleSystem.Play();
            yield return new WaitForSeconds(1);
            unit.deathParticleSystem.Stop();
            yield return new WaitForSeconds(1);
            units.Remove(unit);
            Destroy(unit.gameObject);
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.localPosition + aimCoordinates, 0.3f);

        Gizmos.DrawWireCube(transform.localPosition + aimCoordinates, new Vector3(aimZoneX, 0, aimZoneZ));
    }
}

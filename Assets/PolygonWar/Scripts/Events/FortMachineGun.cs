using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class FortMachineGun : MonoBehaviour
{
    [SerializeField] private float timeToMachineGunRow;
    private float startTimeToMachineGunRow;
    [SerializeField] private StudioEventEmitter dangerZoneMachineGun;
    [SerializeField] public bool InHearingZone = false;
    private void Start()
    {
        startTimeToMachineGunRow = timeToMachineGunRow;
    }

    private void Update()
    {
        if (InHearingZone)
        {
            StartCoroutine(WaitAndShootRow());
        }
        else
        {
            dangerZoneMachineGun.Stop();
        }
    }

    public IEnumerator WaitAndShootRow()
    {
        timeToMachineGunRow += Random.Range(0, 10);
        yield return new WaitForSeconds(timeToMachineGunRow);
        timeToMachineGunRow = startTimeToMachineGunRow;
        dangerZoneMachineGun.Play();
        StartCoroutine(WaitAndShootRow());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            InHearingZone = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            InHearingZone = true;
        }
    }
}

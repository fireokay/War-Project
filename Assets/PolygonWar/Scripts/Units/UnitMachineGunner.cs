using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FMODUnity;
using FMOD.Studio;

public class UnitMachineGunner : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    [SerializeField] private StudioEventEmitter machineGunEmitter;
    [SerializeField] private CheckTriggerZone machinegunTriggerZone;
    public void Start()
    {
        StartCoroutine(RandomShots());
    }
    private void Update()
    {
        
    }
    public IEnumerator RandomShots()
    {
        if (machinegunTriggerZone.InHearingZone == true)
        {
            PlayMachineGunShots();
        }
        yield return new WaitForSeconds(Random.Range(2, 4));
        StartCoroutine(RandomShots());
    }

    void PlayMachineGunShots()
    {
        machineGunEmitter.Play();
    }

    void StopMachineGunShots()
    {
        machineGunEmitter.Stop();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class UnitShooter : MonoBehaviour
{
    [SerializeField] private StudioEventEmitter _shooterEmitter;
    [SerializeField] private CheckTriggerZone _triggerZone;
    public void Start()
    {
        StartCoroutine(RandomShots());
    }
    public IEnumerator RandomShots()
    {
        if (_triggerZone.InHearingZone)
        {
            PlayMachineGunShots();
        }
        yield return new WaitForSeconds(Random.Range(2, 4));
        StartCoroutine(RandomShots());
    }

    void PlayMachineGunShots() => _shooterEmitter.Play();
}

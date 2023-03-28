using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitMachineGunner : MonoBehaviour
{
    FMOD.Studio.EventInstance machineGunEvent;
    public void Start()
    {
        gameObject.GetComponent<Unit>().floatingText.GetComponent<TextMeshPro>().text += "MG";
        machineGunEvent = FMODUnity.RuntimeManager.CreateInstance("event:/soldier/soldier_c1_machine_gun");
        machineGunEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform, GetComponent<Rigidbody>()));
        StartCoroutine(RandomShots(machineGunEvent));
    }

    public IEnumerator RandomShots(FMOD.Studio.EventInstance machineGunEvent)
    {
        Debug.Log("in");
        machineGunEvent.start();
        int time = Random.Range(4, 8);
        yield return new WaitForSeconds(time);
        RandomShots(machineGunEvent);
    }
}

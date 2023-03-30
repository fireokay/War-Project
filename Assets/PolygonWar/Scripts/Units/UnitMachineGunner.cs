using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FMODUnity;
using FMOD.Studio;

public class UnitMachineGunner : MonoBehaviour
{
    public EmitterRef machineGunEmitter;
    public TextMeshPro textMeshPro;
    public void Start()
    {
        StartCoroutine(RandomShots());
    }

    public IEnumerator RandomShots()
    {
        //PlayMachineGunShots();
        yield return new WaitForSeconds(Random.Range(2, 4));
        StartCoroutine(RandomShots());
    }

    //void PlayMachineGunShots()
    //{
    //    machineGunEmitter.Target.Play();
    //} 
}

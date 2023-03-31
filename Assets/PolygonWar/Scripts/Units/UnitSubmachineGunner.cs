using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FMODUnity;
using FMOD.Studio;

public class UnitSubmachineGunner : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    [SerializeField] private StudioEventEmitter submachineGunEmitter;
    public void Start()
    {
        StartCoroutine(RandomShots());
    }
    public IEnumerator RandomShots()
    {
        PlaySubmachineGunShots();
        yield return new WaitForSeconds(Random.Range(2, 4));
        StartCoroutine(RandomShots());
    }

    void PlaySubmachineGunShots()
    {
        submachineGunEmitter.Play();
    }

    void StopSubmachineGunShots()
    {
        submachineGunEmitter.Stop();
    }
}

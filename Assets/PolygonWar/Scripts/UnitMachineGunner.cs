using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitMachineGunner : MonoBehaviour
{
    private FMOD.Studio.EventInstance _machineGunEvent;
    public TextMeshPro textMeshPro;

    AudioEventVisualizer _visualizer;
    private bool _IsPlaying;
    public void Start()
    {
        _visualizer = gameObject.GetComponent<AudioEventVisualizer>();
        textMeshPro.text += "MG";
        gameObject.name += "MG";

        StartCoroutine(RandomShots());
    }

    public IEnumerator RandomShots()
    {
        yield return new WaitForSeconds(Random.Range(1, 2));
        PlayMachineGunShots();
        yield return new WaitForSeconds(2);
        _IsPlaying = false;
        yield return new WaitForSeconds(Random.Range(2, 4));
        StartCoroutine(RandomShots());
    }

    void PlayMachineGunShots()
    {
        _machineGunEvent = FMODUnity.RuntimeManager.CreateInstance("event:/soldier/soldier_c1_machine_gun");
        _machineGunEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform, GetComponent<Rigidbody>()));
        _machineGunEvent.start();
        _IsPlaying = true;
        _machineGunEvent.release();
    }
    private void OnDrawGizmos()
    {
        if (_IsPlaying)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, 0.3f);
        }
        else
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, 0.25f);
        }
    }

}

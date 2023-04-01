using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FMODUnity;
using FMOD.Studio;

public class Unit : MonoBehaviour
{
    [SerializeField] private float _timeBetweenFootsteps;
    private float timeFootsteps;
    public float TimeToDestroy;
    [SerializeField] private StudioEventEmitter footstepsEmitter;
    [SerializeField] private StudioEventEmitter sniperTracerEmitter;
    [SerializeField] private StudioEventEmitter sniperShotEmitter;
    [SerializeField] private bool IsMoving = false;
    [SerializeField] private CheckTriggerZone footstepsTriggerZone;
    public ParticleSystem deathParticleSystem;
    [SerializeField] public CheckTriggerZone sniperTracerTriggerZone;
    public UnitSpawner unitSpawner;

    private void Start()
    {
        timeFootsteps = _timeBetweenFootsteps;
        deathParticleSystem.Stop();
        unitSpawner = GetComponentInParent<UnitSpawner>();
        StartCoroutine(Destroy());
        StartMovement(unitSpawner.aimCoordinates, 2f);
    }
    private void Update()
    {
        PlayFootsteps();
    }

    private void PlayFootsteps()
    {
        _timeBetweenFootsteps -= Time.deltaTime;
        if (_timeBetweenFootsteps > 0) return;
        if (!footstepsTriggerZone.InHearingZone || !IsMoving) return;
        PlayAndStopFootsteps();
        _timeBetweenFootsteps = timeFootsteps;
    }

    public IEnumerator Move(Vector3 aimCoordinates, float speed)
    {
        IsMoving = true;
        float baseTimeBetweenFootsteps = _timeBetweenFootsteps;
        float time = 0f;
        Vector3 startPosition = transform.position;
        float totalTime = Vector3.Distance(startPosition, aimCoordinates) / speed;

        while (time < totalTime)
        {
            transform.position = Vector3.Lerp(startPosition, aimCoordinates, time / totalTime);
            time += Time.deltaTime;
            yield return null;
        }
        IsMoving = false;
    }

    public void StartMovement(Vector3 aimCoordinates, float speed)
    {
        StartCoroutine(Move(aimCoordinates, speed));
    }

    public void PlayAndStopFootsteps()
    {
        footstepsEmitter.Play();
    }
    public void PlaySniperTracerSound() => sniperTracerEmitter.Play();
    public void PlaySniperShotSound() => sniperShotEmitter.Play();

    public IEnumerator Destroy()
    {
        yield return new WaitForSeconds(TimeToDestroy);
        if (sniperTracerTriggerZone.InHearingZone)
        {
            PlaySniperTracerSound();
        }

        GetComponent<ParticleSystem>().Stop();
        deathParticleSystem.Play();
        yield return new WaitForSeconds(1);
        deathParticleSystem.Stop();
        yield return new WaitForSeconds(1);
        unitSpawner.units.Remove(this);
        Destroy(this.gameObject);
    }
}

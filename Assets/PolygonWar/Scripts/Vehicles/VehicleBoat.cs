using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class VehicleBoat : MonoBehaviour
{
    public GameObject door;
    private int rotateDoorDegree = 65;
    [SerializeField] private StudioEventEmitter boatDoorEmitter;
    [SerializeField] private CheckTriggerZone doorTriggerZone;
    public IEnumerator OpenDoor()
    {
        if (doorTriggerZone)
        {
            PlayBoatDoor();
        }
        Debug.Log("opening door");
        float time = 0f;
        Vector3 start = new Vector3(door.transform.rotation.x, 0, 0);
        float totalTime = Vector3.Distance(start, new Vector3(rotateDoorDegree, door.transform.position.y, door.transform.position.z)) / 10;

        while (time < totalTime)
        {
            door.transform.RotateAround(door.transform.position, Vector3.left, 20f * Time.deltaTime);
            time += Time.deltaTime;
            if (door.transform.eulerAngles.x >= 55)
            {
                break;
            }
            yield return null;
        }
        Debug.Log("finished ^_^");
    }

    public void PlayBoatDoor()
    {
        boatDoorEmitter.Play();
    }
}

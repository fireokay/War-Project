using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<CheckTriggerZone>().InHearingZone = true;
    }
    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<CheckTriggerZone>().InHearingZone = true;
    }
}

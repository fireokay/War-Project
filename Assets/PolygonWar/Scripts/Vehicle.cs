using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Vehicle : MonoBehaviour
{
    public float TimeToFlee;
    public IEnumerator Move(Vector3 aimCoordinates, float speed)
    {
        float time = 0f;
        Vector3 start = transform.position;
        float totalTime = Vector3.Distance(start, aimCoordinates) / speed;

        while (time < totalTime)
        {
            transform.position = Vector3.Lerp(start, new Vector3(transform.position.x, transform.position.y, aimCoordinates.z), time / totalTime);
            time += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(10);
        gameObject.GetComponent<VehicleManager>().Destroy(this);
    }

    public void StartMovement(Vector3 aimCoordinates, float speed)
    {
        StartCoroutine(Move(aimCoordinates, speed));
    }
}

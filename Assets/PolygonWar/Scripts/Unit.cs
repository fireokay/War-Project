using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Unit : MonoBehaviour
{
    public float TimeToDestroy;
    public IEnumerator Move(Vector3 aimCoordinates, float speed)
    {
        float time = 0f;
        Vector3 start = transform.position;
        float totalTime = Vector3.Distance(start, aimCoordinates) / speed;

        while (time < totalTime)
        {
            transform.position = Vector3.Lerp(start, aimCoordinates, time / totalTime);
            time += Time.deltaTime;
            yield return null;
        }
    }

    public void StartMovement(Vector3 aimCoordinates, float speed)
    {
        StartCoroutine(Move(aimCoordinates, speed));
    }

    public void SetTimeToDestroy(float time)
    {
        TimeToDestroy = time;
        gameObject.GetComponentInChildren<TextMeshPro>().text = "" + TimeToDestroy;
    }
}

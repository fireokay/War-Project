using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Unit : MonoBehaviour
{
    public float timeToDestroy;
    public GameObject floatingText;
    public bool machineGunner = false;
    private void Start()
    {
        int x = Random.Range(0, 100);
        if (x <= 50)
        {
            gameObject.AddComponent<UnitMachineGunner>();
        }
    }
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
        timeToDestroy = time;
        floatingText.GetComponent<TextMeshPro>().text = "" + timeToDestroy;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitNavMesh : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Vector3 aimCoordinates;
    // Start is called before the first frame update
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = false;
        navMeshAgent.enabled = true;
        navMeshAgent.Warp(transform.position);
    }

    public void MakeCoordinates(Vector3 coordinates)
    {
        aimCoordinates = coordinates;
    }

    // Update is called once per frame
    void Update()
    {
        NavMeshHit hit;
        navMeshAgent.destination = aimCoordinates;
        if (NavMesh.SamplePosition(aimCoordinates, out hit, 5.0f, NavMesh.AllAreas))
        {
            Vector3 result = hit.position;
            Debug.Log(result);
        }

        Debug.DrawRay(transform.position, Vector3.up, Color.blue, 5.0f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Human_AI : MonoBehaviour
{
    [SerializeField] private Human_Manager hm = null; 
    [SerializeField] private Vector2 minSize = new Vector4();
    [SerializeField] private Vector2 maxSize = new Vector4();
    [SerializeField] private float distance = 2.5f;

    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        NewDestination();
    }

    private void FixedUpdate()
    {
        if (agent.remainingDistance < distance)
        {
            NewDestination();
        }
    }

    private void NewDestination()
    {
        float x = Mathf.Lerp(minSize.x, minSize.y, Random.value);
        float z = Mathf.Lerp(maxSize.x, maxSize.y, Random.value);

        agent.SetDestination(new Vector3(x, 1f, z) + hm.NavMeshPos);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*[RequireComponent(typeof(NavMeshAgent))]*/
public class Human_AI : MonoBehaviour
{
    //[SerializeField] private Transform Hammer = null;
    //[Space]
    [SerializeField] private Vector3 fieldZero = new Vector3();
    [SerializeField] private float fieldWith = 10f;
    [Space]
    [SerializeField] private float maxSpeed = 1f;
    [SerializeField] private float smoothTime = 1f;
    [SerializeField] private float stoppingDistance = 0.1f;

    private float targetPos;

    private float currentVelocity;

    private void Start()
    {
        transform.position = fieldZero + Vector3.right * (Random.value * 2f - 1f) * fieldWith;
        NewDestination();
    }

    private void Update()
    {
        transform.position = fieldZero + Vector3.right * Mathf.SmoothDamp(transform.position.x, fieldZero.x + targetPos, ref currentVelocity, smoothTime, maxSpeed, Time.deltaTime);
        if (Mathf.Abs(targetPos - transform.position.x) <= stoppingDistance)
        {
            NewDestination();
        }
    }

    private void NewDestination()
    {
        targetPos = (Random.value * 2f - 1f) * fieldWith;
    }

    public void Smashed()
    {
        //do the logic for when we are smashed
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(fieldZero + Vector3.left * fieldWith, fieldZero + Vector3.right * fieldWith);
        Gizmos.DrawWireSphere(fieldZero + Vector3.right * targetPos, 0.1f);
    }

    /*[SerializeField] private Human_Manager hm = null; 
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
    }*/
}

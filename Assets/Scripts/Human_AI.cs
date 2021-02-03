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
    [Space]
    [SerializeField] private float timeTillDestroyed = 1.5f;
    [Space]
    [Range(0f, 1f)]
    [SerializeField] private float ChangeToFreeze = 0.25f;



    private float targetPos;

    private float currentVelocity;

    private float scaredTimer = 0f;

    private void Start()
    {
        transform.position = fieldZero + Vector3.right * (Random.value * 2f - 1f) * fieldWith;
        NewDestination();
    }

    private void Update()
    {
        transform.position = fieldZero + Vector3.right * Mathf.SmoothDamp(transform.position.x, fieldZero.x + targetPos, ref currentVelocity, smoothTime, maxSpeed, Time.deltaTime);

        if (scaredTimer <= 0f)
        {
            if (Mathf.Abs(targetPos - transform.position.x) <= stoppingDistance)
            {
                NewDestination();
            }
        }
        else
        {
            scaredTimer -= Time.deltaTime;
            if (scaredTimer <= 0f)
                NewDestination();
        }
    }

    private void NewDestination()
    {
        targetPos = (Random.value * 2f - 1f) * fieldWith;
    }

    public void HammerSmash(float hitPos)
    {
        if (Random.value >= ChangeToFreeze)
        {
            if (hitPos < transform.position.x)
                targetPos = fieldWith;
            else targetPos = -fieldWith;
        }
        else targetPos = transform.position.x;

        scaredTimer = 1.5f;
    }

    public void Smashed()
    {
        //change sprite to puddle of blood
        scaredTimer = 999f; //just to make it stop moving
        StartCoroutine(Dying());
    }

    private IEnumerator Dying()
    {
        float timeLeft = timeTillDestroyed;
        while (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            yield return null;
        }

        GameManager.instance.DeleteHuman(this);

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

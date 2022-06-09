using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BeeAI : MonoBehaviour
{
    private Vector3 targetPos;
    private float targetHeight;
    public GameObject beeVis;
    private Rigidbody rb;
    private NavMeshAgent agent;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine("newHeight");
        StartCoroutine("newPos");
    }

    // Update is called once per frame
    void Update()
    {
        beeVis.transform.localPosition = Vector3.Lerp(beeVis.transform.localPosition, new Vector3(0f, targetHeight, 0f), Time.deltaTime * 1);
    }
    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask) {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;
        randomDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);
        return navHit.position;
    }

    IEnumerator newHeight() {
        yield return new WaitForSeconds(Random.Range(0,3));
        targetHeight = Random.Range(1f, 15f);
        StartCoroutine("newHeight");
    }

    IEnumerator newPos() {
        yield return new WaitForSeconds(Random.Range(0, 3));
        Vector3 newPos = RandomNavSphere(transform.position, 5f, -1);
        agent.SetDestination(newPos);
        StartCoroutine("newPos");
    }
}

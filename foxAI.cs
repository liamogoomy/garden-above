using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class foxAI : MonoBehaviour
{
    private Rigidbody rb;
    private NavMeshAgent agent;
    private bool wander = false;
    private bool moving = false;
    private bool turning = false;
    public GameObject foxVis; //holds the model and animations
    public GameObject trowelPickup; // where the fox leads the player
    private Vector3 targetPosition;
    public GameObject player;
    float distToPlayer;
    float distToTrowel;
    private bool activatedrandomwalk = false;
    private Animator m_Animator;

    void Start() {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        targetPosition = gameObject.transform.position;
        m_Animator = foxVis.GetComponent<Animator>();
    }
    private void Awake() {
    }

    // Update is called once per frame
    void Update() {
        if (agent.velocity.magnitude < 0.1) {
            moving = false;
            m_Animator.SetBool("moving", false);
        } else {
            moving = true;
            m_Animator.SetBool("moving", true);
        }
        if (wander == false) {
            distToPlayer = Vector3.Distance(player.transform.position, targetPosition);
            distToTrowel = Vector3.Distance(trowelPickup.transform.position, targetPosition);
            if (moving == false) {
                Vector3 dir = (player.transform.position - transform.position).normalized;
                Quaternion rotTo = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotTo, Time.deltaTime * 50f);
                m_Animator.SetBool("lookAtPlayer", true);
                m_Animator.SetBool("waitingForPlayer", true);
            } else {
                m_Animator.SetBool("lookAtPlayer", false);
                m_Animator.SetBool("waitingForPlayer", false);
            }
            if (distToPlayer < 2 && distToTrowel < 2) {
                wander = true;
            }
            if (distToPlayer < 2f && turning == false) {
                //move the target pos towards the trowel object
                Vector3 direction = (trowelPickup.transform.position - gameObject.transform.position).normalized;
                targetPosition += direction * Mathf.Min(4f, distToTrowel);
                NavMeshHit navHit;
                NavMesh.SamplePosition(targetPosition, out navHit, 500f, -1);
                agent.SetDestination(navHit.position);
                targetPosition = navHit.position;

            } else if (distToPlayer > 8f) {
                //move the target pos towards the player
                Vector3 direction = (player.transform.position - gameObject.transform.position).normalized;
                targetPosition += direction * Mathf.Min(8f, distToPlayer);
                NavMeshHit navHit;
                NavMesh.SamplePosition(targetPosition, out navHit, 500f, -1);
                agent.SetDestination(navHit.position);
                targetPosition = navHit.position;

            }
        } else {
            if (activatedrandomwalk == false) {
                m_Animator.SetBool("waitingForPlayer", false);
                StartCoroutine("wanderPos");
                activatedrandomwalk = true;
            }
            if (distToPlayer < 5 && moving == false) {
                Vector3 dir = (player.transform.position - transform.position).normalized;
                Quaternion rotTo = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotTo, Time.deltaTime * 50f);
            }
        }

    }
    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask) {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;
        randomDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);
        return navHit.position;
    }
    IEnumerator wanderPos() {
        yield return new WaitForSeconds(Random.Range(20, 60));
        Vector3 newPos = RandomNavSphere(transform.position, 80f, -1);
        agent.SetDestination(newPos);
        while (moving) {
            yield return null;
        }
        StartCoroutine("wanderPos");
    }
}


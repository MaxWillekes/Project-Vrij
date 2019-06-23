using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GuardStateMachine : MonoBehaviour
{
    public enum EnemyStates { Idle, Patrol, Alert }
    public EnemyStates state;

    public float alertRange = 10;
    public float speed = 3;

    public Transform[] patrolPositions;

    private GameObject[] coverObjects;
    private Transform currentPatrolTarget;
    private float waitTimer;
    private NavMeshAgent agent;


    // Start is called before the first frame update
    void Start()
    {
        coverObjects = GameObject.FindGameObjectsWithTag("Cover");
        agent = GetComponent<NavMeshAgent>();
        currentPatrolTarget = patrolPositions[Random.Range(0, patrolPositions.Length)];
        waitTimer = Random.Range(3, 5);
        state = EnemyStates.Patrol;
    }

    // Update is called once per frame
    void Update()
    {
        ExecuteState();
    }

    private void ExecuteState()
    {

        if (CheckPlayerInRange(alertRange))
        {
            state = EnemyStates.Alert;
        }

        switch (state)
        {
            case EnemyStates.Alert:
                AlertState();
                break;
            case EnemyStates.Patrol:
                PatrolState();
                break;
            case EnemyStates.Idle:
                IdleState();
                break;
        }
    }

    private bool CheckPlayerInRange(float range)
    {
        return Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position) < range;
    }

    private void IdleState()
    {
        waitTimer -= Time.deltaTime;
        if (waitTimer <= 0)
        {
            waitTimer = Random.Range(3, 5);
            currentPatrolTarget = patrolPositions[Random.Range(0, patrolPositions.Length)];
            SwitchState(EnemyStates.Patrol);
        }
    }
    private void AlertState()
    {
        Vector3 toPlayerDirection = transform.position + GameObject.FindGameObjectWithTag("Player").transform.position;
        //transform.rotation = Quaternion.LookRotation(awayFromPlayerDirection);
        //Vector3 AlertDestination = transform.position + awayFromPlayerDirection * 10;

        GameObject closestCoverObject = GetClosestCoverObject();

        MoveToTarget(GameObject.FindGameObjectWithTag("Player").transform.position);


        if (!CheckPlayerInRange(alertRange * 1.5f))
        {
            SwitchState(EnemyStates.Idle);
        }
    }

    private GameObject GetClosestCoverObject()
    {

        GameObject closestObject = null;
        float dist = Mathf.Infinity;
        foreach (GameObject obj in coverObjects)
        {

            float distanceToCoverObject = Vector3.Distance(transform.position, obj.transform.position);
            if (distanceToCoverObject < dist)
            {
                dist = distanceToCoverObject;
                closestObject = obj;
            }
        }
        return closestObject;

    }

    private void PatrolState()
    {

        if (Vector3.Distance(currentPatrolTarget.position, transform.position) < 1f)
        {
            SwitchState(EnemyStates.Idle);
        }
        MoveToTarget(currentPatrolTarget);

    }

    public void MoveToTarget(Transform target)
    {
        agent.SetDestination(target.transform.position);
    }
    public void MoveToTarget(Vector3 position)
    {
        agent.SetDestination(position);
    }

    public void SwitchState(EnemyStates newState)
    {

        state = newState;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>().Caught();
            other.GetComponent<PlayerMovement>().sprayRemaining = 0;
        }
    }
}

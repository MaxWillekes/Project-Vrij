using UnityEngine;
using UnityEngine.AI;

public class StateMachine : MonoBehaviour
{
    public enum EnemyStates { Idle, Patrol, Panic }
    public EnemyStates state;

    public float panicRange = 5;
    public float speed = 3;

    public Transform[] patrolPositions;

    private GameObject[] coverObjects;
    private Transform currentPatrolTarget;
    private float waitTimer;
    private GameObject player;
    private NavMeshAgent agent;

    public GameObject bMan;
    public Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {
        coverObjects = GameObject.FindGameObjectsWithTag("Cover");
        agent = GetComponent<NavMeshAgent>();
        currentPatrolTarget = patrolPositions[Random.Range(0, patrolPositions.Length)];
        player = GameObject.FindGameObjectWithTag("Player");
        waitTimer = Random.Range(3, 5);
        state = EnemyStates.Patrol;
        myAnim = bMan.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ExecuteState();
    }

    private void ExecuteState() {

        if (CheckPlayerInRange(panicRange)) {
            state = EnemyStates.Panic;
        }

        switch (state) {
            case EnemyStates.Panic:
                PanicState();
                break;
            case EnemyStates.Patrol:
                PatrolState();
                break;
            case EnemyStates.Idle:
                IdleState();
                break;
        }
    }

    private bool CheckPlayerInRange(float range) {
        return Vector3.Distance(player.transform.position, transform.position) < range;
    }

    private void IdleState() {

        myAnim.SetBool("isWalking",false);
        myAnim.SetBool("Panic", false);

        waitTimer -= Time.deltaTime;
        if(waitTimer <= 0) {
            waitTimer = Random.Range(3, 5);
            currentPatrolTarget = patrolPositions[Random.Range(0, patrolPositions.Length)];
            SwitchState(EnemyStates.Patrol);
        }
    }
    private void PanicState() {

        myAnim.SetBool("Panic", true);
        myAnim.SetBool("isWalking", false);
        
        Vector3 awayFromPlayerDirection = transform.position - player.transform.position;
        //transform.rotation = Quaternion.LookRotation(awayFromPlayerDirection);
        //Vector3 panicDestination = transform.position + awayFromPlayerDirection * 10;

        GameObject closestCoverObject = GetClosestCoverObject();

        MoveToTarget(closestCoverObject.transform.position + awayFromPlayerDirection.normalized * 3);


        if (!CheckPlayerInRange(panicRange * 1.5f)) {
            SwitchState(EnemyStates.Idle);
        }
    }

    private GameObject GetClosestCoverObject() {

        GameObject closestObject = null;
        float dist = Mathf.Infinity;
        foreach(GameObject obj in coverObjects) {

            float distanceToCoverObject = Vector3.Distance(transform.position, obj.transform.position);
            if(distanceToCoverObject < dist) {
                dist = distanceToCoverObject;
                closestObject = obj;
            }
        }
        return closestObject;

    }

    private void PatrolState() {

        myAnim.SetBool("isWalking", true);
        myAnim.SetBool("Panic", false);


        if (Vector3.Distance(currentPatrolTarget.position, transform.position) < 1f) {
            SwitchState(EnemyStates.Idle);
        }
        MoveToTarget(currentPatrolTarget);

    }

    public void MoveToTarget(Transform target) {
        //Vector3 targetDirection = target.transform.position - transform.position;
        //transform.rotation = Quaternion.LookRotation(targetDirection);
        //transform.position += transform.forward * speed * Time.deltaTime;
        agent.SetDestination(target.transform.position);
    }
    public void MoveToTarget(Vector3 position) {
        agent.SetDestination(position);
    }

    public void SwitchState(EnemyStates newState) {

        state = newState;
    }
}

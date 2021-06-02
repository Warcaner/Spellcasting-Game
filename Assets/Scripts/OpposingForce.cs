using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class OpposingForce : MonoBehaviour
{

    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float walkPointRange;
    public float attackInterval;
    bool alreadyAttacked;
    public float sightRange, attackRange;
    bool playerInSightRange, playerInAttackRange;

    /*private void Awake()
    {
        player = GameObject.Find("Construction Light Low").transform;
        agent = GetComponent<NavMeshAgent>();
        Debug.Log("enemy engaged");

    } */

    private void Patrolling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void Chasing()
    {
        agent.SetDestination(player.position);
        Debug.Log("The enemy is chasing the player.");
    }

    private void Attacking()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            Debug.Log("The enemy is attacking the player.");



            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), attackInterval);

        }

    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patrolling();
            Debug.Log("Patrolling engaged");
        }

        if (playerInSightRange && !playerInAttackRange)
        {
            Chasing();
            Debug.Log("Chasing engaged");
        }

        if (playerInSightRange && playerInAttackRange)
        {
            Attacking();
            Debug.Log("Attacking engaged");
        }
    }
}

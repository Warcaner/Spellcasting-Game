using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    [SerializeField] float stoppingDistance;

    [SerializeField] float damage;

    [SerializeField] float magicDamage1;

    [SerializeField] float magicDamage2;



    float lastAttackTime = 0;

    float attackCooldown = 2;

    NavMeshAgent agent;
    GameObject target;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindGameObjectWithTag("Player");

    }

    private void Update()
    {
        float dist = Vector3.Distance(transform.position, target.transform.position);
        if(dist < stoppingDistance)
        {
            StopEnemy();
            Attack();
            
        }

        else
        {
            GoToTarget();
        }
        
        
    }

    private void GoToTarget()
    {
        agent.isStopped = false;
        agent.SetDestination(target.transform.position);

    }
           

    private void StopEnemy()
    {
        agent.isStopped = true;

    }

    private void Attack()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;
            target.GetComponent<CharacterStats>().TakeDamage(damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {


        if (other.gameObject.CompareTag("MagicType1"))
        {
            GetComponent<CharacterStats>().TakeDamage(magicDamage1);


        }

        if (other.gameObject.CompareTag("MagicType2"))
        {
            GetComponent<CharacterStats>().TakeDamage(magicDamage2);

        }
    }

}

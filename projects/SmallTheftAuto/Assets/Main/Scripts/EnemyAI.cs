using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public float health;
    Animator animator;

    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatisGround;
    public LayerMask WhatisPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    public float timebetweenAttacks;
    public bool alreadyAttacked;


    public float sightRange;
    public float attackRange;
    public bool playerInSightRange;
    public bool PlayerInAttackRange;


    private void Awake()
    {
        player = GameObject.Find("Player_New").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        PlayerInAttackRange = Physics.CheckSphere(transform.position, attackRange, WhatisPlayer);
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, WhatisPlayer);

        if(!playerInSightRange && !PlayerInAttackRange)
        {
            Patroling();
            
        }
        if(playerInSightRange && !PlayerInAttackRange)
        {
            ChasePlayer();
        }
        if(PlayerInAttackRange && playerInSightRange)
        {
            AttackPlayer();
        }
    }

    private void Patroling()
    {
        animator.SetBool("Patrol", true);
        animator.SetBool("Chase", false);
        animator.SetBool("Attack", false);
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        else
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if(distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatisGround))
        {
            walkPointSet = true;
        }
    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        animator.SetBool("Patrol", false);
        animator.SetBool("Chase", true);
        animator.SetBool("Attack", false);
    }

    private void AttackPlayer()
    {
        animator.SetBool("Patrol", false);
        animator.SetBool("Chase", false);
        animator.SetBool("Attack", true);
        agent.SetDestination(transform.position);
        transform.LookAt(player.position);

        // need the attack function here
        // I could not find which sceipt is responsible for attacks and shootings


        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timebetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("Patrol", false);
        animator.SetBool("Chase", false);
        alreadyAttacked = false;
       
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Invoke(nameof(DestroyEnemy), 0.5f);
        }
    }

    private void DestroyEnemy()
    {

    }

}

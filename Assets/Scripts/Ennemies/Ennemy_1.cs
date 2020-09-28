using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

public class Ennemy_1 : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask WhatIsGround, WhatIsPlayer;

    //Patroling 

    public Vector3 walkPoint;
    public float   walkPointRange;
    bool           walkPointSet;

    //Attacking

    public float timeBetweenAttacks;
    bool         alreadyAttacked;

    //States

    public float sightRange, attackRange;
    public bool  playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Kara").transform;
        agent  = GetComponent<NavMeshAgent>();

    }

    private void Update()
    {
        //Check la portée et la vision
        playerInSightRange  = Physics.CheckSphere(transform.position, sightRange,  WhatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, WhatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange  && !playerInAttackRange) ChasePlayer();
        if (playerInSightRange  &&  playerInAttackRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint atteint

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        //Calcul d'un point aléatoire à portée

        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, WhatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //S'assurer que l'ennemi ne bouge pas

        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //Attack code here

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AvatarFollow : MonoBehaviour
{
    public Transform player;       // Assign the XR Rig (Player) in Inspector
    public float followDistance = 2f;  // Max distance before the avatar starts moving
    public float moveSpeed = 2f;       // Speed of movement
    public Animator animator;          // Animator component

    private NavMeshAgent agent;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > followDistance)
        {
            agent.SetDestination(player.position);
            animator.SetBool("isWalking", true); // Trigger walk animation
            Debug.Log("The avatar is walking!");
        }
        else
        {
            agent.SetDestination(transform.position);
            animator.SetBool("isWalking", false); // Idle when close enough
            Debug.Log("The avatar is not walking!");
        }
    }
}

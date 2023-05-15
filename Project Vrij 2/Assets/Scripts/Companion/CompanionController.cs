using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

namespace Invector.vCharacterController.AI
{
    public class CompanionController : MonoBehaviour
    {
        public Transform playerTransform;   //the player's transform
        public float followDistance = 5.0f;  //the distance at which the companion should start following the player
        public float stoppingDistance = 2.0f; //the distance at which the companion should stop following the player
        public float moveSpeed = 5.0f;       //the speed at which the companion should move

        private NavMeshAgent navMeshAgent;  //the NavMeshAgent component
        private bool isFollowing = false;   //flag to indicate if the companion is following the player

        void Start()
        {
            //get the NavMeshAgent component
            navMeshAgent = GetComponent<NavMeshAgent>();

            //set the speed of the NavMeshAgent
            navMeshAgent.speed = moveSpeed;
        }

        void Update()
        {
            //check if the player is within range
            if (!isFollowing && Vector3.Distance(transform.position, playerTransform.position) <= followDistance)
            {
                isFollowing = true;
            }

            //if the companion is following the player, set the NavMeshAgent's destination
            if (isFollowing)
            {
                navMeshAgent.SetDestination(playerTransform.position);

                //stop following the player if the companion is close enough
                if (Vector3.Distance(transform.position, playerTransform.position) <= stoppingDistance)
                {
                    navMeshAgent.isStopped = true;
                }
                else
                {
                    navMeshAgent.isStopped = false;
                }
            }
        }
    }
}

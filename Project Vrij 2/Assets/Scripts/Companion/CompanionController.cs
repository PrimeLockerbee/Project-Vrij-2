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
        public float stunRange = 5.0f;       //the range at which enemies can be stunned
        public float stunDuration = 70.0f;    //the duration of the stun effect
        public Image cooldownImage;          //the image to use for the cooldown visual representation

        private NavMeshAgent navMeshAgent;  //the NavMeshAgent component
        private bool isFollowing = false;   //flag to indicate if the companion is following the player
        private bool isCooldown = false;    //flag to indicate if the stun ability is on cooldown
        public float cooldownTime = 15.0f;  //the remaining time on the stun ability cooldown

        void Start()
        {
            //get the NavMeshAgent component
            navMeshAgent = GetComponent<NavMeshAgent>();

            //set the speed of the NavMeshAgent
            navMeshAgent.speed = moveSpeed;

            //initialize the cooldown image
            cooldownImage.fillAmount = 0f;
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

                //update the cooldown image fill amount
                if (isCooldown)
                {
                    float fillAmount = cooldownTime / 15;
                    cooldownImage.fillAmount = fillAmount;
                    cooldownTime -= Time.deltaTime;

                    if (cooldownTime <= 0.0f)
                    {
                        isCooldown = false;
                        cooldownImage.fillAmount = 0.0f;
                        cooldownTime = 15.0f;
                    }
                }

                //check for input to stun nearest enemy
                if (!isCooldown && Input.GetKeyDown(KeyCode.P))
                {
                    //find the nearest enemy within stun range
                    Collider[] colliders = Physics.OverlapSphere(transform.position, stunRange, LayerMask.GetMask("Enemy"));
                    if (colliders.Length > 0)
                    {
                        Collider nearestCollider = colliders[0];
                        float nearestDistance = Vector3.Distance(transform.position, nearestCollider.transform.position);
                        foreach (Collider collider in colliders)
                        {
                            float distance = Vector3.Distance(transform.position, collider.transform.position);
                            if (distance < nearestDistance)
                            {
                                nearestDistance = distance;
                                nearestCollider = collider;
                            }
                        }


                        //disable the NavMeshAgent of the nearest enemy for the stun duration
                        v_AIController enemyNavMeshAgent = nearestCollider.GetComponent<v_AIController>();
                        if (enemyNavMeshAgent != null)
                        {
                            enemyNavMeshAgent.patrolSpeed = 0.0f;
                            enemyNavMeshAgent.wanderSpeed = 0.0f;
                            enemyNavMeshAgent.chaseSpeed = 0.0f;
                            enemyNavMeshAgent.strafeSpeed = 0.0f;
                            StartCoroutine(EnableNavMeshAgent(enemyNavMeshAgent, stunDuration));
                        }

                        //start the cooldown timer
                        isCooldown = true;
                    }
                }
            }
        }

        IEnumerator EnableNavMeshAgent(v_AIController agent, float delay)
        {
            Debug.Log("Stun works");
            yield return new WaitForSeconds(delay);
            agent.patrolSpeed = 0.5f;
            agent.wanderSpeed = 0.5f;
            agent.chaseSpeed = 1.0f;
            agent.strafeSpeed = 1.0f;

        }
    }
}

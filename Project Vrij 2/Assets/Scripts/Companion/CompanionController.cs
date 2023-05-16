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

        public GameObject stunVFXPrefab;
        GameObject stunVFX;
        vControlAIMelee enemyNavMeshAgent;

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
                        enemyNavMeshAgent = nearestCollider.GetComponent<vControlAIMelee>();
                        if (enemyNavMeshAgent != null)
                        {
                            enemyNavMeshAgent.stopMove = true;

                            stunVFX = Instantiate(stunVFXPrefab, nearestCollider.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                            StartCoroutine(EnableNavMeshAgent(enemyNavMeshAgent, stunDuration, stunVFX));
                        }

                        //start the cooldown timer
                        isCooldown = true;
                        stunVFX.transform.position = enemyNavMeshAgent.gameObject.transform.position + new Vector3(0, 1, 0);
                    }
                }
            }
        }

        private IEnumerator EnableNavMeshAgent(vControlAIMelee agent, float delay, GameObject stunVFX)
        {
            yield return new WaitForSeconds(delay);

            agent.stopMove = false;

            Destroy(stunVFX);
        }
    }
}

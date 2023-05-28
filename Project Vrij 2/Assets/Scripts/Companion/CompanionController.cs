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

        //Stun
        private bool isStunCooldown = false;    //flag to indicate if the stun ability is on cooldown
        public float stunCooldownTime = 15.0f;  //the remaining time on the stun ability cooldown
        public GameObject stunVFXPrefab;
        GameObject stunVFX;
        vControlAIMelee enemyNavMeshAgent;
        public float stunRange = 5.0f;       //the range at which enemies can be stunned
        public float stunDuration = 7.0f;    //the duration of the stun effect
        public Image stunCooldownImage;          //the image to use for the cooldown visual representation

        //Shield
        private bool isShieldCooldown = false;    //flag to indicate if the shield ability is on cooldown
        public float shieldCooldownTime = 20.0f;  //the remaining time on the shield ability cooldown
        public GameObject shieldVFXPrefab;
        GameObject shieldVFX;
        public Transform shieldSpawn; 
        public float shieldDuration = 5.0f;    //the duration of the stun effect
        public Image shieldCooldownImage;          //the image to use for the cooldown visual representation

        void Start()
        {
            //get the NavMeshAgent component
            navMeshAgent = GetComponent<NavMeshAgent>();

            //set the speed of the NavMeshAgent
            navMeshAgent.speed = moveSpeed;

            //initialize the cooldown image
            stunCooldownImage.fillAmount = 0f;

            //initialize the cooldown image
            shieldCooldownImage.fillAmount = 0f;
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

                StunEffect();
                ShieldEffect();
            }
        }

        public void StunEffect()
        {
            //update the cooldown image fill amount
            if (isStunCooldown)
            {
                float fillAmount = stunCooldownTime / 15;
                stunCooldownImage.fillAmount = fillAmount;
                stunCooldownTime -= Time.deltaTime;

                if (stunCooldownTime <= 0.0f)
                {
                    isStunCooldown = false;
                    stunCooldownImage.fillAmount = 0.0f;
                    stunCooldownTime = 15.0f;
                }
            }

            //check for input to stun nearest enemy
            if (!isStunCooldown && Input.GetKeyDown(KeyCode.O))
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
                    isStunCooldown = true;
                    stunVFX.transform.position = enemyNavMeshAgent.gameObject.transform.position + new Vector3(0, 1, 0);
                }
            }
        }

        public void ShieldEffect()
        {
            //update the cooldown image fill amount
            if (isShieldCooldown)
            {
                float fillAmount = shieldCooldownTime / 15;
                shieldCooldownImage.fillAmount = fillAmount;
                shieldCooldownTime -= Time.deltaTime;

                if (shieldCooldownTime <= 0.0f)
                {
                    isShieldCooldown = false;
                    shieldCooldownImage.fillAmount = 0.0f;
                    shieldCooldownTime = 15.0f;
                }
            }

            //check for input to shield player
            if (!isShieldCooldown && Input.GetKeyDown(KeyCode.P))
            {
                shieldVFX = Instantiate(shieldVFXPrefab, shieldSpawn.position + new Vector3(0, 1, 0), Quaternion.identity);

                //start the cooldown timer
                isShieldCooldown = true;
            }

            if(shieldVFX != null)
            {
                shieldVFX.transform.position = shieldSpawn.position;
                shieldVFX.transform.rotation = shieldSpawn.rotation;
            }
            
            Destroy(shieldVFX, shieldDuration);
        }

        private IEnumerator EnableNavMeshAgent(vControlAIMelee agent, float delay, GameObject stunVFX)
        {
            yield return new WaitForSeconds(delay);

            agent.stopMove = false;

            Destroy(stunVFX);
        }
    }
}

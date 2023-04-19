using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerIndex = 1; // Player index for cooperative play
    public float moveSpeed = 5.0f;
    public float rotationSpeed = 10.0f;
    public float jumpForce = 10.0f;

    // Melee attack parameters
    public Transform meleeAttackPoint;
    public float meleeAttackRange = 1.0f;
    public LayerMask meleeAttackLayer;
    public int meleeAttackDamage = 10;

    // Ranged attack parameters
    public GameObject meleeWeapon;
    public GameObject rangedWeapon;
    public Transform rangedAttackPoint;
    public float rangedAttackForce = 20.0f;
    public int rangedAttackDamage = 5;

    private CharacterController characterController;
    private Camera mainCamera;
    private Vector3 moveDirection;
    private bool isGrounded;
    private bool isMeleeWeaponEquipped = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        meleeWeapon.SetActive(true);
        rangedWeapon.SetActive(false);
    }

    void Update()
    {
        //Get input based on player index
        string horizontalInput = "Horizontal" + playerIndex;
        string verticalInput = "Vertical" + playerIndex;
        string jumpInput = "Jump" + playerIndex;
        string attackInput = "Attack" + playerIndex;
        string switchWeaponInput = "SwitchWeapon" + playerIndex;

        float horizontal = Input.GetAxis(horizontalInput);
        float vertical = Input.GetAxis(verticalInput);

        Vector3 moveDirection = (transform.TransformDirection(Vector3.forward) * vertical) + (transform.TransformDirection(Vector3.right) * horizontal);
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

        //Rotate player to face the same direction as the camera
        Vector3 playerDirection = new Vector3(mainCamera.transform.forward.x, 0f, mainCamera.transform.forward.z);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(playerDirection), rotationSpeed * Time.deltaTime);

        if (characterController.isGrounded)
        {
            isGrounded = true;
            if (Input.GetButtonDown(jumpInput))
            {
                Jump();
            }
        }

        if (Input.GetButtonDown(attackInput))
        {
            if (isMeleeWeaponEquipped)
            {
                MeleeAttack();
            }
            else
            {
                RangedAttack();
            }
        }

        if (Input.GetButtonDown(switchWeaponInput))
        {
            SwitchWeapon();
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            moveDirection.y = jumpForce;
            isGrounded = false;
        }
    }

    void MeleeAttack()
    {
        Collider[] hitColliders = Physics.OverlapSphere(meleeAttackPoint.position, meleeAttackRange, meleeAttackLayer);
        foreach (Collider collider in hitColliders)
        {
            // Apply melee attack damage to the hit objects
            collider.SendMessage("TakeDamage", meleeAttackDamage, SendMessageOptions.DontRequireReceiver);
        }
    }

    void RangedAttack()
    {
        GameObject projectile = Instantiate(rangedWeapon, rangedAttackPoint.position, rangedAttackPoint.rotation);
        Rigidbody projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.velocity = rangedAttackPoint.forward * rangedAttackForce;

        //projectileController.SetDamage(rangedAttackDamage);
    }

    void SwitchWeapon()
    {
        if (isMeleeWeaponEquipped)
        {
            isMeleeWeaponEquipped = false;
            meleeWeapon.SetActive(false);
            rangedWeapon.SetActive(true);
        }
        else
        {
            isMeleeWeaponEquipped = true;
            meleeWeapon.SetActive(true);
            rangedWeapon.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool isGrounded;
    [SerializeField] private float gravity;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float jumpHeight;
    [SerializeField] public float jumpCooldown;

    private Vector3 moveDirection;
    private Vector3 velocity;

    private CharacterController controller;
    private Animator anim;

    private float lastTimeGrounded;

    // Start is called before the first frame update
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2.5f;
        }

        float moveZ = Input.GetAxis("Vertical");
        float moveX = Input.GetAxis("Horizontal");

        Vector3 inputDirection = new Vector3(moveX, 0, moveZ).normalized;

        if (inputDirection != Vector3.zero)
        {
            // Calculer la rotation pour faire face à la direction du déplacement
            Quaternion toRotation = Quaternion.LookRotation(inputDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 8f);
        }

        // Utiliser la direction avant du transform pour le déplacement
        Vector3 moveDirection = transform.forward * inputDirection.magnitude;

        if (moveDirection != Vector3.zero)
        {
            moveDirection *= moveSpeed;
            anim.SetFloat("Speed",1f);
        }
        else if (moveDirection == Vector3.zero)
        {
            anim.SetFloat("Speed", 0f);
        }

        if (isGrounded && Time.time - lastTimeGrounded > jumpCooldown)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            anim.ResetTrigger("Jump");
        }

        controller.Move(moveDirection * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        // Réduire la vélocité horizontale pendant le saut en fonction de la vitesse de marche
        anim.SetTrigger("Jump");
        //float reducedSpeed = isRunning ? moveSpeed : moveSpeed * 0.1f;
        velocity.x = moveDirection.x * 0.5f;
        velocity.z = moveDirection.z * 0.5f;
        lastTimeGrounded = Time.time;
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
    }
}

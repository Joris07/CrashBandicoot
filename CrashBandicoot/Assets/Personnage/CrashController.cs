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

    private Vector3 moveDirection;
    private Vector3 velocity;

    private CharacterController controller;
    private Animator anim;

    private bool isRunning;

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

            // Vérifier si le personnage court pour ajuster l'animation
            isRunning = Input.GetKey(KeyCode.LeftShift);
        }

        // Utiliser la direction avant du transform pour le déplacement
        Vector3 moveDirection = transform.forward * inputDirection.magnitude;

        if (moveDirection != Vector3.zero)
        {
            moveDirection *= isRunning ? moveSpeed * 2f : moveSpeed;
            anim.SetFloat("Speed", isRunning ? 1.5f : 0.5f, 0.1f, Time.deltaTime);
        }
        else if (moveDirection == Vector3.zero)
        {
            anim.SetFloat("Speed", 0f, 0.1f, Time.deltaTime);
        }

        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }

        controller.Move(moveDirection * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        // Réduire la vélocité horizontale pendant le saut en fonction de la vitesse de marche
        float reducedSpeed = isRunning ? moveSpeed : moveSpeed * 0.1f;
        velocity.x = moveDirection.x * reducedSpeed;
        velocity.z = moveDirection.z * reducedSpeed;

        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);

        anim.SetFloat("Speed", 1f);
    }
}

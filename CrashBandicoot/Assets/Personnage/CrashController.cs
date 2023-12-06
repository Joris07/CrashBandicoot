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
    [SerializeField] private AudioClip attackSound;

    private Vector3 moveDirection;
    private Vector3 velocity;

    private CharacterController controller;
    private Animator anim;
    private AudioSource audioSource;

    private float lastTimeGrounded;
    private bool isRotating;

    // Start is called before the first frame update
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();
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
            Quaternion toRotation = Quaternion.LookRotation(inputDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 8f);
        }

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
        
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(Attack());
        }
        else
        {
            //Invoke("DisableRotation", 2f);
            anim.ResetTrigger("Attack");
        }

        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space) && Time.time - lastTimeGrounded > jumpCooldown)
            {
                Jump();
            }
        }
        else
        {
            anim.ResetTrigger("Jump");
        }

        // anim.ResetTrigger("Attack");
        controller.Move(moveDirection * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        // Réduire la vélocité horizontale pendant le saut en fonction de la vitesse de marche
        anim.SetTrigger("Jump");
        velocity.x = moveDirection.x * 0.5f;
        velocity.z = moveDirection.z * 0.5f;
        lastTimeGrounded = Time.time;
        velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
    }

    private IEnumerator Attack()
    {
        isRotating = true;
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 1);
        anim.SetTrigger("Attack");

        if (attackSound != null)
        {
            audioSource.PlayOneShot(attackSound);
        }

        yield return new WaitForSeconds(0.9f);
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 0);
        isRotating = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "box_low(Clone)" && isRotating)
        {
            other.gameObject.GetComponent<Collider>().enabled = false;
            other.gameObject.GetComponent<ParticleSystem>().Play();
            other.gameObject.GetComponent<MeshRenderer>().enabled = false;
            Destroy(other.gameObject, 1);
        }
    }
}

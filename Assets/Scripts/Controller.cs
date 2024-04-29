using System;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private AudioSource steps;
    public static bool isMoving;
    private CharacterController controller;
    private Animator animator;
    private float speed;
    private float turnSmoothTime;
    private float turnSmoothVelocity;
   
    private void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        speed = 1.5f;
        turnSmoothTime = 0.1f;
        isMoving = false;
        steps = GameObject.FindGameObjectWithTag("Steps").GetComponent<AudioSource>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (transform.position.y <= 0.5f)
        {
            animator.SetBool("IsJumping", false);
        }

        if (direction != Vector3.zero)
        {
            isMoving = true;
            steps.Play();
            animator.SetBool("IsWalking", true);

        } else 
        {
            isMoving = false;
            steps.Pause();
            animator.SetBool("IsWalking", false);
        }

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            controller.Move(direction * speed * Time.deltaTime);
        }

        /* if (Input.GetButtonDown("Jump") && transform.position.y <= 0.5f)
        {
            animator.SetBool("IsJumping", true);
            velocity.y = jump;
        } else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);*/
    }
}

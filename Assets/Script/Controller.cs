using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class Controller : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Rigidbody rb;
    private Vector3 vector;
    public float turnSmoothTime = 0.1f;
    float turnSmoothValocity;
    /*public Animator animator;*/

    [Header("Move Speed")]
    public float speed = 6f;
    public float runspeed = 12f;

    [Header("Dash")]
    public const float maxDashTime = 1.0f;
    public float dashDistance = 10;
    public float dashStoppingSpeed = 0.1f;
    float currentDashTime = maxDashTime;
    public float dashSpeed = 8f;
    public float dashCooldown = 3f;
    public float dashTimer = 0f;
    //public ParticleSystem DashEffect;

    [Header("Gravity and Jump")]
    private float verticalVelocity;
    public float gravity = 9.81f;
    public float jumpForce = 4f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //DashEffect.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothValocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            //move
            controller.Move(moveDir.normalized * speed * Time.deltaTime);

            //run
            if (Input.GetKey(KeyCode.LeftShift))
            {
                controller.Move(moveDir.normalized * runspeed * Time.deltaTime);
            }
        }

        //jump
        Vector3 vec = Vector3.zero;
        vec.y = verticalVelocity;
        controller.Move(vec * Time.deltaTime);

        if (controller.isGrounded)
        {
            verticalVelocity -= gravity * Time.deltaTime;
            if (Input.GetKey(KeyCode.Space))
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        //dash
        if (dashTimer <= 0 && Input.GetKeyDown(KeyCode.F))
        {
            currentDashTime = 0;
            dashTimer = dashCooldown;
            //Instantiate(DashEffect, transform.position, Quaternion.identity);
        }
        else
        {
            dashTimer -= Time.deltaTime;
        }

        if (currentDashTime < maxDashTime)
        {
            vector = transform.forward * dashDistance;
            currentDashTime += dashStoppingSpeed;
        }
        else
        {
            vector = Vector3.zero;
        }
        controller.Move(vector * Time.deltaTime * dashSpeed);
    }
}


using System;
using System.Collections.Generic;
using Cinemachine;
using KBCore.Refs;
using Unity.Mathematics;
using UnityEngine;
using Utilities;


namespace Platformer
{
    public class PlayerController : ValidatedMonoBehaviour
    {
        [Header("References")]
        [SerializeField, Self] Rigidbody rb;
        [SerializeField, Self] GroundChecker groundChecker;
        [SerializeField, Self] Animator animator;
        [SerializeField, Anywhere] CinemachineFreeLook freeLookCam;
        [SerializeField, Anywhere] InputReader input;

        [Header("Movement Settings")]
        [SerializeField] float moveSpeed = 6f;
        [SerializeField] float rotationSpeed = 15f;
        [SerializeField] float smoothTime = 0.2f;

        [Header("Jump Settings")]
        
        [SerializeField] float jumpForce = 10f;
        [SerializeField] float jumpDuration = 0.5f;
        [SerializeField] float jumpCooldown = 0f;
        [SerializeField] float jumpMaxHeight = 2f;
        [SerializeField] float gravityMultiplier = 3f;

        Transform mainCam;

        const float ZeroF = 0f;

        float currentSpeed;
        float velocity;
        [SerializeField] float jumpVelocity;

        Vector3 movement;

        List<Timer> timers;

        CountdownTimer jumpTimer;
        CountdownTimer jumpCooldownTimer;

        //Animator parameters
        static readonly int Speed = Animator.StringToHash("Speed");

        void Awake()
        {
            mainCam = Camera.main.transform;
            freeLookCam.Follow = transform;
            freeLookCam.LookAt = transform;
            freeLookCam.OnTargetObjectWarped(transform, transform.position - freeLookCam.transform.position -  Vector3.forward);
            rb.freezeRotation = true;

            jumpTimer = new CountdownTimer(jumpDuration);
            jumpCooldownTimer = new CountdownTimer(jumpCooldown);
            timers = new List<Timer>(2) {jumpTimer, jumpCooldownTimer};

            jumpTimer.OnTimerStop += () => jumpCooldownTimer.Start();
        }

        void Start()
        {
            input.EnablePlayerActions();
        }

        void OnEnable()
        {
            input.Jump += OnJump;
        }

        void OnDisable()
        {
            input.Jump -= OnJump;
        }
        void Update()
        {
            movement =  new Vector3(input.Direction.x, 0f, input.Direction.y);

            HandleTimers();
            UpdateAnimator();
        }


        void FixedUpdate()
        {
            HandleJump();
            HandleMovement();
        }


        private void UpdateAnimator()
        {
            animator.SetFloat(Speed,currentSpeed);
        }

        private void HandleTimers()
        {
            foreach (var timer in timers)
            {
                timer.Tick(Time.deltaTime);
            }
        }

        private void HandleJump()
        {
            //not jumping & grounded velocity = 0
            if (!jumpTimer.IsRunning && groundChecker.IsGrounded)
            {
                jumpVelocity = ZeroF;
                jumpTimer.Stop();
                return;
            }

            //calculate velocity por jump or fall
            if (jumpTimer.IsRunning)
            {
                float launchPoint = 0.9f;
                if (jumpTimer.Progress > launchPoint)
                {
                    //calculate velocity to reach de jump height
                    jumpVelocity = Mathf.Sqrt(2*jumpMaxHeight * MathF.Abs(Physics.gravity.y));
                }
                else
                {
                    //gradually  reduce the upward force
                    jumpVelocity += (1 - jumpTimer.Progress) * jumpForce * Time.fixedDeltaTime;
                }
            }
            else
            {
                //gravity  pull down
                jumpVelocity = Physics.gravity.y * gravityMultiplier * Time.fixedDeltaTime;
            }

            //Apply velocity

            rb.velocity = new Vector3(rb.velocity.x, jumpVelocity, rb.velocity.z);
        }

        private void HandleMovement()
        {

            var adjustedDirection = Quaternion.AngleAxis(mainCam.eulerAngles.y, Vector3.up) * movement;
            if (adjustedDirection.magnitude > ZeroF)
            {
                HandleRotation(adjustedDirection);
                HandleHorizontalMovement(adjustedDirection);
                SmoothSpeed(adjustedDirection.magnitude);
            }
            else
            {
                SmoothSpeed(ZeroF);

                //reset horizontal velocity for a snappy stop
                rb.velocity = new Vector3(ZeroF, rb.velocity.y, ZeroF);
            }
        }

        private void HandleHorizontalMovement(Vector3 adjustedDirection)
        {
            //move Player
            Vector3 velocity = adjustedDirection * (moveSpeed * Time.fixedDeltaTime);
            rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
        }

        private void HandleRotation(Vector3 adjustedDirection)
        {
            var targetRotation = Quaternion.LookRotation(adjustedDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.LookAt(transform.position + adjustedDirection);
        }

        private void SmoothSpeed(float value)
        {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, value, ref velocity, smoothTime);
        }

        void OnJump(bool performed)
        {
            if (performed && !jumpTimer.IsRunning && !jumpCooldownTimer.IsRunning && groundChecker.IsGrounded)
            {
                jumpTimer.Start();
            }
            else if (!performed && jumpTimer.IsRunning)
            {
                jumpTimer.Stop();
            }
        }
    }
}

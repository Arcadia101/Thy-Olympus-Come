using System;
using UnityEngine;
using Utilities;

namespace Platformer
{
    public class PlayerDetector : MonoBehaviour
    {
        [SerializeField, Range(0f,360f)] private float detectionAngle = 60f; // Cone in front of enemy.
        [SerializeField, Range(0f, 25f)] private float detectionRadius = 10f; // Large circle araund the enemy.
        [SerializeField, Range(0f, 25f)] private float innerDetectionRadius = 5f; // Small circle araund the enemy.
        [SerializeField, Range(0f, 5f)] private float detectionCooldown = 1f; // Time between detections.
        
        public Transform player { get; private set; }
        private CountdownTimer detectionTimer;

        private IDetectionStrategy detectionStrategy;

        private void Start()
        {
            detectionTimer = new CountdownTimer(detectionCooldown);
            player = GameObject.FindGameObjectWithTag("Player").transform; // Make sure to TAG the player!
            detectionStrategy = new ConeDetectionStrategy(detectionAngle, detectionRadius, innerDetectionRadius);
        }

        private void Update() => detectionTimer.Tick(Time.deltaTime);

        public bool CanDetectPlayer()
        {
            return detectionTimer.IsRunning || detectionStrategy.Execute(player, transform, detectionTimer);
        }

        public void SetDetectionStrategy(IDetectionStrategy detectionStrategy) =>
            this.detectionStrategy = detectionStrategy;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            
            // draw a spheres for de radius
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
            Gizmos.DrawWireSphere(transform.position, innerDetectionRadius);
            
            //calculate cone directions
            Vector3 forwardConeDirection =
                Quaternion.Euler(0, detectionAngle / 2, 0) * transform.forward * detectionRadius;
            Vector3 backwardConeDirection =
                Quaternion.Euler(0, -detectionAngle / 2, 0) * transform.forward * detectionRadius;
            
            //draw lines to represent cone
            Gizmos.DrawLine(transform.position,transform.position + forwardConeDirection);
            Gizmos.DrawLine(transform.position,transform.position + backwardConeDirection);
        }
    }
}
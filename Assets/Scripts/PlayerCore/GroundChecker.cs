using UnityEngine;

namespace Platformer
{
    public class GroundChecker : MonoBehaviour
    {
        [SerializeField] float groundDistance = 0.08f;
        [SerializeField] LayerMask groundLayers;

        public bool IsGrounded{get; private set; }

        void Update()
        {
            IsGrounded = Physics.CheckSphere(transform.position, groundDistance, groundLayers);
        }
        void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, groundDistance);
            
            if (IsGrounded) {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundDistance);
            }
        }
    }
}

using UnityEngine;

namespace Platformer
{
    public class Ether : Entity
    {
        [SerializeField] private float EtherValue = 10f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<PlayerEther>().IncrementEther(EtherValue);
                Destroy(gameObject);
            }
        }
    }
}
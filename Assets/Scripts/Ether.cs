using UnityEngine;

namespace Platformer
{
    public class Ether : Entity
    {
        [SerializeField] private float EtherValue = 10f;
        [SerializeField] private FloatEventChannel etherChanel;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                etherChanel.Invoke(EtherValue);
                Destroy(gameObject);
            }
        }
    }
}
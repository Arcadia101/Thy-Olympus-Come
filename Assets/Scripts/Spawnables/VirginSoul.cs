using UnityEngine;

namespace Platformer
{
    public class VirginSoul : Entity
    {
        [SerializeField] private int SoulsValue = 1;
        [SerializeField] private IntEventChannel virginSoulsChanel;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                virginSoulsChanel.Invoke(SoulsValue);
                Destroy(gameObject);
            }
        }
    }
}
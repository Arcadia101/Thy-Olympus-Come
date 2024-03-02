using System;
using UnityEngine;

namespace Platformer
{
    public class PlayerEther : MonoBehaviour
    {
        [SerializeField] private float maxEther = 100;
        [SerializeField] private FloatEventChannel playerEtherChannel;

        private float currentEther;

        public bool IsFull => currentEther >= maxEther;
        public bool IsEmpty => currentEther <= 0f;

        private void Awake()
        {
            currentEther = 0f;
        }

        private void Start()
        {
            PublishEtherPercentage();
        }

        public void IncrementEther(float ether)
        {
            if (IsFull)
            {
                currentEther = maxEther;
            }
            else
            {
                currentEther += ether;
            }
            PublishEtherPercentage();
        }
        public void DecreaseEther(float ether)
        {
            if (IsEmpty)
            {
                currentEther = 0f;
            }
            else
            {
                currentEther -= ether;
            }
            PublishEtherPercentage();
        }

        private void PublishEtherPercentage()
        {
            if (playerEtherChannel != null)
                playerEtherChannel.Invoke(currentEther / maxEther);
        }
    }
}
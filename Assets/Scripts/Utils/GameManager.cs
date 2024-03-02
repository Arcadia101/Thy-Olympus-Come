using System;
using UnityEngine;

namespace Platformer
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public int VirginsSoulsCollected { get; private set; }
        public GameObject player { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void AddVirginSouls(int souls)
        {
            VirginsSoulsCollected += souls;
        }
        
        public void AddEther(int ether)
        {
            if (player == null)
            {
                player = GameObject.FindWithTag("Player");
            }
            player.GetComponent<PlayerEther>().IncrementEther(ether);
        }
        
    }
}
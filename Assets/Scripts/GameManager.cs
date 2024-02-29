﻿using System;
using UnityEngine;

namespace Platformer
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public int VirginsSoulsCollected { get; private set; }

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
        
    }
}
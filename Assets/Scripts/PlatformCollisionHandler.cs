﻿using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Platformer
{
    public class PlatformCollisionHandler : MonoBehaviour
    {
        private Transform platform;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("MovingPlatform"))
            {
                //if de contact normal is pointing up, we've collider with the top of the platform
                ContactPoint contact = other.GetContact(0);
                if (contact.normal.y < 0.5f) return;
                
                platform = other.transform;
                transform.SetParent(platform);
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.CompareTag("MovingPlatform"))
            {
                transform.SetParent(null);
                platform = null;
            }
        }
    }
}
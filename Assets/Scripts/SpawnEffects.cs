using System;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

namespace Platformer
{
    [RequireComponent(typeof(AudioSource))]
    public class SpawnEffects : MonoBehaviour
    {
        [SerializeField] private GameObject spawnVFX;
        [SerializeField] private float animationDuration = 1f;

        private void Start()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, animationDuration).SetEase(Ease.OutBack);

            if (spawnVFX != null)
            {
                Instantiate(spawnVFX, transform.position, quaternion.identity);
            }
            
            GetComponent<AudioSource>().Play();
        }
    }
}
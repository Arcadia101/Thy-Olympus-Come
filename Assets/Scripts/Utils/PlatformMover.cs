using System;
using UnityEngine;
using DG.Tweening;

namespace Platformer
{
    public class PlatformMover : MonoBehaviour
    {
        [SerializeField] Vector3 moveTo = Vector3.zero;
        [SerializeField] private float moveTime = 1f;
        [SerializeField] private Ease ease = Ease.InOutQuad;

        private Vector3 startPosition;

        private void Start()
        {
            startPosition = transform.position;
            move();
        }

        private void move()
        {
            transform.DOMove(startPosition + moveTo, moveTime).SetEase(ease).SetLoops(-1, LoopType.Yoyo);
        }
    }
}
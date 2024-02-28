using System;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.AI;

namespace Platformer
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : Entity
    {
        [SerializeField, Self] private NavMeshAgent agent;
        [SerializeField, Child] private Animator animator;

        private StateMachine stateMachine;

        private void OnValidate() => this.ValidateRefs();

        private void Start()
        {
            stateMachine = new StateMachine();
        }
        
        void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
        void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);

        private void Update()
        {
            stateMachine.Update();
        }

        private void FixedUpdate()
        {
            stateMachine.FixedUpdate();
        }
    }
}
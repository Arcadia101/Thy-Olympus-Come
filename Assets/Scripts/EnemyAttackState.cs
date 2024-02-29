using UnityEngine;
using UnityEngine.AI;

namespace Platformer
{
    public class EnemyAttackState: EnemyBaseState
    {
        private readonly NavMeshAgent agent;
        private readonly Transform player;

        public EnemyAttackState(Enemy enemy, Animator animator, NavMeshAgent agent, Transform player) : base(enemy, animator)
        {
            this.agent = agent;
            this.player = player;
        }

        public override void OnEnter()
        {
            Debug.Log("attack");
            if (enemy.IsRanged)
                animator.CrossFade(RangedAttackHash, crossFadeDuration);
            else if (!enemy.IsRanged)
                animator.CrossFade(AttackHash, crossFadeDuration);
        }

        public override void Update()
        {
            if (enemy.IsRanged)
                agent.SetDestination(enemy.transform.position);
            else if (!enemy.IsRanged)
                agent.SetDestination(player.position);
            
            enemy.Attack();
            
        }
    }
}
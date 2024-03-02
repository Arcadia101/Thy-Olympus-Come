using UnityEngine;

namespace Platformer
{
    public class SkillState : BaseState
    {
        public SkillState(PlayerController player, Animator animator) : base(player, animator)
        {
        }

        public override void OnEnter()
        {
            animator.CrossFade(SkillHash, crossFadeDuration);
            player.Skill();
        }

        public override void FixedUpdate()
        {
            player.HandleMovement();
        }
    }
}
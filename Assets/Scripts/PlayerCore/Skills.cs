using System;
using KBCore.Refs;
using UnityEngine;

namespace Platformer
{
    public enum Skill
    {
        SpearAttack,
        AreaAttack,
        KairosStrike,
        TimeSlow
    }
    public class Skills : MonoBehaviour
    {
        [SerializeField, Self] private PlayerController playerController;
        [SerializeField, Self] private Animator animator;
        [SerializeField] private Skill currentSkill;
        [SerializeField] private Skill defaultSkill;
        public Skill publicCurrentSkill;

        private float sAttack;
        private float aAttack;
        private float kStrike;
        private float tSlow;
        
        //Animator parameters
        static readonly int Spear = Animator.StringToHash("SpearAttack");
        static readonly int Area = Animator.StringToHash("AoEAttack");
        static readonly int Strike = Animator.StringToHash("KairosStrike");
        static readonly int Slow = Animator.StringToHash("TimeSlow");
        
        /*private void Awake()
        {
            SetDefaultSkill();
        }*/

        private void Update()
        {
            publicCurrentSkill = currentSkill;
            SetSkillAnimation();
            UpdateAnimator();
        }

        public void SetDefaultSkillAsCurrent()
        {
            currentSkill = defaultSkill;
        }

        public void SetDefaultSkill(Skill skill)
        {
            defaultSkill = skill;
        }

        public void UseSkill()
        {
            switch (currentSkill)
            {
                case Skill.SpearAttack:
                    SpearAttack();
                    break;
                case Skill.AreaAttack:
                    AreaAttack();
                    break;
                case Skill.KairosStrike:
                    KairosStrike();
                    break;
                case Skill.TimeSlow:
                    TimeSlow();
                    break;
            }
        }

        private void SpearAttack()
        {
            Debug.Log("SpearAttack");
        }

        private void AreaAttack()
        {
            Debug.Log("AreaAttack");
        }

        private void KairosStrike()
        {
            Debug.Log("KairosStrike");
        }

        private void TimeSlow()
        {
            Debug.Log("TimeSlow");
        }

        private void SetSkillAnimation()
        {
            switch (currentSkill)
            {
                case Skill.SpearAttack:
                    sAttack = 1f;
                    aAttack = 0f;
                    kStrike = 0f;
                    tSlow = 0f;
                    break;
                case Skill.AreaAttack:
                    sAttack = 0f;
                    aAttack = 1f;
                    kStrike = 0f;
                    tSlow = 0f;
                    break;
                case Skill.KairosStrike:
                    sAttack = 0f;
                    aAttack = 0f;
                    kStrike = 1f;
                    tSlow = 0f;
                    break;
                case Skill.TimeSlow:
                    sAttack = 0f;
                    aAttack = 0f;
                    kStrike = 0f;
                    tSlow = 1f;
                    break;
            }
        }
        private void UpdateAnimator()
        {
            animator.SetFloat(Spear,sAttack);
            animator.SetFloat(Area, aAttack);
            animator.SetFloat(Strike,kStrike);
            animator.SetFloat(Slow, tSlow);
        }
    }
}
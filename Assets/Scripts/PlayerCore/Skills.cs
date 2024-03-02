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
        [SerializeField] private Skill currentSkill;
        [SerializeField] private Skill defaultSkill;
        
        /*private void Awake()
        {
            SetDefaultSkill();
        }*/

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
    }
}
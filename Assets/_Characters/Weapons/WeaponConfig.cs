using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters 
{
    [CreateAssetMenu(menuName = ("RPG/Weapon"))]
    public class WeaponConfig : ScriptableObject
    {

        public Transform gripTransfrom;

        [SerializeField] GameObject weaponPrefab;
        [SerializeField] AnimationClip attackAnimation;
        [SerializeField] float timeBetweenAnimationCycle = .5f;
        [SerializeField] float maxAttackRange = 2f;
        [SerializeField] float additionalDamage = 10f;
        [SerializeField] float damageDelay = .5f;

        //获取最小攻击间隔
        public float GetTimeBetweenAnimationCycle()
        {
            return timeBetweenAnimationCycle;
        }

        //获取最大攻击范围
        public float GetMaxAttackRange()
        {
            return maxAttackRange;
        }

        public float GetDamageDelay()
        {
            return damageDelay;
        }

        //获取武器预制体
        public GameObject GetWeaponPrefab()
        {
            return weaponPrefab;
        }

        //获取动画
        public AnimationClip GetAttackAnimClip()
        {
            RemoveAnimationEvents();
            return attackAnimation;
        }

        //获取额外的伤害（武器，技能）
        public float GetAdditionalDamage()
        {
            return additionalDamage;
        }

        //避免资源包崩溃
        private void RemoveAnimationEvents()
        {
            attackAnimation.events = new AnimationEvent[0];
        }
    }
}


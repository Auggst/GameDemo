using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
    public class PowerAttackBehaviour : AbilityBehaviour
    {
        public override void Use(GameObject target)
        {
            //音效
            PlayAbilitySound();
            //处理伤害
            DealDamage(target);
            //粒子效果
            PlayParticleEffect();
            PlayAbilityAnimation();
        }
        
        //伤害函数
        private void DealDamage(GameObject target)
        {
            float damageToDeal =(config as PowerAttackConfig).GetExtraDamage();
            target.GetComponent<HealthSystem>().TakeDamage(damageToDeal);
        }

    }

}

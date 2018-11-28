using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Characters;
using RPG.Core;
using System;

public class AreaEffectBehaviour : AbilityBehaviour{

    //使用范围技能的执行函数
    public override void Use(GameObject target)
    {
        //处理音效
        PlayAbilitySound();
        //处理伤害
        DealRadiaDamage();
        //处理粒子特效
        PlayParticleEffect();
        PlayAbilityAnimation();
    }

    /*
    * 函数:DealRadiaDamage
    * 功能:处理范围伤害
    * 参数:无
    * 类型:privat void
    */
    private void DealRadiaDamage()
    {
        RaycastHit[] hits = Physics.SphereCastAll(
            transform.position,
            (config as AreaEffectConfig).GetRadius(),
            Vector3.up,
            (config as AreaEffectConfig).GetRadius()
            ); //从transform.position向四周发射射线，范围半径为config.GetRadius(),扫描的方向，扫描的最大长度

        foreach (RaycastHit hit in hits) //批处理碰撞物体
        {
            var damageable = hit.collider.gameObject.GetComponent<HealthSystem>();
            bool hitPlayer = hit.collider.gameObject.GetComponent<PlayerControl>();
            if (damageable != null && !hitPlayer)
            {
                float damageToDeal = (config as AreaEffectConfig).GetDamageToEachTarget();
                damageable.TakeDamage(damageToDeal);
            }
        }
    }
}

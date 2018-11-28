using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Characters
{
    [CreateAssetMenu(menuName = ("RPG/Special  Abiltiy/Area Effect"))]
    public class AreaEffectConfig : AbilityConfig
    {
        [Header("Area Effect Specific")]
        [SerializeField] float radius = 5f;
        [SerializeField] float damageToEachTarget = 15f;

        public override AbilityBehaviour  GetBehaviourComponent(GameObject gameObjectToattachTo)
        {
           return  gameObjectToattachTo.AddComponent<AreaEffectBehaviour>();
        }

        //返回额外伤害damageToEachTarget
        public  float GetDamageToEachTarget()
        {
            return damageToEachTarget;
        }

        //返回半径radius
        public float GetRadius()
        {
            return radius;
        }
    }
}


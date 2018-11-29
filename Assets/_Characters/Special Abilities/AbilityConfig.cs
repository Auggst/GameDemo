
using UnityEngine;

namespace RPG.Characters
{

    public abstract class AbilityConfig : ScriptableObject 
    {
        [Header("Special Ability General")]
        [SerializeField] float energyCost = 10f;
        [SerializeField] GameObject particlePerfab = null;
        [SerializeField] AnimationClip animation;
        [SerializeField] AudioClip[] audioClips = null;
        [SerializeField] GameObject gameObject = null;


        protected AbilityBehaviour behaviour;
         
        public abstract AbilityBehaviour  GetBehaviourComponent(GameObject gameObjectToAttachTo);

        public void AttachAbilityTo(GameObject objectToattachTo)
        {
            AbilityBehaviour behaviourComponent = GetBehaviourComponent(objectToattachTo);
            behaviourComponent.SetConfig(this);
            behaviour = behaviourComponent;
        }

        public void Use(GameObject target)
		{
            behaviour.Use(target);
        }

        //获取能量消耗
        public float GetEnergyCost()
        {
            return energyCost;
        }

        //获取粒子特效预设
        public GameObject GetParticlePrefab()
        {
            return particlePerfab; ;
        }

        public AnimationClip GetAbilityAnimation()
        {
            return animation;
        }

        //获取音频
        public AudioClip GetRandomAbilitySound()
        {
            return audioClips[Random.Range(0,audioClips.Length)];
        }
    }


}

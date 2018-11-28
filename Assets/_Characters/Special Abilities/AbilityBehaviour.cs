using System.Collections;
using UnityEngine;

namespace RPG.Characters
{
    public abstract  class AbilityBehaviour : MonoBehaviour
    {
        protected AbilityConfig config;

        const string ATTACK_TRIGGER = "Attack";
        const string DEFAULT_ATTACK_STATE = "DEFAULT ATTACK";
        const float PARTICE_CLEAN_UP_DELAY = 20f;

        public abstract void Use(GameObject target = null);

        //设置配置脚本
        public void SetConfig(AbilityConfig configToSet)
        {
            config = configToSet;
        }

        /*
        * 函数:PlayParticleEffect
        * 功能:播放技能粒子动画
        * 参数:无
        * 类型:protect void
        */
        protected void PlayParticleEffect()
        {
            var particlePrefab = config.GetParticlePrefab();
            var particleObject = Instantiate(
               particlePrefab, 
                transform.position, 
                particlePrefab.transform.rotation
            );
            particleObject.transform.parent = transform; //如果需要就在预制体中设为世界坐标系
            particleObject.GetComponent<ParticleSystem>().Play();
            StartCoroutine(DestroyParticleWhenFinished(particleObject));
        }

        IEnumerator DestroyParticleWhenFinished(GameObject particlePrefab)
        {
            while (particlePrefab.GetComponent<ParticleSystem>().isPlaying)
            {
                yield return new WaitForSeconds(PARTICE_CLEAN_UP_DELAY);
            }
            Destroy(particlePrefab);
            yield return new WaitForEndOfFrame();//等到该帧结束
        }

        protected void PlayAbilityAnimation()
        {
            var abilityAnimation = config.GetAbilityAnimation();
            var animatorOverrideController = GetComponent<Character>().GetOverrideController();
            var animator = GetComponent<Animator>();
            animator.runtimeAnimatorController = animatorOverrideController;
            animatorOverrideController[DEFAULT_ATTACK_STATE] = config.GetAbilityAnimation();
            animator.SetTrigger(ATTACK_TRIGGER);
            
        }

        protected void PlayAbilitySound()
        {
            var abilitySound = config.GetRandomAbilitySound();//TODO 改为随机音频
            var audioSource = GetComponent<AudioSource>();
            audioSource.PlayOneShot(abilitySound);
        }

    }
}


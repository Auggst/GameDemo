using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace RPG.Characters
{
    public class HealthSystem : MonoBehaviour
    {

        [SerializeField] float maxHealthPoints = 100f;
        [SerializeField] Image healthBar;
        [SerializeField] AudioClip[] damageSounds;
        [SerializeField] AudioClip[] deathSound;
        [SerializeField] float deathVanishSeconds = 2.0f;

        const string DEATH_TRIGGER = "Death";

        public float currentHealthPoints = 0;
        Animator animator;
        AudioSource audioSource = null;
        Character characterMovement;

        public float healthAsPercentage
        {
            get
            {
                return currentHealthPoints / maxHealthPoints;
            }
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
            audioSource = GetComponent<AudioSource>();
            characterMovement = GetComponent<Character>();

            currentHealthPoints = maxHealthPoints;
        }

        private void Update()
        {
            UpdateHealthBar();
        }

        void UpdateHealthBar()
        {
            if(healthBar)
            {
                healthBar.fillAmount = healthAsPercentage;
            }
        }

        public void Heal(float points)
        {
            currentHealthPoints = Mathf.Clamp(currentHealthPoints + points, 0f, maxHealthPoints);
        }

        /*
        * 函数:TakeDamage
        * 功能:受到伤害，生命值低于0，毁灭物体
        * 参数:float damage,伤害值
        * 类型:public void
        */
        public void TakeDamage(float damage)
        {
            bool characterDies = (currentHealthPoints - damage <= 0);
            currentHealthPoints = Mathf.Clamp(currentHealthPoints - damage, 0f, maxHealthPoints);
            var clip = damageSounds[UnityEngine.Random.Range(0, damageSounds.Length)];
            audioSource.PlayOneShot(clip);
            if (characterDies)
            {
                StartCoroutine(KillCharacter());
            }
        }

        //死亡协程
        IEnumerator KillCharacter()
        {
            characterMovement.Kill();
            //死亡动画
            animator.SetTrigger(DEATH_TRIGGER);

            //死亡音效
            audioSource.clip = deathSound[UnityEngine.Random.Range(0, deathSound.Length)];
            audioSource.Play(); //覆盖现有的声音
            yield return new WaitForSecondsRealtime(audioSource.clip.length);

            var playerComponent = GetComponent<PlayerControl>();
            if(playerComponent && playerComponent.isActiveAndEnabled)
            {
                //重新加载游戏
                SceneManager.LoadScene(0);
            }
            else
            {
                DestroyObject(gameObject,deathVanishSeconds);
            }
        }
    }
}


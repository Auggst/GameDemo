using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.CameraUI;
using System;

namespace RPG.Characters
{
    public class SpecialAbilities : MonoBehaviour
    {
        [SerializeField] AbilityConfig[] abilities;
        [SerializeField] Image energyBar = null;
        [SerializeField] float maxEnergyPoints = 100f;
        [Tooltip("能量每秒回复值")] //提示信息
        [SerializeField] float regenPointsPerSecond = 1f; //每秒的回复值
        [SerializeField] AudioClip outOfEnergy;

        float currentEnergyPoints;
        AudioSource audioSource = null;

        float energyAsPercent { get { return currentEnergyPoints / maxEnergyPoints; } }


        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            currentEnergyPoints = maxEnergyPoints;
            AttachInitialAbilities();
            UpdateEnergyBar();
        }

        private void Update()
        {
            if(currentEnergyPoints < maxEnergyPoints)
            {
                //添加能量
                AddEnergyPoint();
                //更新能量显示
                UpdateEnergyBar();
            }
        }

        //批量添加技能
        private void AttachInitialAbilities()
        {
            for (int abilityIndex = 0; abilityIndex < abilities.Length; abilityIndex++)
            {
				abilities[abilityIndex].AttachAbilityTo(gameObject);
            }
        }


        /*
        * 函数:AttemptSpecialAbility
        * 功能:配置技能，并使用技能
        * 参数:int abilityIndex，所需技能的索引值
        * 类型:public void
        */
        public void AttemptSpecialAbility(int abilityIndex,GameObject target = null)
        {
            var energyComponent = GetComponent<SpecialAbilities>();
            var energyCost = abilities[abilityIndex].GetEnergyCost();

            if (energyCost <= currentEnergyPoints)
            {
                ConsumeEnergy(energyCost);
				if (target == null) 
				{
					target = GameObject.FindWithTag ("Player");
				}
                abilities[abilityIndex].Use(target);
                
            }
            else
            {
                audioSource.PlayOneShot(outOfEnergy);
            }
        }

        public int GetNumberOfAbilities()
        {
            return abilities.Length;
        }

        //能量回复函数
        private void AddEnergyPoint()
        {
            var pointsToAdd = regenPointsPerSecond * Time.deltaTime;
            currentEnergyPoints = Mathf.Clamp(currentEnergyPoints + pointsToAdd, 0, maxEnergyPoints);
        }


        //能量消耗函数，消耗amount点能量，并更新能量条显示
        public void ConsumeEnergy(float amount)
        {
            float newEnergyPoints = currentEnergyPoints - amount;
            currentEnergyPoints = Mathf.Clamp(newEnergyPoints, 0, maxEnergyPoints);
            UpdateEnergyBar();
        }


        private void UpdateEnergyBar()
        {
            if(energyBar)
            {
                energyBar.fillAmount = energyAsPercent;
            }
        }
    }

}

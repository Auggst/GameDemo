using System;
using UnityEngine;
using UnityEngine.AI;
using RPG.CameraUI;

namespace RPG.Characters
{   [SelectionBase]
    public class Character : MonoBehaviour
    {
        [Header("Animator Settings")]
        [SerializeField] RuntimeAnimatorController animatorController;
        [SerializeField] AnimatorOverrideController animatorOverrideController;
        [SerializeField] Avatar characterAvater;
        [SerializeField] [Range(.1f,1f)]float animatorForwardCap = 1f;

        [Header("Audio Source Settings")]
        [SerializeField] float audioSourceSpatialBlend = 0.5f;

        [Header("Capsule Collider Settings")]
        [SerializeField] Vector3 colliderCenter = new Vector3(0f,0.96f,0f);
        [SerializeField] float colliderRadius = 0.3f;
        [SerializeField] float colliderHeight = 2.1f;

        [Header("Movement Settings")]
        [SerializeField] float stoppingDistance = 1.5f;
        [SerializeField] float moveSpeedMultiplier = .7f;
        [SerializeField] float animationSpeedMultiplier = 1.5f;
        [SerializeField] float moveTurnSpeed = 360f;
        [SerializeField] float stationaryTurnSpeed = 180f;
        [SerializeField] float moveThresshold = 1f;

        [Header("Nav Mesh Agent Seetings")]
        [SerializeField] float navMeshAgentSteeringSpeed = 1.0f;
        [SerializeField] float navMeshAgentStoppingDistance = 1.0f;

        Vector3 clickPoint;
        NavMeshAgent navMeshAgent;
        Animator animator;
        Rigidbody m_Rigidbody;
        float forwardAmount;
        float turnAmount;
        bool isAlive = true;

        private void Awake()
        {
            AddRequiredComponents();
        }

        private void AddRequiredComponents()
        {
            var capsuleCollider = gameObject.AddComponent<CapsuleCollider>();
            capsuleCollider.center = colliderCenter;
            capsuleCollider.radius = colliderRadius;
            capsuleCollider.height = colliderHeight;

            m_Rigidbody = gameObject.AddComponent<Rigidbody>();
            m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;

            var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = audioSourceSpatialBlend;

            animator = gameObject.AddComponent<Animator>();
            animator.runtimeAnimatorController = animatorController;
            animator.avatar = characterAvater;

            navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
            navMeshAgent.speed = navMeshAgentSteeringSpeed;
            navMeshAgent.stoppingDistance = navMeshAgentStoppingDistance;
            navMeshAgent.autoBraking = false;
            navMeshAgent.updateRotation = false;
            navMeshAgent.updatePosition = true;
        }

        public float GetAnimSpeedMultiplier()
        {
            return animator.speed;
        }

        private void Update()
        {
            if(navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance && isAlive)
            {
                Move(navMeshAgent.desiredVelocity);
            }
            else
            {
                Move(Vector3.zero);
            }
        }

        public void Kill()
        {
            isAlive = false;
        }

        public void SetDestination(Vector3 worldPos)
        {
            navMeshAgent.destination = worldPos;
        }

        public AnimatorOverrideController GetOverrideController()
        {
            return animatorOverrideController;
        }

        public void Move(Vector3 movement)
        {
            SetForwardAndTurn(movement);
            ApplyExtraTurnRotation();
            UpdateAnimator();
        }

        void SetForwardAndTurn(Vector3 movement)
        {
            if (movement.magnitude > moveThresshold)
            {
                movement.Normalize();
            }
            var localMove = transform.InverseTransformDirection(movement);
            turnAmount = Mathf.Atan2(localMove.x, localMove.z);
            forwardAmount = localMove.z;
        }


        void UpdateAnimator()
        {
            // update the animator parameters
            animator.SetFloat("Forward", forwardAmount * animatorForwardCap, 0.1f, Time.deltaTime);
            animator.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);
            animator.speed = animationSpeedMultiplier;
        }

        void ApplyExtraTurnRotation()
        {
            // help the character turn faster (this is in addition to root rotation in the animation)
            float turnSpeed = Mathf.Lerp(stationaryTurnSpeed, moveTurnSpeed, forwardAmount);
            transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0);
        }

        /*
        * 函数:OnMouseOverTerrain
        * 功能:观察者监听的方法，如果点击鼠标左键就移动到目的地位置
        * 参数:Vector3 destination,鼠标点击目的地
        * 类型:void
        */
        void OnMouseOverTerrrain(Vector3 destination)
        {
            if(Input.GetMouseButton(0))
            {
                navMeshAgent.SetDestination(destination);//自动寻路到目的地
            }
        }

        /*
        * 函数:OnMouseOverEnemy
        * 功能:鼠标点击敌人，向敌人移动
        * 参数:EnemyAI enemy,鼠标点击的敌人
        * 类型:void
        */
        //TODO 移到玩家控制器
        void OnMouseOverEnemy(EnemyAI enemy)
        {
            if(Input.GetMouseButton(0)|| Input.GetMouseButtonDown(1))
            {
                navMeshAgent.SetDestination(enemy.transform.position);//自动寻路至敌人坐标
            }
        }

        void OnAnimatorMove()
        {
            if(Time.deltaTime > 0)
            {
                Vector3 velocity = (animator.deltaPosition * moveSpeedMultiplier) / Time.deltaTime;
                velocity.y = m_Rigidbody.velocity.y;
                m_Rigidbody.velocity = velocity;
            }
        }

    }
}




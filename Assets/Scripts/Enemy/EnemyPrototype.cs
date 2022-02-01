using System;
using System.Collections;
using System.IO;
using DG.Tweening;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(SpriteRenderer))]
    public abstract class EnemyPrototype : MonoBehaviour
    {
        [SerializeField] protected float health;
        [SerializeField] protected float damage;
        [SerializeField] protected float attackRange;
        [SerializeField] protected float speed;
        [SerializeField] protected float followOffset;

        private PlayerController _playerController;
        protected Animator Animator;
        private Coroutine _startAttackRoutine;
        private float direction;
        protected abstract bool IsAngry { get; set; }
        protected bool IsRunning {get; private set; }
        protected bool IsEnemyDead{get ; private set;}
        
        protected SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void OnValidate()
        {
            _playerController ??= FindObjectOfType<PlayerController>();
            
        }
        

        protected void FollowPlayer()
        {
            
            if (_playerController.transform.position.x < transform.position.x)
            {
                direction = -1;
                spriteRenderer.flipX = true;
            }
            else
            {
                direction = 1;
                spriteRenderer.flipX = false;

            }
            if (Vector3.Distance(transform.position, _playerController.gameObject.transform.position) >
                GetFollowOffset())
            {
                transform.position = new Vector2(transform.position.x + (speed * direction * Time.deltaTime),
                    transform.position.y);

               if (IsRunning) return;
                
                IsRunning = true;

                Debug.Log("running");
            }
            else
            {
                if (!IsRunning) return;

                IsRunning = false;
                Debug.Log("idle");
            }

            if (IsAngry)
            {
                TryAttack();
            }
        }

        private float GetFollowOffset()
        {
            return !IsAngry ? followOffset : attackRange ;
        }

        protected bool TryAttack()
        {
            if (Vector3.Distance(transform.position, _playerController.gameObject.transform.position) <
                attackRange)
            {
                if (_startAttackRoutine != null) return false;
                _startAttackRoutine = StartCoroutine(StartAttack());
                return true;
            }

            if (Vector3.Distance(transform.position, _playerController.gameObject.transform.position) >
                attackRange)
            {
                StopCoroutine(StartAttack());
                return false;
               _startAttackRoutine = null;
                
            }

            return true;

        }

        IEnumerator StartAttack()
        {
           yield return new WaitUntil(() => Animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.5f);
           //yield return new WaitForSeconds(1);
           //Animator.SetBool(IsAttack, true);
            _playerController.ChangeHealth(-damage);
            _startAttackRoutine = null;
        }

        public void TakeDamage(float playerDamage)
        {
            health -= playerDamage;
            Animator.SetTrigger("takeDamage");
            if (health <= 0)
            {
                IsEnemyDead = true;
                StopCoroutine(StartAttack());
            }
        }
        

       


    }
}
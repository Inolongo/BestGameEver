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
        [SerializeField] protected float attackSpeed;
        protected readonly int IsAttack = Animator.StringToHash("IsAttack");

        protected PlayerController _playerController;
        protected Animator Animator;
        private Coroutine _startAttackRoutine;
        private float direction;
        protected abstract bool IsAngry { get; set; }
        protected bool IsRunning {get; private set; }
        public bool IsEnemyDead{get ; private set;}
        
        protected SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            Animator = GetComponent<Animator>();
        }

        private void OnValidate()
        {
            _playerController ??= FindObjectOfType<PlayerController>();

        }

        public void TakeDamage(float playerDamage)
        {
            health -= playerDamage;
            Debug.Log("poluchai fascist" + health)
                ;
            Animator.SetTrigger("takeDamage");
            
            if (health <= 0)
            {
                IsEnemyDead = true;
                StopCoroutine(StartAttack());
            }
        }

        protected void SetPLayerDamage(float damage)
        {
            _playerController.ChangeHealth(-damage);
        }

        protected void FollowPlayer()
        {
            
            if (_playerController.transform.position.x < transform.position.x)
            {
                direction = -1;
                spriteRenderer.flipX = false;
            }
            else
            {
                direction = 1;
                spriteRenderer.flipX = true;

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

        protected void TryAttack()
        
        {
            if (Vector3.Distance(transform.position, _playerController.gameObject.transform.position) <
                attackRange)
            {
                if (_startAttackRoutine != null) return;
                Animator.SetBool(IsAttack, false);

                _startAttackRoutine = StartCoroutine(StartAttack());
                Debug.Log("pizdyat aa");
            }

            if (Vector3.Distance(transform.position, _playerController.gameObject.transform.position) >
                attackRange)
            {
                Animator.SetBool(IsAttack, false);
                StopCoroutine(StartAttack());
                _startAttackRoutine = null;
                
            }
        }

        private float GetFollowOffset()
        {
            return !IsAngry ? followOffset : attackRange ;
        }

        protected IEnumerator StartAttack()
        {
            yield return new WaitForSeconds(attackSpeed);
           Animator.SetBool(IsAttack, true);
           SetPLayerDamage(damage);
           _startAttackRoutine = null;
        }
    }
}
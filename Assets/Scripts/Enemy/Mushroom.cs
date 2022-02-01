using System.Collections;
using DefaultNamespace;
using UnityEngine;

namespace Enemy
{
    public class Mushroom : EnemyPrototype
    {
        private static readonly int IsRun = Animator.StringToHash("isRun");
        private static readonly int IsAttack = Animator.StringToHash("IsAttack");
        private static readonly int IsDead = Animator.StringToHash("IsDead");
        protected override bool IsAngry { get; set; }

        [SerializeField] private FirstMushroomDialog firstMushroomDialog;
        [SerializeField] private SecondMushroomDialog secondMushroomDialog;


        private void Start()
        {
            firstMushroomDialog ??= FindObjectOfType<FirstMushroomDialog>();
            secondMushroomDialog ??= FindObjectOfType<SecondMushroomDialog>();
            Animator = GetComponent<Animator>();

        }

        private void Update()
        {
            if (firstMushroomDialog.IsFollowPlayer || secondMushroomDialog.IsFollowPlayer)
            {
                if (firstMushroomDialog.IsFighting || secondMushroomDialog.IsFighting)
                {
                    IsAngry = true;
                    if (TryAttack())
                    {
                        Animator.SetBool(IsAttack, true);
                    }
                    else
                    {
                        Animator.SetBool(IsAttack, false);
                        
                    }
                }
                FollowPlayer();
                Animator.SetBool(IsRun, IsRunning);

            }

            if (IsEnemyDead)
            {
                DiedOfCringe();
            }

            if (secondMushroomDialog.IsNeedless || firstMushroomDialog.IsNeedless)
            {
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            }
        }

        private void DiedOfCringe()
        {
            Animator.SetBool(IsDead, IsEnemyDead);
            Destroy(GetComponent<BoxCollider2D>());
            Destroy(GetComponent<Rigidbody2D>());
            Destroy(this);

        }

        
    }
}
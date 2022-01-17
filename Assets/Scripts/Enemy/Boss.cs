using Enemy;
using UnityEngine;

namespace DefaultNamespace
{
    public class Boss: EnemyPrototype
    {
        protected override bool IsAngry { get; set; }
        [SerializeField] private BossDialog bossDialog;
        
        private static readonly int IsRun = Animator.StringToHash("isRun");
        private static readonly int IsAttack = Animator.StringToHash("IsAttack");
        private static readonly int IsDead = Animator.StringToHash("IsDead");

        void Start()
        {
            
        }

        void Update()
        {
            if (!IsRunning) return;
            if (bossDialog.IsFighting)
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

            if (bossDialog.IsFirstAttack)
            {
                
            }
                
            FollowPlayer();
            Animator.SetBool(IsRun, IsRunning);
        }


        private void PissAttack()
        {
            
        }
        
    }
}
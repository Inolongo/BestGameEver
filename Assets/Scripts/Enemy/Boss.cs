using System;
using Enemy;
using UnityEngine;

namespace DefaultNamespace
{
    public class Boss: EnemyPrototype
    {
        protected override bool IsAngry { get; set; }
        [SerializeField] private BossDialog bossDialog;
        [SerializeField] private ParticleSystem pissParticle;
        
        private static readonly int IsRun = Animator.StringToHash("isRun");
        private static readonly int IsAttack = Animator.StringToHash("IsAttack");
        private static readonly int IsDead = Animator.StringToHash("IsDead");
        
        void Start()
        {
            bossDialog.SuperAttackStarted += SuperPissAttack;
        }

        void Update()
        {
            if (bossDialog.IsFollowPlayer)
            {
                if (bossDialog.IsFirstAttack)
                {
                }
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
                
                FollowPlayer();
                Animator.SetBool(IsRun, IsRunning);
            }
            
        }


        private void SuperPissAttack()
        {
            Instantiate(pissParticle, new Vector3(90, 0, 0), pissParticle.transform.rotation);
            
            pissParticle.Play();
            pissParticle.transform.Rotate(new Vector3(20, 0, 0) * Time.deltaTime);
            bossDialog.SuperAttackStarted -= SuperPissAttack;

        }
        
    }
}
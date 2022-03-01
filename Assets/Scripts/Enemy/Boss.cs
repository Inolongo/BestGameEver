using System;
using System.Collections.Generic;
using System.Linq;
using Enemy;
using UnityEngine;

namespace DefaultNamespace
{
    public class Boss : EnemyPrototype
    {
        protected override bool IsAngry { get; set; }
        [SerializeField] private BossDialog bossDialog;
        [SerializeField] private SuperPissAttack pissParticlePrefab;

        private static readonly int IsRun = Animator.StringToHash("isRun");
        private static readonly int IsAttack = Animator.StringToHash("IsAttack");
        private static readonly int IsDead = Animator.StringToHash("IsDead");
        


        private void Start()
        {
            Animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (bossDialog.IsFollowPlayer)
            {
                if (bossDialog.IsFirstAttack)
                {
                    bossDialog.SuperAttackStarted += SuperPissAttack;
                    bossDialog.IsFirstAttack = false;
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

                // FollowPlayer();
                // Animator.SetBool(IsRun, IsRunning);
            }
        }


        private void SuperPissAttack()
        {
            var superPissAttack = Instantiate(pissParticlePrefab, new Vector3(90, 1, 0),
                pissParticlePrefab.transform.rotation);
            superPissAttack.OnPlayerDamage += SetPLayerDamage;
            superPissAttack.Play();
        }
    }
}
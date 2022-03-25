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
        [SerializeField] private Vector3 pissAttackOffset;
        
        private static readonly int IsRun = Animator.StringToHash("IsRun");
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
                IsAngry = true;
    
                if (bossDialog.IsFighting)
                {
                    TryAttack();
                }
                
                if (IsEnemyDead)
                {
                    DiedOfCringe();
                }
                
                if (!FindObjectOfType<SuperPissAttack>())
                {
                    FollowPlayer();
                    Animator.SetBool(IsRun, IsRunning);
                }
                else
                {
                    Animator.SetBool(IsRun,false);
                }
            }
        }


        private void SuperPissAttack()
        {
            var playerCoord = _playerController.transform.position;
                var superPissAttack = Instantiate(pissParticlePrefab, playerCoord + pissAttackOffset,
                pissParticlePrefab.transform.rotation);
            superPissAttack.OnPlayerDamage += SetPLayerDamage;
            superPissAttack.Play();
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
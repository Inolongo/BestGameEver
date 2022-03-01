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

        private PlayerController _playerController;
        private static readonly int IsRun = Animator.StringToHash("IsRun");
        private static readonly int IsAttack = Animator.StringToHash("IsAttack");
        private static readonly int IsDead = Animator.StringToHash("IsDead");
        


        private void Start()
        {
            Animator = GetComponent<Animator>();
            _playerController ??= FindObjectOfType<PlayerController>();
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
                    
                    if (TryAttack())
                    {
                        Animator.SetBool(IsAttack, true);
                        Debug.Log("boss pizdit player");
                    }
                    else
                    {
                        Animator.SetBool(IsAttack, false);
                    }
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
    }
}
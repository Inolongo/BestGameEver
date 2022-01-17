using System;
using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class BossDialog : DialogReactions
    {
        private PlayerController _playerController;
        [SerializeField] private ParticleSystem pissAttack;
        public bool IsFighting { get; private set; }
        
        public bool IsFirstAttack { get; private set; }
        public bool IsFollowPlayer { get; private set; }

        private bool _reactionStarted;
        
        public Action SuperAttackStarted;


        private void OnTriggerEnter2D(Collider2D other)
        {

            if (!other.TryGetComponent(typeof(PlayerController), out var player)) return;
            _playerController = (player as PlayerController);
        }

       
           
           
        

        public override void StartReaction()
        {
            IsFighting = true;
            IsFollowPlayer = true;
            SuperAttackStarted?.Invoke();
            IsFirstAttack = true;
            StartCoroutine(PissAttack());
        }

        private IEnumerator PissAttack()
        {
            yield return new WaitForSeconds(0.2f);
            pissAttack.Play();
        }

    }
}
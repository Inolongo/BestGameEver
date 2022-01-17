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

        private bool reactionStarted;

        private void OnTriggerEnter2D(Collider2D other)
        {

            if (!other.TryGetComponent(typeof(PlayerController), out var player)) return;
            _playerController = (player as PlayerController);
        }

        private void Update()
        {
            if (!reactionStarted) return;
            pissAttack.Play();
            pissAttack.transform.Rotate(new Vector3(20, 0, 0)*Time.deltaTime);
           
        }

        public override void StartReaction()
        {
            //StartCoroutine(PissAttack());
            IsFighting = true;
            IsFirstAttack = true;

        }

        private IEnumerator PissAttack()
        {
            yield return new WaitForSeconds(0.2f);
            
        }

    }
}
using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class BossDialog : DialogReactions
    {
        private PlayerController _playerController;
        [SerializeField] private ParticleSystem pissAttack;

        private void OnTriggerEnter2D(Collider2D other)
        {

            if (!other.TryGetComponent(typeof(PlayerController), out var player)) return;
            _playerController = (player as PlayerController);
        }


        public override void StartReaction()
        {
            StartCoroutine(PissAttack());
        }

        private IEnumerator PissAttack()
        {
            yield return new WaitForSeconds(1);
            pissAttack.transform.Rotate(180f, 0f, 0f);
            pissAttack.Play();
        }

    }
}
using System;
using System.Collections;
using DefaultNamespace;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
internal class PissingTree : DialogReactions
{
    [SerializeField] private ParticleSystem pissParticle;
    private PlayerController _playerController;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(typeof(PlayerController), out var player)) return;
        _playerController = (player as PlayerController);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _playerController = null;
    }

    public override void StartReaction()
    {
        pissParticle.Play();
        StartCoroutine(YellowSpriteWait());
    }

    private IEnumerator YellowSpriteWait()
    {
        yield return new WaitForSeconds(0.5f);
        if (_playerController == null) yield break;
        _playerController.GetComponent<SpriteRenderer>().DOColor(Color.yellow, 2f);
        _playerController.ChangeHealth(changedHealthPoints);
    }
}
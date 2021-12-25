using System;
using System.Collections;
using DefaultNamespace;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
internal class BeerEffect : DialogReactions
{
    [SerializeField] private ParticleSystem beerParticle;
    private PlayerController _playerController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<PlayerController>(out var playerController))
        {
            _playerController = playerController;
                 }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _playerController = null;
    }

    public override void StartReaction()
    {
        StartCoroutine(WhiteSpiteWait());
    }

    private IEnumerator WhiteSpiteWait()
    {
        yield return new WaitForSeconds(1);
        if (_playerController != null)
        {
            beerParticle.Play();
            _playerController.GetComponent<SpriteRenderer>().color = Color.white;
            _playerController.ChangeHealth(changedHealthPoints);
        }
    }
}
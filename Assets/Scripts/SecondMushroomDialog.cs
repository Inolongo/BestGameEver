using System;
using System.Collections;
using DefaultNamespace;
using Enemy;
using UnityEngine;

public class SecondMushroomDialog : DialogReactions
{
    [SerializeField] private ParticleSystem beerParticle;
    private PlayerController _playerController;
    public bool IsFollowPlayer { get; private set; }
    public bool IsFighting { get; private set; }


   

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
        if (!IsAngry)
        {
            StartCoroutine(WhiteSpiteWait());
            IsFollowPlayer = true;
        }
        else
        {
            StartCoroutine(StartFighting());
        }
        
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

    private IEnumerator StartFighting()
    {
        yield return new WaitForSeconds(1);
        IsFollowPlayer = true;
        IsFighting = true;
        gameObject.SetActive(false);
    }
}
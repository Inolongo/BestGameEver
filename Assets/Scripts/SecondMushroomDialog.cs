using DefaultNamespace;
using UnityEngine;

internal class SecondMushroomDialog : DialogReactions
{
    [SerializeField] private ParticleSystem pissParticle;

    private PlayerController _playerController;
    private MushroomController _mushroomController;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (FindObjectOfType<FirstMushroomDialog>().isActiveAndEnabled) return;

        if (!other.TryGetComponent(typeof(PlayerController), out var player)) return;
        _playerController = (player as PlayerController);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _playerController = null;
    }
    
    public override void StartReaction()
    {
        Debug.Log("reaction");
    }
}
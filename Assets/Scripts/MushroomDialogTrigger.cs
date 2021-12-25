using UnityEngine;

namespace DefaultNamespace
{
    public class MushroomDialogTrigger: MonoBehaviour
    {
        [SerializeField] private PlayerController playerController;
        [SerializeField] private GameObject firstMushroomDialog;
        [SerializeField] private GameObject secondMushroomDialog;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(typeof(PlayerController), out var player)) return;
            playerController = (player as PlayerController);
            ChooseDialogVersion();
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            playerController = null;
            enabled = false;

        }

        private void ChooseDialogVersion()
        {
            if (playerController.GetComponent<SpriteRenderer>().color == Color.yellow)
            {
                firstMushroomDialog.SetActive(true);
            }
            else
            {
                secondMushroomDialog.SetActive(true);
            }
            
            gameObject.SetActive(false);

        }
    }
}
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace DefaultNamespace
{
    public enum Choice
    {
        None,
        Left,
        Right,
    }

    public class Dialog : MonoBehaviour
    {
        [SerializeField] private GameObject NPC;
        [SerializeField] private DialogCloud cloudPrefab;
        [SerializeField] private DialogData dialogData;
        [SerializeField] private DialogReactions dialogReaction;
        private DialogCloud _dialogCloudPlayer;
        private DialogCloud _dialogCloudNPC;
        private Coroutine _startDialogRoutine;
        private PlayerController _playerController;
        private Choice _choice = Choice.None;


        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.TryGetComponent(typeof(PlayerController), out var player)) return;
            _playerController = (player as PlayerController);
            _playerController.StopMovementWhenDialogStarted();
            StartDialog(other.transform);
        }

        private void OnDisable()
        {
            Debug.Log("fdsafsd");
        }

        private void StartDialog(Transform player)
        {
            _dialogCloudNPC = Instantiate(cloudPrefab);

            _dialogCloudPlayer = Instantiate(cloudPrefab);

            _dialogCloudPlayer.LeftButtonPressed += OnLeftButtonPressed;
            _dialogCloudPlayer.RightButtonPressed += OnRightButtonPressed;

            _dialogCloudPlayer.gameObject.SetActive(false);
            _dialogCloudNPC.gameObject.SetActive(false);

            _dialogCloudPlayer.FollowTarget(player);
            _dialogCloudNPC.FollowTarget(NPC.transform);
            _startDialogRoutine = StartCoroutine(StartDialogSequence());
        }

        private void OnLeftButtonPressed()
        {
            _choice = Choice.Left;
        }

        private void OnRightButtonPressed()
        {
            _choice = Choice.Right;
        }


        private IEnumerator StartDialogSequence(int index = 0)
        {
            var sentences = new List<DialogData.Sentence>();
            sentences.AddRange(dialogData.GetSentence());
            for (var i = index; i < sentences.Count; i++)
            {
                var sentence = sentences[i];
                if (sentence.Speaker == DialogData.Speaker.Player)
                {
                    StartCoroutine(_dialogCloudPlayer.PrintText(sentence.SentenceText));
                    _dialogCloudPlayer.gameObject.SetActive(true);
                    if (sentence.SentenceWithChoice)
                    {
                        _dialogCloudPlayer.SetActiveButtons();
                        yield return new WaitUntil(() => _choice != Choice.None);

                        switch (_choice)
                        {
                            case Choice.Right:
                                i = sentence.RightButtonNextDialogIndex - 1;

                                break;
                            case Choice.Left:
                                i = sentence.LeftButtonNextDialogIndex - 1;
                                sentences.RemoveAt(sentence.RightButtonNextDialogIndex);

                                break;
                        }

                        _choice = Choice.None;
                    }

                    if (sentence.IsHasReaction)
                    {
                        if (sentence.IsAngry)
                        {
                            dialogReaction.IsAngry = true;
                            
                        }
                        dialogReaction.StartReaction();
                    }
                    else
                    {
                        _dialogCloudPlayer.InactivateButtons();
                    }

                    if (sentence.IsEndOfDialog)
                    {
                        PleaseStopThisDialogForTheGodSake();
                    }
                }
                else
                {
                    StartCoroutine(_dialogCloudNPC.PrintText(sentence.SentenceText));
                    _dialogCloudNPC.gameObject.SetActive(true);
                    if (sentence.IsHasReaction)
                    {
                        dialogReaction.StartReaction();
                    }

                }

                yield return new WaitUntil(() => Input.GetButtonDown("Submit"));
                yield return new WaitUntil(() => !Input.GetButtonDown("Submit"));
            }

            PleaseStopThisDialogForTheGodSake();
            GetComponent<BoxCollider2D>().enabled = false;
        }

        private void PleaseStopThisDialogForTheGodSake()
        {
            StopCoroutine(_startDialogRoutine);
            _playerController.StartMovementWhenDialogEnded();
            Destroy(_dialogCloudPlayer.gameObject);
            Destroy(_dialogCloudNPC.gameObject);
            gameObject.SetActive(false);
        }
    }
}
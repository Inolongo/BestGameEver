using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace DefaultNamespace
{
    public class DialogCloud: MonoBehaviour
    {
        [SerializeField] private TextMeshPro dialogContent;
        [SerializeField] private Vector2 dialogOffset;
        [SerializeField] private GameObject buttons;
        [SerializeField] private SpriteRenderer buttonLeft;
        [SerializeField] private SpriteRenderer buttonRight;
        [SerializeField] private float printDelay;
        private Transform _target;
        public Action LeftButtonPressed;
        public Action RightButtonPressed;
        public bool IsPrinting;


        void OnValidate()
        {
            buttonLeft.color = Color.magenta;
            buttonRight.color = Color.cyan;
        }
        

        void Update()
        {
            if (_target != null)
            {
                transform.position = new Vector2( _target.position.x + dialogOffset.x, 
                                                           _target.position.y + dialogOffset.y);
            }
            if (Input.GetButtonDown("Horizontal") && buttons.activeSelf)
            {
                ButtonColorChange();
                ButtonChoice();
            }

            if (Input.GetButtonDown("Submit") && buttons.activeSelf)
            {
                if (IsPrinting) return;
                ButtonChoice();
                Debug.Log("submitted");

            }
        }

        public IEnumerator PrintText(string content)
        {
            dialogContent.text = string.Empty;
            foreach (var symbol in content)
            {
                dialogContent.text += symbol.ToString();
                yield return new WaitForSeconds(printDelay);
                IsPrinting = true;
            }

            IsPrinting = false;

        }
        

        public void FollowTarget(Transform target)
        {
            _target = target;
        }
        

        public void SetActiveButtons()
        {
            buttons.SetActive(true);
        }

        private void ButtonColorChange()
        {
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                buttonRight.color = Color.magenta;
                buttonLeft.color = Color.cyan;
            }
            else
            {
                buttonLeft.color = Color.magenta;
                buttonRight.color = Color.cyan;
            }
        }

        private void ButtonChoice()
        {
            if (buttonLeft.color == Color.magenta && Input.GetButtonDown("Submit"))
            {
                LeftButtonPressed?.Invoke();
            }
            else if (buttonRight.color == Color.magenta && Input.GetButtonDown("Submit"))
            {
                RightButtonPressed?.Invoke();
            }
        }

        public void InactivateButtons()
        {
            buttons.SetActive(false);
        }
        
    }
}
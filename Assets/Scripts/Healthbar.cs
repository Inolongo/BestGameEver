using System;
using System.Net.Mime;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class Healthbar: MonoBehaviour
    {
        [SerializeField] private Image healthbar;
        [SerializeField] private TextMeshProUGUI healthPercent;
        [SerializeField] private PlayerController playerController;
        
        private float _currentHealth;
        private Tween _fillHealthTween;

        private void OnValidate()
        {
            if (playerController is null)
            {
                var findObjectsOfType = FindObjectsOfType<PlayerController>();
                if (findObjectsOfType.Length > 1)
                {
                    Debug.LogError("Need only one player on scene!");
                }

                playerController = findObjectsOfType[0];
            }
        }

        void Start()
        {
            _currentHealth = playerController.Health;
            healthbar.fillAmount = _currentHealth / 100;
            playerController.HealthChanged += OnHealthChanged;
        }
        

        private void OnHealthChanged(float health)
        {
            _fillHealthTween.Kill();
            _fillHealthTween = null;
            _fillHealthTween = healthbar.DOFillAmount(health / 100, 1f);
            healthPercent.text = health+ "%";
        }
    }
}
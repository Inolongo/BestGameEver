using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class SuperPissAttack : MonoBehaviour
    {
        [SerializeField] private float damage;
        private ParticleSystem _particleSystem;
        private List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();

        public event Action<float> OnPlayerDamage;


        

        public void Play()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            _particleSystem.Play();
            Debug.Log("playing");
            //_animator.Play();
        }

        private void OnParticleCollision(GameObject other)
        {
            Debug.Log("check collision");
            if (other.TryGetComponent(typeof(PlayerController), out var playerController))
            {
                OnPlayerDamage?.Invoke(damage);
                Debug.Log("damage particle");
            }
        }
    }
}
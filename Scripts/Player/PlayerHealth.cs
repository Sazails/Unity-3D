using System;
using _Project.Scripts._Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts._Player
{
    [Serializable]
    public class PlayerHealth : IHealth
    {
        public int CurrentHealth => currentHealth;
        [SerializeField] private int currentHealth;
        private const int MaxHealth = 100;

        public void Init()
        {
            currentHealth = MaxHealth;
            UpdateUI();
        }

        public void SetHealth(int health)
        {
            currentHealth = health;
            ClampHealth();
            UpdateUI();
        }

        public void AddHealth(int health)
        {
            if (!NeedHealth())
                return;

            currentHealth += health;
            ClampHealth();
            UpdateUI();
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            ClampHealth();

            if (currentHealth <= 0)
                Die();

            UpdateUI();
        }

        public bool NeedHealth()
        {
            return currentHealth < MaxHealth;
        }

        public void Die()
        {
            Debug.Log("Player has died!");
        }

        private void ClampHealth()
        {
            currentHealth = Mathf.Clamp(currentHealth, 0, MaxHealth);
        }

        private void UpdateUI()
        {
            // TODO : Add screen indication of health (red glow for very low health)
        }
    }
}

using UnityEngine;

namespace Assets._Core.Scripts._Actor
{
    /// <summary>
    /// All health related functions to the Actor, will be dealt with here, except functions like dying, which need to be called back to the ActorBase.
    /// </summary>
    public class ActorHealth
    {
        public int _health = 100;
        public int _maxHealth = 100;

        public void SetHealth(int health, int maxHealth)
        {
            _health = health;
            _maxHealth = maxHealth;
            Debug.LogFormat("Health set {0}/{1}", health, maxHealth);
        }

        public void TakeDamage(ActorBase damageFrom, int damage)
        {
            _health -= damage;
            if (_health < 1)
            {
                Die(damageFrom);
                _health = 0;
            }

            Debug.Log("Took damage");
        }

        public void AddHealth(int health)
        {
            _health += health;
            if (_health > _maxHealth)
                _health = _maxHealth;

            Debug.Log("Added health");
        }

        public void Die(ActorBase dieFrom)
        {
            Debug.Log("Died");
        }
    }
}
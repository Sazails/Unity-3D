namespace _Project.Scripts._Interfaces
{
    public interface IHealth
    {
        void SetHealth(int health);
        void AddHealth(int health);
        void TakeDamage(int damage);
        bool NeedHealth();
        void Die();
    }
}
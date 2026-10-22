using Aetherium.Combat.Data;

namespace Aetherium.Combat.Interfaces
{
    public interface IDamageable
    {
        void TakeDamage(DamagePayload payload);
    }

    public interface IPostureHittable
    {
        void TakePostureDamage(float amount);
    }

    public interface IParryable
    {
        bool TryParry(DamagePayload payload);
    }
}

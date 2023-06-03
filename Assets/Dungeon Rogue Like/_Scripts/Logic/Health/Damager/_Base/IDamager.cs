using Dungeon.Logic.Health;

namespace Dungeon.Logic.Health
{
    public interface IDamager
    {
        public void Damage(IDamageable damageable);
    }
}

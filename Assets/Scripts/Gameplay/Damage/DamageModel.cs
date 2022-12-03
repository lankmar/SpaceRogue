namespace Gameplay.Damage
{
    public sealed class DamageModel
    {
        public float DamageAmount { get; }

        public DamageModel(float damageAmount)
        {
            DamageAmount = damageAmount;
        }
    }
}
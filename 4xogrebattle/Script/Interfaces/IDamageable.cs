public interface IDamageable {
    void TakeDamage(int amount);
    void Restore(int amount);
    bool IsDead { get; }
}
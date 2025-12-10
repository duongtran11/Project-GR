public interface IDamageable
{
    string Name { get; }
    float Health { get; set; }
    void OnDamage(float damageAmount);
}
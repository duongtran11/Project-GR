using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    public string Name { get; }

    public float Health { get; set; }

    public void OnDamage(float damageAmount)
    {
        Health -= damageAmount;
        Debug.Log("Get shot!");
    }
}

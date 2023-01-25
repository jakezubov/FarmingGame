using UnityEngine;

public class EnemyAttackHitbox : MonoBehaviour
{
    public Stats _stats;
    private float _damage;

    void Start()
    {
        _damage = GetComponentInParent<SetupEnemy>()._enemy.damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _stats.LowerCurrentStatAmount(Stat.health, _damage);
        }
    }
}

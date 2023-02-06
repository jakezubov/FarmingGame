using UnityEngine;

public class EnemyAttackHitbox : MonoBehaviour
{
    private Stat _health;
    private CombatTraits _combat;
    private MagicTraits _magic;
    private float _damage;
    private DamageType _damageType;

    void Start()
    {       
        _health = GameObject.Find("Health").GetComponent<Stat>();        
        _damage = GetComponentInParent<SetupEnemy>()._enemy.damage;
        _damageType = GetComponentInParent<SetupEnemy>()._enemy.damageType;

        GameObject traits = GameObject.Find("UI Canvas").transform.GetChild(0).GetChild(2).GetChild(4).gameObject;
        _combat = traits.transform.GetChild(0).GetComponent<CombatTraits>();
        _magic = traits.transform.GetChild(1).GetComponent<MagicTraits>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.CompareTag("Player"))
        {
            float damage = _damage;
            if (_damageType == DamageType.Piercing || _damageType == DamageType.Slashing ||
                _damageType == DamageType.Bludgeoning)
            {
                damage *= (1f - _combat.GetSturdyDamageReduction() / 100f);                
            }
            else if (_damageType == DamageType.Arcane || _damageType == DamageType.Fire ||
                     _damageType == DamageType.Lightning || _damageType == DamageType.Ice)
            {
                damage *= (1f - _magic.GetWillpowerDamageReduction() / 100f);
            }
            _health.LowerStatAmount(damage);
        }
    }
}

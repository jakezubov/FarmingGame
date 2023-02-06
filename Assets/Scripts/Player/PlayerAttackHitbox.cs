using UnityEngine;

public class PlayerAttackHitbox : MonoBehaviour
{
    public CombatTraits _combat; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (InventoryManager._instance.GetSelectedToolbarItem(false) != null)
            {
                float damage = InventoryManager._instance.GetSelectedToolbarItem(false).damage;
                if (InventoryManager._instance.GetSelectedToolbarItem(false).subType == SubType.Bow ||
                    InventoryManager._instance.GetSelectedToolbarItem(false).subType == SubType.Crossbow)
                {
                    damage *= (1f + _combat.GetRangedAffinityExtraDamagePercentage() / 100f);
                }
                else
                {
                    damage *= (1f + _combat.GetMeleeAffinityExtraDamagePercentage() / 100f);
                }
                collision.GetComponentInChildren<ChangeSlider>().LowerValue(Mathf.RoundToInt(damage));
            }            
        }
    }
}

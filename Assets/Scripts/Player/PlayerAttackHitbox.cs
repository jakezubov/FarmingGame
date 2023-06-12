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
                if (InventoryManager._instance.GetSelectedToolbarItem(false).type == Type.Tool) 
                { 
                    Tool tool = (Tool)InventoryManager._instance.GetSelectedToolbarItem(false);

                    // if hitbox hits an enemy do damage based on the tools damage and the melee affinity modifier
                    tool.damage *= (1f + (SaveData.meleeAffinityLevel * 2) / 100f);
                    collision.GetComponentInChildren<ChangeSlider>().LowerValue(Mathf.RoundToInt(tool.damage));
                }
                else if (InventoryManager._instance.GetSelectedToolbarItem(false).type == Type.Weapon) 
                {
                    Weapon weapon = (Weapon)InventoryManager._instance.GetSelectedToolbarItem(false);        
                    if (weapon.weaponType == WeaponType.Bow || weapon.weaponType == WeaponType.Crossbow)
                    {
                        // if hitbox hits an enemy do damage based on the weapons damage and the ranged affinity modifier
                        weapon.damage *= (1f + (SaveData.rangedAffinityLevel * 2) / 100f);
                    } 
                    else 
                    {
                        // if hitbox hits an enemy do damage based on the weapons damage and the melee affinity modifier
                        weapon.damage *= (1f + (SaveData.meleeAffinityLevel * 2) / 100f); 
                    }
                    collision.GetComponentInChildren<ChangeSlider>().LowerValue(Mathf.RoundToInt(weapon.damage));
                }
            }            
        }
    }
}

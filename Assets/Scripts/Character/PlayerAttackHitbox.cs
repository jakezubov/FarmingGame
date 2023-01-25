using UnityEngine;

public class PlayerAttackHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (InventoryManager._instance.GetSelectedToolbarItem(false) != null)
            {
                float damage = InventoryManager._instance.GetSelectedToolbarItem(false).damage;
                collision.GetComponentInChildren<ChangeSlider>().LowerValue(Mathf.RoundToInt(damage));
            }            
        }
    }
}

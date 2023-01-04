public class BinSlot : Slot
{
    public override void OnDropBase(InventoryItem item)
    {
        Destroy(item.gameObject);
        InventoryManager._instance.SetInventoryFull(false);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class SetupCraftableItem : MonoBehaviour
{
    private Button _button;
    private CraftableItemList _list;
    private CraftableItem _item;

    void Start()
    {
        _list = transform.parent.parent.GetComponent<CraftableItemList>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(delegate() { _list.UpdateItemInformation(_item); });
    }

    public void SetItem(CraftableItem Item)
    {
        _item = Item;
    }
}

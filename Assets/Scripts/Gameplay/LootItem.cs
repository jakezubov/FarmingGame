using System.Collections;
using UnityEngine;

public class LootItem : MonoBehaviour
{
    public SpriteRenderer _sr;

    private Item _item;
    private bool _throwingItem = false;
    private readonly float _moveSpeed = 4;

    public void Initialise(Item item)
    {
        this._item = item;
        _sr.sprite = item.inventoryImage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !_throwingItem)
        {
            // only transitions towards player if inventory isnt full
            bool canAdd = InventoryManager._instance.AddItem(_item);
            if (canAdd)
            {
                StartCoroutine(MoveAndCollect(collision.transform));
            }        
        }
    }

    private IEnumerator MoveAndCollect(Transform target)
    {
        // move item towards player and destroy on contact and add item to inventory
        while (transform.position != target.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, _moveSpeed * Time.deltaTime);
            yield return 0;
        }

        Destroy(gameObject);
    }

    public IEnumerator ThrowItem(Vector3 target)
    {
        _throwingItem = true;

        // selects a random position to drop item
        target.x += Random.Range(1, -2);
        target.y -= 1;

        // transitions to that location
        while (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, _moveSpeed * Time.deltaTime);
            yield return 0;
        }

        _throwingItem = false;
    }
}

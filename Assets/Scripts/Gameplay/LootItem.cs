using System.Collections;
using UnityEngine;

public class LootItem : MonoBehaviour
{
    public SpriteRenderer _sr;
    public BoxCollider2D _collider;

    private float _moveSpeed = 4;
    private Item _item;
    private bool _throwingItem = false;

    public void Initialise(Item item)
    {
        this._item = item;
        _sr.sprite = item.GetImage();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !_throwingItem)
        {
            bool canAdd = InventoryManager._instance.AddItem(_item);
            if (canAdd)
            {
                StartCoroutine(MoveAndCollect(collision.transform));
            }        
        }
    }

    private IEnumerator MoveAndCollect(Transform target)
    {
        Destroy(_collider);

        while (transform.position != target.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, _moveSpeed * Time.deltaTime);
            yield return 0;
        }

        Destroy(gameObject);
    }

    // for drop item script
    public IEnumerator ThrowItem(Vector3 target)
    {
        _throwingItem = true;
        Destroy(_collider);

        target.x += Random.Range(1, -2);
        target.y -= 1;

        while (transform.position != target)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, _moveSpeed * Time.deltaTime);
            yield return 0;
        }

        _throwingItem = false;
    }
}

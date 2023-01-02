using System.Collections;
using UnityEngine;

public class LootItem : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sr;
    [SerializeField] private BoxCollider2D _collider;
    [SerializeField] private float _moveSpeed;

    private Item item;

    public void Initialise(Item item)
    {
        this.item = item;
        _sr.sprite = item._image;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bool canAdd = InventoryManager._instance.AddItem(item);
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
}

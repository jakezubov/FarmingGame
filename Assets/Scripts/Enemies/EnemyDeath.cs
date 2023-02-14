using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyDeath : MonoBehaviour
{
    public ChangeSlider _health;
    public GameObject _lootPrefab;
    public GameObject _destination;

    void Update()
    {
        if (_health.GetValue() <= 0)
        {
            // add in death animation here
            Destroy(gameObject);
            Destroy(_destination);

            // choses an item from the enemies drop list and spawns it on the ground 
            Tilemap tilemap = GameObject.Find("Dropped Objects").GetComponent<Tilemap>();
            Vector3Int pos = tilemap.WorldToCell(transform.position);
            GameObject loot = Instantiate(_lootPrefab, pos, Quaternion.identity);

            SetupEnemy enemy = GetComponentInChildren<SetupEnemy>();
            Item randItem = enemy._enemy.droppedItems[Random.Range(0, enemy._enemy.droppedItems.Length)];
            loot.GetComponent<LootItem>().Initialise(randItem);

            GameObject parentAfterDrop = GameObject.Find("DroppedObjects");
            loot.transform.SetParent(parentAfterDrop.transform);
        }
    }
}

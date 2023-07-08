using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyAnimationFinishers : MonoBehaviour
{
    public Animator _anim;
    public ChangeSlider _health;
    public GameObject _lootPrefab;
    public GameObject _destination;

    void Update()
    {
        if (_health.GetValue() <= 0)
        {
            _anim.SetBool("Death", true);
        }
    }

    public void SetDestination(GameObject destination)
    {
        _destination = destination;
    }

    public void DeathAnimationFinish()
    {
        // Destroy the enemy
        Destroy(transform.parent.gameObject);
        Destroy(_destination);

        // choses an item from the enemies drop list and spawns it on the ground 
        Tilemap tilemap = GameObject.Find("Dropped Objects NC").GetComponent<Tilemap>();
        Vector3Int pos = tilemap.WorldToCell(transform.position);
        GameObject loot = Instantiate(_lootPrefab, pos, Quaternion.identity);

        SetupEnemy enemy = GetComponentInChildren<SetupEnemy>();
        Item randItem = enemy._enemy.droppedItems[Random.Range(0, enemy._enemy.droppedItems.Length)];
        loot.GetComponent<LootItem>().Initialise(randItem);

        GameObject parentAfterDrop = GameObject.Find("Dropped Objects");
        loot.transform.SetParent(parentAfterDrop.transform);
    }

    public void AttackAnimationFinish()
    {
        _anim.SetBool("Attack", false);
    }
}

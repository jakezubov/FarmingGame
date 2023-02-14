using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy[] _enemies;
    public int _resetCooldown;
    public int _maxEnemies;
    public GameObject _prefab;

    private float _cooldown;
    private bool _playerInRange;

    void Start()
    {
        _cooldown = _resetCooldown;    
    }

    void Update()
    {
        // spawns enemy if player isnt in range and the max amount of enimies isnt currently spawned by the spawner
        if (!_playerInRange && transform.childCount < _maxEnemies)
        {
            _cooldown -= Time.deltaTime;

            if (_cooldown <= 0)
            {
                _cooldown = _resetCooldown;

                GameObject newEnemy = Instantiate(_prefab, transform.position, Quaternion.identity);
                newEnemy.transform.SetParent(transform);

                Enemy randEnemy = _enemies[Random.Range(0, _enemies.Length)];
                newEnemy.GetComponentInChildren<SetupEnemy>()._enemy = randEnemy;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _playerInRange = false;
        }
    }
}

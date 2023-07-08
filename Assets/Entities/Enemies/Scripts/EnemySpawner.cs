using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy[] _enemies;
    public GameObject _prefab;
    public int _cooldown;
    public int _maxEnemies;

    private float _currentCooldown;
    private bool _playerInRange;

    void Start()
    {
        _currentCooldown = 1;    
    }

    void Update()
    {
        // spawns enemy if player isnt in range and the max amount of enimies isnt spawned by the spawner
        if (!_playerInRange && _maxEnemies > 0)
        {
            _currentCooldown -= Time.deltaTime;

            if (_currentCooldown <= 0)
            {
                _currentCooldown = _cooldown;

                GameObject newEnemy = Instantiate(_prefab, transform.position, Quaternion.identity);
                newEnemy.transform.SetParent(transform);

                Enemy randEnemy = _enemies[Random.Range(0, _enemies.Length)];
                newEnemy.GetComponentInChildren<SetupEnemy>()._enemy = randEnemy;

                _maxEnemies--;
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

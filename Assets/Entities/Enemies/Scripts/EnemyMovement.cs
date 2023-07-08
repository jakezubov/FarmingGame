using UnityEngine;
using Pathfinding;
using UnityEngine.Tilemaps;

public class EnemyMovement : MonoBehaviour
{   
    public AIPath _aiPath;
    public AIDestinationSetter _aiDest;
    public Animator _anim;
    public EnemyAnimationFinishers _death;

    private Tilemap _tilemap;
    private Vector3Int _currentCell;
    private GameObject _destination;
    private bool _hasDestination = false, _isChasing = false;
    private float _walkSpeed;
    private readonly int _travelDistance = 10;
    

    private void Start()
    {
        _tilemap = GameObject.Find("Ground NC").GetComponent<Tilemap>();   
        _walkSpeed = GetComponentInParent<SetupEnemy>()._enemy.walkSpeed;

        // creates desination object, sets the spawner as the parent and gives it to the EnemyDeath script so it can be deleted on death
        _destination = new GameObject("Destination");
        _destination.transform.SetParent(transform.parent.parent.parent);
        _death.SetDestination(_destination);
    }

    private void Update()
    {
        while (!_hasDestination)
        {
            // picks a random destination in a sphere
            Vector3 randPos = transform.position + Random.insideUnitSphere * _travelDistance;
            randPos.z = 0;

            // gives destination to Astar pathfinder
            GraphNode node = AstarPath.active.GetNearest(randPos).node;
            _destination.transform.position = (Vector3)node.position;

            // transitions towards desination
            if (node.Walkable)
            {
                _aiDest.target = _destination.transform;
                _hasDestination = true;
            }
        }

        if (_aiPath.reachedDestination && !_isChasing)
        {
            _hasDestination = false;
        }

        // once the player is a certain distance away the enemy will resume wandering
        if (_isChasing && Vector3.Distance(transform.position, _aiDest.target.position) > 10.0f )
        {    
            _aiPath.maxSpeed = _walkSpeed;
            _isChasing = false;
            _hasDestination = false;
        }
    }

    void FixedUpdate()
    {
        _currentCell = _tilemap.WorldToCell(transform.position);

        // controls all the animations for the enemy
        if (_aiPath.desiredVelocity.x >= 0.01f && _aiPath.desiredVelocity.x > _aiPath.desiredVelocity.y)
        {
            _currentCell.x += 1;
            SetAllAnimationsFalse();
            _anim.SetBool("MoveRight", true);
        }
        else if (_aiPath.desiredVelocity.x <= -0.01f && _aiPath.desiredVelocity.x < _aiPath.desiredVelocity.y)
        {
            _currentCell.x -= 1;
            SetAllAnimationsFalse();
            _anim.SetBool("MoveLeft", true);
        }
        else if (_aiPath.desiredVelocity.y >= 0.01f && _aiPath.desiredVelocity.y > _aiPath.desiredVelocity.x)
        {
            _currentCell.y += 1;
            SetAllAnimationsFalse();
            _anim.SetBool("MoveUp", true);       
        }
        else if (_aiPath.desiredVelocity.y <= -0.01f && _aiPath.desiredVelocity.y < _aiPath.desiredVelocity.x)
        {
            _currentCell.y -= 1;
            SetAllAnimationsFalse();
            _anim.SetBool("MoveDown", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if the player comes in range the enemy will start chasing 
        if (collision.CompareTag("Player"))
        {
            _aiPath.maxSpeed = _walkSpeed*2;
            _aiDest.target = collision.gameObject.transform;
            _isChasing = true;
        }
    }

    // default state for animations
    private void SetAllAnimationsFalse()
    {
        _anim.SetBool("MoveLeft", false);
        _anim.SetBool("MoveRight", false);
        _anim.SetBool("MoveUp", false);
        _anim.SetBool("MoveDown", false);
    }
}

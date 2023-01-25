using UnityEngine;
using Pathfinding;
using UnityEngine.Tilemaps;

public class EnemyMovement : MonoBehaviour
{
    public Tilemap _groundTilemap;

    private AIPath _aiPath;
    private AIDestinationSetter _aiDest;
    private Animator _anim;

    private Vector3Int _currentCell;
    private GameObject _destinationParent;
    private GameObject _destination;
    private bool _hasDestination = false;
    private bool _isChasing = false;

    private float _walkSpeed;
    

    private void Start()
    {
        _aiPath = transform.parent.GetComponentInParent<AIPath>();
        _aiDest = transform.parent.GetComponentInParent<AIDestinationSetter>();
        _anim = GetComponentInParent<Animator>();

        if (!GameObject.Find("Enemy Destinations"))
        {
            _destinationParent = new GameObject("Enemy Destinations");
        }
        _destination = new GameObject("Destination");
        _destination.transform.SetParent(_destinationParent.transform);

        _walkSpeed = GetComponentInParent<SetupEnemy>()._enemy.walkSpeed;
    }

    private void Update()
    {
        while (!_hasDestination)
        {
            // picks a random destination
            Vector3 randPos = Random.insideUnitSphere * 20;
            randPos.z = 0;

            GraphNode node = AstarPath.active.GetNearest(randPos).node;
            _destination.transform.position = (Vector3)node.position;

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

        if (_isChasing && Vector3.Distance(transform.position, _aiDest.target.position) > 10.0f )
        {    
            _anim.SetBool("Walk", true);
            _anim.SetBool("Run", false);
            _aiPath.maxSpeed = _walkSpeed;
            _isChasing = false;
            _hasDestination = false;
        }
    }

    void FixedUpdate()
    {
        _currentCell = _groundTilemap.WorldToCell(transform.position);

        if (_aiPath.desiredVelocity.x >= 0.01f && _aiPath.desiredVelocity.x > _aiPath.desiredVelocity.y)
        {
            _currentCell.x += 1;
            SetAllAnimationsFalse();
            _anim.SetBool("MoveSide", true);
            transform.parent.GetComponentInParent<Transform>().localScale = new Vector3(1f, 1f, 1f);
        }
        else if (_aiPath.desiredVelocity.x <= -0.01f && _aiPath.desiredVelocity.x < _aiPath.desiredVelocity.y)
        {
            _currentCell.x -= 1;
            SetAllAnimationsFalse();
            _anim.SetBool("MoveSide", true);
            transform.parent.GetComponentInParent<Transform>().localScale = new Vector3(-1f, 1f, 1f);
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
        if (collision.CompareTag("Player"))
        {
            _anim.SetBool("Run", true);
            _anim.SetBool("Walk", false);
            _aiPath.maxSpeed = _walkSpeed*2;
            _aiDest.target = collision.gameObject.transform;
            _isChasing = true;
        }
    }

    private void SetAllAnimationsFalse()
    {
        _anim.SetBool("MoveSide", false);
        _anim.SetBool("MoveUp", false);
        _anim.SetBool("MoveDown", false);
    }

    public Vector3 GetCurrentCell()
    {
        return _currentCell;
    }
}

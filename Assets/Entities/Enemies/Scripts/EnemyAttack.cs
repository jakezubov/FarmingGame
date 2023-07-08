using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Animator _anim;

    private float _resetCooldown;
    private float _cooldown;

    private void Start()
    {
        _resetCooldown = transform.parent.GetComponentInParent<SetupEnemy>()._enemy.cooldown;
        _cooldown = _resetCooldown;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // attacks when player is in range
        if (collision.CompareTag("Player"))
        {
            _anim.SetBool("Attack", true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // if the player stays in the enemies collider the cooldown starts for the enemy to attack again
        if (collision.CompareTag("Player"))
        {
            _cooldown -= Time.deltaTime;
        }
        if (_cooldown <= 0)
        {
            _cooldown = _resetCooldown;
            _anim.SetBool("Attack", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _cooldown = _resetCooldown;
    }
}

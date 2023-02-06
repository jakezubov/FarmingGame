using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Animator _anim;
    private float _resetCooldown;
    private float _cooldown;

    private void Start()
    {
        _resetCooldown = GetComponentInParent<SetupEnemy>()._enemy.cooldown;
        _cooldown = _resetCooldown;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(PlaySwingAnimation(collision));
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _cooldown -= Time.deltaTime;
        }
        if (_cooldown <= 0)
        {
            _cooldown = _resetCooldown;
            StartCoroutine(PlaySwingAnimation(collision));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _cooldown = _resetCooldown;
    }

    private IEnumerator PlaySwingAnimation(Collider2D collision)
    {
        _anim.SetBool("Swing", true);
        yield return new WaitForSeconds(0.65f);
        _anim.SetBool("Swing", false);
    }
}

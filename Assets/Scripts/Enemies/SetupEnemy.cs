using UnityEngine;

public class SetupEnemy : MonoBehaviour
{
    public Enemy _enemy;
    public Canvas _healthCanvas;
    public ChangeSlider _health;

    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = _enemy.image;
        GetComponent<Animator>().runtimeAnimatorController = _enemy.animationController;
        _healthCanvas.worldCamera = Camera.main;
        _health.SetMaxValue(Mathf.RoundToInt(_enemy.health));
        _health.SetValue(Mathf.RoundToInt(_enemy.health));
    }
}

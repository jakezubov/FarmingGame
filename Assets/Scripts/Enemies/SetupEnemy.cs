using UnityEngine;

public class SetupEnemy : MonoBehaviour
{
    public Enemy _enemy;
    public Canvas _healthCanvas;
    public ChangeSlider _health;

    void Start()
    {
        // gets the default image and the animation controller for the enemy and sets them (defined in the enemy scriptable object)
        GetComponent<SpriteRenderer>().sprite = _enemy.image;
        GetComponent<Animator>().runtimeAnimatorController = _enemy.animationController;

        // sets the health values
        _healthCanvas.worldCamera = Camera.main;
        _health.SetMaxValue(Mathf.RoundToInt(_enemy.health));
        _health.SetValue(Mathf.RoundToInt(_enemy.health));
    }
}

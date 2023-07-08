using UnityEditor;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public SceneAsset _scene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject.Find("Black Fade").GetComponent<SceneFade>().FadeOut(_scene.name);
        }
    }
}

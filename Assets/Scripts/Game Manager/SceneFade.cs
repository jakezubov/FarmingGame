using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFade : MonoBehaviour
{
    public Animator _animator;
    private string _sceneName;

    public void FadeOut(string newScene)
    {
        _sceneName = newScene;
        _animator.SetBool("FadeOut", true);
    }

    public void OnFadeOutComplete()
    {
        _animator.SetBool("FadeOut", false);
        SceneManager.LoadScene(_sceneName);
    }

    public void FadeIn()
    {
        _animator.SetBool("FadeIn", true);
    }

    public void OnFadeInComplete()
    {
        _animator.SetBool("FadeIn", false);
    }
}

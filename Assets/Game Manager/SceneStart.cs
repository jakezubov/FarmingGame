using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneStart : MonoBehaviour
{
    public SceneFade _fade;
    private GameObject[] _startLocations;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _startLocations = GameObject.FindGameObjectsWithTag("PlayerSpawn");

        foreach (GameObject location in _startLocations)
        {
            if (scene.name == "The Farm")
            {
                if (location.name == "House") { transform.position = location.transform.position; }
            }
            else if (scene.name == "Eastern Forest")
            {
                if (location.name == "Entrance") { transform.position = location.transform.position; }
            }
        }

        _fade.FadeIn();
    }
}

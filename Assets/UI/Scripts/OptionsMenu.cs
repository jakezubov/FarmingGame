using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public void Quit()
    {
        // button to quit game or if using in editor it will exit the active play session
        #if UNITY_STANDALONE
                Application.Quit();
        #endif
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}

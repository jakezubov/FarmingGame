using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class HighlightTile : MonoBehaviour
{
    public Tile _highlightTile;

    private Tilemap _highlightMap;
    private Scene _scene;
    private Vector3Int _previous;
    private readonly int _reach = 1;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _scene = scene;
        if (scene.name != "Startup")
        {
            _highlightMap = GameObject.Find("Highlight Tile NC").GetComponent<Tilemap>();
        }
    }

    private void LateUpdate()
    {
        if (_scene.name != "Startup")
        {
            // get current movements from the player controller
            (float movementInputHoriztonal, float movementInputVertical) = PlayerController.GetPlayerMovements();

            Vector3Int currentCell = _highlightMap.WorldToCell(transform.position);

            // change highlight tile position based on faced direction
            if (movementInputHoriztonal == -1) { currentCell.x -= _reach; }
            else if (movementInputHoriztonal == 1) { currentCell.x += _reach; }
            else if (movementInputVertical == -1) { currentCell.y -= _reach; }
            else if (movementInputVertical == 1) { currentCell.y += _reach; }
            else { currentCell = _previous; }

            // updates highlight tile to new cell when position changed
            if (currentCell != _previous)
            {
                _highlightMap.SetTile(currentCell, _highlightTile);
                _highlightMap.SetTile(_previous, null);

                _previous = currentCell;
            }
        }
    }
}

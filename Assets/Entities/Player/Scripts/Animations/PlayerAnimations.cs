using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class PlayerAnimations : MonoBehaviour
{
    public Animator _baseAnimator, _toolAnimator;
    public ToolStateManager _tools;
    public Tile _highlightTile;

    private Tilemap _highlightMap;
    private Vector3Int _previous;
    private readonly int _reach = 1;

    private void Awake() { SceneManager.sceneLoaded += OnSceneLoaded; }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Startup") { _highlightMap = GameObject.Find("Highlight Tile NC").GetComponent<Tilemap>(); }
    }

    public Vector3Int UpdateMovement(float movementInputHoriztonal, float movementInputVertical, Vector3Int currentCell)
    {
        // controls all the movement animations
        if (movementInputHoriztonal == -1 && movementInputVertical == 0) // start horizontal checks
        {
            currentCell.x -= _reach;
            SetAllMoveAnimationsFalse();

            _baseAnimator.SetBool("MoveLeft", true);
            _toolAnimator.SetBool("MoveLeft", true);
        }
        else if (movementInputHoriztonal == 1 && movementInputVertical == 0)
        {
            currentCell.x += _reach;
            SetAllMoveAnimationsFalse();

            _baseAnimator.SetBool("MoveRight", true);
            _toolAnimator.SetBool("MoveRight", true);
        }
        else if (movementInputVertical == -1 && movementInputHoriztonal == 0) // start vertical checks
        {
            currentCell.y -= _reach;
            SetAllMoveAnimationsFalse();

            _baseAnimator.SetBool("MoveDown", true);
            _toolAnimator.SetBool("MoveDown", true);
        }
        else if (movementInputVertical == 1 && movementInputHoriztonal == 0)
        {
            currentCell.y += _reach;
            SetAllMoveAnimationsFalse();

            _baseAnimator.SetBool("MoveUp", true);
            _toolAnimator.SetBool("MoveUp", true);
        }
        else if (movementInputVertical == 0 && movementInputHoriztonal == 0) 
        {
            currentCell = _previous;
            SetAllMoveAnimationsFalse();
        }

        if (currentCell != _previous) 
        {
            _highlightMap.SetTile(currentCell, _highlightTile);
            _highlightMap.SetTile(_previous, null);

            _previous = currentCell;  
        }

        return currentCell;
    }

    public void StartToolAnimation(Vector3Int currentCell)
    {
        _tools.GetData(currentCell);
        _baseAnimator.SetBool("UseTool", true);
        _toolAnimator.SetBool("UseTool", true);
        PlayerActions.SetInteracting(true);
        CheckForTools();
    }

    private void CheckForTools()
    {
        // changes the animation for what tool is held based on the currently selected toolbar item
        if (InventoryManager._instance.GetSelectedToolbarItem(false) != null &&
           InventoryManager._instance.GetSelectedToolbarItem(false).type == Type.Tool)
        {
            Tool tool = (Tool)InventoryManager._instance.GetSelectedToolbarItem(false);

            SetAllToolAnimationsFalse();

            if (tool.toolType == ToolType.Axe) 
            { 
                _toolAnimator.SetBool("HasAxe", true);
                _baseAnimator.SetBool("HasAxeHoePick", true);
            }
            else if (tool.toolType == ToolType.Pickaxe) 
            { 
                _toolAnimator.SetBool("HasPick", true);
                _baseAnimator.SetBool("HasAxeHoePick", true);
            }
            else if (tool.toolType == ToolType.Hoe) 
            { 
                _toolAnimator.SetBool("HasHoe", true);
                _baseAnimator.SetBool("HasAxeHoePick", true);
            }
            else if (tool.toolType == ToolType.FishingRod)
            {
                _toolAnimator.SetBool("HasRod", true);
                _baseAnimator.SetBool("HasRod", true);
            }
        }
    }

    private void SetAllMoveAnimationsFalse()
    {
        // default state for animations
        _baseAnimator.SetBool("MoveLeft", false);
        _baseAnimator.SetBool("MoveRight", false);
        _baseAnimator.SetBool("MoveUp", false);
        _baseAnimator.SetBool("MoveDown", false);

        _toolAnimator.SetBool("MoveLeft", false);
        _toolAnimator.SetBool("MoveRight", false);
        _toolAnimator.SetBool("MoveUp", false);
        _toolAnimator.SetBool("MoveDown", false);
    }

    private void SetAllToolAnimationsFalse()
    {
        _baseAnimator.SetBool("HasAxeHoePick", false);
        _baseAnimator.SetBool("HasRod", false);
        _baseAnimator.SetBool("PullRod", false);

        _toolAnimator.SetBool("HasPick", false);
        _toolAnimator.SetBool("HasAxe", false);
        _toolAnimator.SetBool("HasHoe", false);
        _toolAnimator.SetBool("HasRod", false);
        _toolAnimator.SetBool("PullRod", false);
    }
}

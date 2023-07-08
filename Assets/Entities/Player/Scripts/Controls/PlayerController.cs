using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region Properties

    public Spellbook _spellbook;
    public Map _map;
    public PlayerAnimations _playerAnimations;
    public SpellbookAnimations _spellbookAnimations;
    public SpellbookControls _spellbookControls;
    public GeneralControls _generalControls;

    private Tilemap _groundTilemap;
    private Rigidbody2D _rigidbody;
    private Scene _scene;
    private Vector3Int _currentCell;

    #endregion

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        SpellbookActions.SetSpellbookActive(false);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        _scene = scene;
        if (scene.name != "Startup") { _groundTilemap = GameObject.Find("Ground NC").GetComponent<Tilemap>(); }
    }

    private void Update()
    {
        if (_scene.name != "Startup")
        {
            // switch between action maps
            if (SpellbookActions.IsSpellbookActive())
            {
                PlayerActions.GetControls().SpellBook.Enable();
                PlayerActions.GetControls().General.Disable();

                if (!SpellbookActions.IsBookAnimationPlaying())
                {
                    _spellbookControls.UseControls(this, _spellbookAnimations, _map);
                }
            }
            else
            {
                PlayerActions.GetControls().SpellBook.Disable();
                PlayerActions.GetControls().General.Enable();

                _generalControls.UseControls(this, _spellbookAnimations, _map);
            }
        }
    }

    private void FixedUpdate()
    {
        if (_scene.name != "Startup" && !SpellbookActions.IsSpellbookActive() && !SpellbookActions.IsBookAnimationPlaying() &&
                !PlayerActions.IsInteracting() && !PlayerActions.IsMapOpen())
        {
            (float movementInputHoriztonal, float movementInputVertical) = PlayerActions.GetPlayerMovements();
            _currentCell = _groundTilemap.WorldToCell(transform.position);

            // Move the player
            Vector3 currentPosition = transform.position;
            currentPosition.x += movementInputHoriztonal * SaveData.moveSpeed * Time.deltaTime;
            currentPosition.y += movementInputVertical * SaveData.moveSpeed * Time.deltaTime;
            _rigidbody.MovePosition(currentPosition);

            _currentCell = _playerAnimations.UpdateMovement(movementInputHoriztonal, movementInputVertical, _currentCell);
        }
    }

    public Vector3Int GetCurrentCell()
    {
        return _currentCell;
    }
}

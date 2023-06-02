using UnityEngine;

public class PlayableCharacters : MonoBehaviour
{

# region CommonCoreComponents
    public CoreHandler core { get; private set; }
    public Animator anim { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }

    protected PlayerData _playerData = new PlayerData(); //data for stats refactor might not need it here
    
    [SerializeField]
    protected PlayerSOData playerSOData;//Data for states  
    #endregion
    #region  collision variables
    public Vector2 playerDirection;
    #endregion
    protected virtual void Awake()
    {
        core = GetComponentInChildren<CoreHandler>();
    }
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
    }
    protected virtual void Update() { }
    protected virtual void FixedUpdate() { }
}

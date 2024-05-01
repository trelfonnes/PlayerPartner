using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class BasicNPC : MonoBehaviour, IInteractable
{
    [SerializeField] protected string npcID;
   [SerializeField] protected NPCDataSO npcData;
    [SerializeField] bool isMovingNPC;
    protected bool isConversationActive = false;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float startingMoveSpeed = 3f;
    [SerializeField] float minMoveDistance = 2f;
    [SerializeField] float maxMoveDistance = 5f;
    [SerializeField] float minPauseDuration;
    [SerializeField] float maxPauseDuration;
    [SerializeField] GameObject itemToSpawn;
    [SerializeField] Transform spawnPoint;
    
    Animator anim;
    Vector2 moveDirection;
    Vector2 currentDirection;
    Rigidbody2D rb;
    ProgressMarker currentProgress;
    [SerializeField]LayerMask whatIsTurnFrom;
    [SerializeField]DialogueSystemTrigger DST;

   protected enum NPCState { Moving, TalkToPlayer, Idling}
   protected NPCState currentState = NPCState.Moving;
    private bool isPlayerColliding;

    protected virtual void Start()
    {
        DST = GetComponent<DialogueSystemTrigger>();
        GetNPCData();
        GetConversation();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if (isMovingNPC)
        {
            StartCoroutine(MoveRoutine());
            anim.SetBool("move", true);
        }
    }
    void GetConversation()
    {

    }
    void GetNPCData()
    {
        npcData = NPCDataManager.Instance.GetNPCData(npcID);

    }
    public void ChangeToMovingState() // call this after player is done talking to NPC will need to work with Dialogue system
    {
        Move();
    }
    public virtual void Interact()
    {
        currentState = NPCState.TalkToPlayer;
        AudioManager.Instance.PlayAudioClip("Interact");

        StartConversation();
    }
    protected virtual void StartConversation()
    {
        DialogueManager.Instance.conversationEnded += ConversationFinished;
        isConversationActive = true;
        moveSpeed = 0f;
        gameObject.GetComponent<DialogueSystemTrigger>().OnUse();
        
    }
    public virtual void ConversationFinished(Transform primaryActorName)
    {
        if (isConversationActive)
        {
            currentState = NPCState.Idling;
            if (isMovingNPC)
            {
                moveSpeed = startingMoveSpeed;
                currentState = NPCState.Moving;

            }
            isConversationActive = false;
            DialogueManager.Instance.conversationEnded -= ConversationFinished;
        }
    }
    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            if (currentState == NPCState.Moving)
            {
                // Determine a random move direction
                moveDirection = Random.insideUnitCircle.normalized;
              
                // Determine a random move distance
                float moveDistance = Random.Range(minMoveDistance, maxMoveDistance);

                // Move in the chosen direction for the determined distance
                float distanceMoved = 0f;
                FindAnimDirection(moveDirection);
                while (distanceMoved < moveDistance)
                {
                    Vector2 movement = moveDirection * moveSpeed * Time.deltaTime;
                    rb.MovePosition(rb.position + movement);
                    distanceMoved += movement.magnitude;
                    yield return null;
                }

                // Introduce a random pause before the next movement
                float pauseDuration = Random.Range(minPauseDuration, maxPauseDuration);
                Idle();
                yield return new WaitForSeconds(pauseDuration);
                Move();
            }
            else
            {
                yield return null;
            }
        }
    }

    private void FindAnimDirection(Vector2 moveDirection)
    {
         currentDirection = (moveDirection * moveSpeed);
        
            CheckToFlip(currentDirection);
        
        anim.SetFloat("moveX", currentDirection.x);
        anim.SetFloat("moveY", currentDirection.y);
    }

    void CheckToFlip(Vector2 vector2)
    {
        if (vector2.x < 0)
        {
            rb.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (vector2.x > 0)
        {
            rb.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    void Idle()
    {
        currentState = NPCState.Idling;
        anim.SetBool("move", false);
        anim.SetBool("idle", true);
    }
    void Move()
    {
        currentState = NPCState.Moving;
        anim.SetBool("idle", false);
        anim.SetBool("move", true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(currentState == NPCState.Moving && whatIsTurnFrom == (whatIsTurnFrom | (1 << collision.gameObject.layer)))
        {
            moveDirection *= -1f;
        }
       
    }
   

    protected void CheckGameProgress()
    {
        currentProgress = GameManager.Instance.ReturnCurrentGameProgress();
        if(currentProgress == ProgressMarker.act1)
        {

        }
        else if(currentProgress == ProgressMarker.act2)
        {

        }
        else if(currentProgress == ProgressMarker.act3)
        {

        }
        else if(currentProgress == ProgressMarker.act4)
        {

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DST.conversationActor = collision.transform;
        }
    }

    void CheckNPCNameItemSpawn(string name)
    {
        if(name == npcID)
        {
            if (!npcData.hasGivenItem)
            {
                Instantiate(itemToSpawn, spawnPoint);
                npcData.hasGivenItem = true;
            }
        }
    }

    //connecting with LUA

    private void OnEnable()
    {
        Lua.RegisterFunction("CheckNPCNameItemSpawn", this, SymbolExtensions.GetMethodInfo(() => CheckNPCNameItemSpawn(string.Empty)));
    }
    private void OnDisable()
    {
        Lua.UnregisterFunction("CheckNPCNameItemSpawn");
    }
}

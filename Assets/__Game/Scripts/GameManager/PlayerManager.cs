using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerPrefabMapping
{
    public PlayerType playerType;
    public GameObject playerPrefab;
}
public class PlayerManager : MonoBehaviour
{

    [SerializeField] private List<PlayerPrefabMapping> playerMappings = new List<PlayerPrefabMapping>();

    private Dictionary<PlayerType, GameObject> playerPrefabs = new Dictionary<PlayerType, GameObject>();
    Player playerClass;
    

    GameObject player;
    [SerializeField] Vector2 startingSpawnPoint; //TODO: For testing
    private static PlayerManager instance;
    public static PlayerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerManager>();
                if (instance == null)
                {
                    GameObject managerObject = new GameObject("PlayererManager");
                    instance = managerObject.AddComponent<PlayerManager>();
                }
            }
            return instance;
        }
    }

    public void SetPlayerType(PlayerType type)
    {
            player = GetPlayerPrefab(type);
            InstantiatePlayer(player);
    }
    private void Awake()
    {
        InitializePlayerPrefabs();
    }
    private void InitializePlayerPrefabs()
    {
        // Load and store partner prefabs in the dictionary
        foreach (var mapping in playerMappings)
        {
            if (mapping.playerPrefab != null && !playerPrefabs.ContainsKey(mapping.playerType))
            {
                playerPrefabs.Add(mapping.playerType, mapping.playerPrefab);
            }
            else
            {
                Debug.LogError("Invalid prefab mapping for " + mapping.playerType.ToString());
            }
        }
     
    }

    public GameObject GetPlayerPrefab(PlayerType playerType)
    {
        if (playerPrefabs.ContainsKey(playerType))
        {
            return playerPrefabs[playerType];
        }
        else
        {
            Debug.LogError("Prefab not found for " + playerType.ToString());
            return null;
        }
    }
    void InstantiatePlayer(GameObject player)
    {
        playerClass = player.GetComponent<Player>();
        Instantiate(player);
        player.transform.position = startingSpawnPoint;
        GameManager.Instance.SetPlayer(playerClass);
        player.SetActive(true);
    }
    public void MoveThePlayer(Transform playerTransform, Vector2 newPosition)
    {
        playerTransform.position = newPosition;
    }
}

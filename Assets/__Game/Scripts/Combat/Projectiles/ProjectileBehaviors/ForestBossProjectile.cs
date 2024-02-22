using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestBossProjectile : MonoBehaviour
{
    [SerializeField] float damageAmount;
    [SerializeField] float knockBackAmount;
    [SerializeField] float velocity;
    [SerializeField] AttackType attackType;
    [SerializeField] GameObject[] ChildProjectiles;
    
    private List<Vector2> directions = new List<Vector2>(); // List of directions
    private void Start()
    {
        directions.Add(new Vector2(0, 1));  // Up
        directions.Add(new Vector2(0, -1)); // Down
        directions.Add(new Vector2(1, 0));  // Right
        directions.Add(new Vector2(-1, 0)); // Left
        directions.Add(new Vector2(1, 1));  // Up-Right
        directions.Add(new Vector2(-1, -1));// Down-Left
        Shuffle(directions);
        
    }

    void Shoot(Vector2 position, Vector2 direction)
    {
        transform.position = position;
        int directionIndex = 0;
        foreach (GameObject projectile in ChildProjectiles)
        {
            if(directionIndex < directions.Count)
            {
                projectile.GetComponent<ForestBossChildProjectile>().SetParameters(directions[directionIndex], velocity, damageAmount, knockBackAmount, attackType);
                
                directionIndex++;
            }
            else
            {
                Debug.LogWarning("Not enough directions for all projectiles!");
                break;
            }
          
        }
    }
    // Fisher-Yates shuffle algorithm for shuffling the list

    void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    private void OnEnable()
    {
        ProjectileEventSystem.Instance.OnBossDirectionSet += Shoot;
    }
    private void OnDisable()
    {
        ProjectileEventSystem.Instance.OnBossDirectionSet -= Shoot;

    }
}

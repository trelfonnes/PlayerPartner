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
    [SerializeField] float activeTime = 2.5f;


    private List<Vector2> directions = new List<Vector2>(); // List of directions

    private void Start()
    {
        directions.Add(new Vector2(0, 1));  // Up
        directions.Add(new Vector2(0, -1)); // Down
        directions.Add(new Vector2(1, 0));  // Right
        directions.Add(new Vector2(-1, 0)); // Left
        directions.Add(new Vector2(1, 1));  // Up-Right
        directions.Add(new Vector2(-1, -1));// Down-Left
        directions.Add(new Vector2(1, -1));// down-right
        directions.Add(new Vector2(-1, 1));// up-left

        Shuffle(directions);
        SetInitialBehavior();  
    }
   
    void Shoot(Vector2 position, Vector2 direction)
        {
            transform.position = position;
            int directionIndex = 0;
            foreach (GameObject projectile in ChildProjectiles)
            {
                Debug.Log(ChildProjectiles.Length);

                if (directionIndex < directions.Count)
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
            StartCoroutine(DeactivateAfterTime());

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
        void OnEnable()
        {
            ProjectileEventSystem.Instance.OnBossDirectionSet += Shoot;
        }
        void OnDisable()
        {
            ProjectileEventSystem.Instance.OnBossDirectionSet -= Shoot;

        }
        IEnumerator DeactivateAfterTime()
        {
            yield return new WaitForSeconds(activeTime);
            foreach (GameObject projectile in ChildProjectiles)
            {
                projectile.transform.position = this.transform.position;

            }
            gameObject.SetActive(false);
        }
    void SetInitialBehavior() //this is used because a loop WILL NOT run the first use of the object.
    {
        GameObject projectile1 = ChildProjectiles[0];
        GameObject projectile2 = ChildProjectiles[1];
        GameObject projectile3 = ChildProjectiles[2];
        GameObject projectile4 = ChildProjectiles[3];
        GameObject projectile5 = ChildProjectiles[4];
        GameObject projectile6 = ChildProjectiles[5];
        GameObject projectile7 = ChildProjectiles[6];
        GameObject projectile8 = ChildProjectiles[7];

        projectile1.GetComponent<ForestBossChildProjectile>().SetParameters(directions[0], velocity, damageAmount, knockBackAmount, attackType);
        projectile2.GetComponent<ForestBossChildProjectile>().SetParameters(directions[1], velocity, damageAmount, knockBackAmount, attackType);
        projectile3.GetComponent<ForestBossChildProjectile>().SetParameters(directions[2], velocity, damageAmount, knockBackAmount, attackType);
        projectile4.GetComponent<ForestBossChildProjectile>().SetParameters(directions[3], velocity, damageAmount, knockBackAmount, attackType);
        projectile5.GetComponent<ForestBossChildProjectile>().SetParameters(directions[4], velocity, damageAmount, knockBackAmount, attackType);
        projectile6.GetComponent<ForestBossChildProjectile>().SetParameters(directions[5], velocity, damageAmount, knockBackAmount, attackType);
        projectile7.GetComponent<ForestBossChildProjectile>().SetParameters(directions[6], velocity, damageAmount, knockBackAmount, attackType);
        projectile8.GetComponent<ForestBossChildProjectile>().SetParameters(directions[7], velocity, damageAmount, knockBackAmount, attackType);

    }
}


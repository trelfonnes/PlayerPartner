using UnityEngine;

public class AddEvolutionPower : MonoBehaviour
{
    [SerializeField] int amount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Partner")
        {
            collision.GetComponentInChildren<IEvolutionPower>().IncreaseEP(amount);
            gameObject.SetActive(false);

        }

    }
}

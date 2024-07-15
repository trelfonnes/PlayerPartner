using UnityEngine;

public class AddEvolutionPower : MonoBehaviour
{
    [SerializeField] int amount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Partner") && !collision.isTrigger)
        {
            collision.GetComponentInChildren<IEvolutionPower>().IncreaseEP(amount);
            AudioManager.Instance.PlayAudioClip("CollectEP");

            gameObject.SetActive(false);

        }

    }
}

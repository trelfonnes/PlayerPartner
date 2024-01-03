using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AreaAttackObject : MonoBehaviour
{
    Collider2D[] colliders = new Collider2D[10];
    [SerializeField] protected LayerMask whatIsDamageable;
    Animator anim;
    float damage;
    AttackType attackType;
    float knockBack;
    Vector3 location;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PerformAreaAttack(Vector3 location, float size, float damage, AttackType attackType, float knockbackStrength)
    {
        this.attackType = attackType;
        this.damage = damage;
        knockBack = knockbackStrength;
        transform.position = location;
        
        CreateAndCheckCollider(size, location);

       
    }
    public void TriggerDammageAndKnockBack()
    {
        DealDamage(damage, attackType);

        ApplyKnockback(location, knockBack);
    }

    private void CreateAndCheckCollider(float newSize, Vector3 position)
    {

        
        Physics2D.OverlapCircleNonAlloc(position, newSize, colliders, whatIsDamageable);

    }

    private void DealDamage(float damage, AttackType attackType)
    {
        foreach (Collider2D collider in colliders)
        {
            if (collider != null && collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.Damage(damage, attackType);
            }
        }
    }
    private void ApplyKnockback(Vector2 position, float knockbackStrength)
    {
        foreach (Collider2D collider in colliders)
        {
            if (collider != null && collider.TryGetComponent(out IKnockBackable knockbackable))
            {
                knockbackable.KnockBack(position, knockbackStrength, 0, -1);
            }
        }
    }
    public void AnimationOver()
    {
        AreaAttackObjectFactory.Instance.ResetPooledObjects();

    }
}

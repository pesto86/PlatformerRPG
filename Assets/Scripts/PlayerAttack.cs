using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public CapsuleCollider2D attackHitBox;
    private bool canAttack = true;
    private int attackDamage = 5;

    void Awake()
    {
        attackHitBox.enabled = false;
    }

    void Attack()
    {
        StartCoroutine(AttackRoutine());
        Debug.Log("Player swings");

    }

    IEnumerator AttackRoutine()
        {
            attackHitBox.enabled = true;

            yield return new WaitForSeconds(0.2f);

            attackHitBox.enabled = false;

            yield return new WaitForSeconds(0.2f);

            canAttack = true;
        }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L) && canAttack == true)
        {
            Attack();
            canAttack = false;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.gameObject.TryGetComponent(out EnemyProfile enemyProfile))
            {
                enemyProfile.health -= attackDamage;
                enemyProfile.Damage(); 
                enemyProfile.SetHealth();
            }
        }
    }

    public void UpdateAttackDamage(int newDamage)
    {
        attackDamage = newDamage;
    }

}

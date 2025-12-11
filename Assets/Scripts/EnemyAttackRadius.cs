using UnityEngine;

public class EnemyAttackRadius : MonoBehaviour
{
    public EnemyAI enemy;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.playerPresent = true;
            enemy.patrolActive = false;
            enemy.chargeMode = true;
            enemy.returnMode = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    { 
        if (other.CompareTag("Player"))
        {
            enemy.playerPresent = false;
            enemy.patrolActive = false;
            enemy.chargeMode = false;
            enemy.returnMode = true;
        }
    }

}

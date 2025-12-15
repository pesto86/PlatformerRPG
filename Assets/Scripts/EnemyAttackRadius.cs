using UnityEngine;
using UnityEngine.UI;

public class EnemyAttackRadius : MonoBehaviour
{
    public EnemyAI enemy;
    public EnemyProfile enemyProfile;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.playerPresent = true;
            enemy.patrolActive = false;
            enemy.chargeMode = true;
            enemy.returnMode = false;

            enemyProfile.playerInRange = true;
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

            enemyProfile.playerInRange = false;
        }
    }

    

}

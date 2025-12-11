using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyAI enemy;
    public PlayerManager player;
    public EnemyProfile enemyProfile;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player is being damaged -1HP");
            player.Damage(enemyProfile.attackDamage);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
            enemy.playerPresent = true;
            enemy.patrolActive = false;
            enemy.chargeMode = true;
            enemy.returnMode = false;
    }
}

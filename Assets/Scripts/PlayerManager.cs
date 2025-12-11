using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public ParticleSystem bloodEffect;
    public PlayerHealth playerHealth;
    
    public void Damage(int damage)
    {
        bloodEffect.Play();
        playerHealth.OnDamageTaken(damage);
    }
    
    void Update()
    {
        
    }
}

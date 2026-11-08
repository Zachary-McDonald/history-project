using UnityEngine;

public class playerHealth : MonoBehaviour
{
    public float health = 100;
    

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Debug.Log("Death");
        }
    }
}

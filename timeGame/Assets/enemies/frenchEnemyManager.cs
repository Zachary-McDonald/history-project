using UnityEngine;

public class frenchEnemyManager : MonoBehaviour
{
    public float Health;
    public string Name;
    public int Damage;
    public float Speed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setHeatlh(float health)
    {
        Health = health;
    }
    public float getHeatlh()
    {
        return Health;
    }
    public frenchEnemyManager(float health, int damage,  string name, float speed)
    {
        Health = health;
        Name = name;
        Damage = damage;
        Speed = speed;
    }
    public void Die(GameObject enemy)
    {
        Destroy(enemy);
    }
    public void takeDamage(int amount, GameObject enemy)
    {
        Health = Health - amount;
        
        
        if(Health <= 0)
        {
            Die(enemy);
        }
    }
    public bool IsAlive()
    {
        if (Health > 0)
        {
            return true;
        }
        else
        {
            return false; 
        }
    }
    public int getDamage()
    {
        return Damage;
    }

}

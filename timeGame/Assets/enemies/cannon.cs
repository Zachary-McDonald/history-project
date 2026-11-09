using System.Collections;
using UnityEngine;

public class cannon : MonoBehaviour
{
    public GameObject  shooter;
    private Transform shootpoint;
    public GameObject player;
    private bool inRange;
    public GameObject cannonBall;
    private bool playerInRange;

    void Start()
    {
        shootpoint = shooter.transform;
        player = GameObject.FindGameObjectWithTag("Player");
    }

   
    void Update()
    {
        float distance = Vector3.Distance(
            transform.position,
            player.transform.position);

        Vector3 direction = shootpoint.position - player.transform.position;
        
        if(direction.x > 10 && direction.x < 50 && direction.z > -15 && direction.z < 15)
        {
            playerInRange = true;
            StartCoroutine(shoot());
        }
        else
        {
            playerInRange = false;
        }
       
        
    }
    IEnumerator shoot()
    {

        while (playerInRange)
        {
            yield return new WaitForSeconds(3f);

            if (!playerInRange)
                break;

            Debug.Log("Firing cannonball");

            Instantiate(cannonBall, shootpoint.position, shootpoint.rotation);
            yield return new WaitForSeconds(3f);
        }


    }
}

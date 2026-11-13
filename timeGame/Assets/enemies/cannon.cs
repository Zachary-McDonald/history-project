using System.Collections;
using UnityEngine;
using UnityEngine.Assemblies;

public class cannon : MonoBehaviour
{
   
    private bool hasattacked = true;
    public GameObject cannonBall;
    public Transform playerPos;
    public Transform cannonBallSpawn;
    public float range = 60;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        float distance = Vector3.Distance(transform.position, playerPos.position);
        Debug.Log(distance);

        if(hasattacked && distance < range)
        {

         
            GameObject currentBall = Instantiate(cannonBall, cannonBallSpawn.position, cannonBallSpawn.rotation);
            
            cannonball shoot = currentBall.GetComponent<cannonball>();
            shoot.Shoot(cannonBallSpawn);

            StartCoroutine(startShooting());

        }
    }
    private IEnumerator startShooting()
    {
        hasattacked = false;
        yield return new WaitForSeconds(3);
        hasattacked = true;
    }

}

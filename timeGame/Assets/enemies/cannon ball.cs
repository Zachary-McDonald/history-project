using System.Collections;
using UnityEngine;

public class cannonball : MonoBehaviour
{
    public float force;
    public Rigidbody rb;
    void Start()
    {
       
        if (rb != null)
        {
           

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Shoot(Transform direction)
    {
        rb.AddForce(direction.forward * force, ForceMode.Impulse);
        StartCoroutine(death());
    }
    public IEnumerator death()
    {
        yield return new WaitForSeconds(4);
        
        Destroy(gameObject);
    }
}

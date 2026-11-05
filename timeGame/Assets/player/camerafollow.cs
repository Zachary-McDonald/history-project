using UnityEngine;

public class camerafollow : MonoBehaviour
{
    public Transform orientationPoint;
    public Vector3 offset;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = orientationPoint.position + offset;
    }
}


using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    void Update()
    {
        
        Vector3 position = transform.position;
        position.x = 0f;
        position.y = target.transform.position.y;
        if (position.y > transform.position.y)
        {
            transform.position = position;
        }
    }
}

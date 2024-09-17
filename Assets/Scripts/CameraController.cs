using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float maxOffset = 5;

    [SerializeField] private float verticalOffset = 1;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = transform.position;
        Vector3 targetPosition = target.position;
        newPosition.y = target.position.y+verticalOffset;
        float offset = target.position.x - transform.position.x;
        if (Mathf.Abs(offset)>maxOffset)
        {
            
            newPosition.x = (Mathf.Sign(offset) > 0)
                ? (targetPosition.x - maxOffset)
                : (targetPosition.x + maxOffset);
        }

        transform.position = newPosition;
    }
}

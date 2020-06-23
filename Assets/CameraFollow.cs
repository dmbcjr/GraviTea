using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;

    //float lerpTime = 1f;

    //float currentLerpTime;

    public float cameraSmoothing = 0.200f;
    public Vector3 offset;

    Vector3 startPos;
    Vector3 endPos;

    void Start()
    {
        
        transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
    }

    
    void FixedUpdate()
    {
        endPos = new Vector3(player.transform.position.x, transform.position.y, transform.position.z) + offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, endPos, cameraSmoothing);

        transform.position = smoothedPosition;

        if (player.transform.rotation.z > .20)
        {
            offset.x = -3;
        }else if(player.transform.rotation.z < -.20)
        {
            offset.x = 3;
        }
        


        

    }
}

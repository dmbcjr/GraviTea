using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
   
    //**to be possibly used instead of editing mass 
    //ie force * jumpSpeed * deltaTime
    public float jumpSpeed = 5f;

    Rigidbody rb;

    AudioSource m_PlayerAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        m_PlayerAudioSource = GetComponent<AudioSource>();

    }


    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
       
    }

    private void PlayerMovement()
    {
        
       
        /*
         * Audio Notes
         * Audio of engine should sound in sync with thrust values
         * At the moment it just shuts off
         * The engine should sound like its ramping up and down
         * i.e lerp through values depending on whether the player is thrust
         * 0 being off 1 being 100% thrust/pitch or volume
         * */
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up);
            Debug.Log("is jump");

            if(!m_PlayerAudioSource.isPlaying)
            m_PlayerAudioSource.Play();
           

        }else 
        {
            
            
            m_PlayerAudioSource.Pause();
            
        }


        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.forward * jumpSpeed * Time.deltaTime);

            print("Rotating left");

        }else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-Vector3.forward * jumpSpeed * Time.deltaTime);
            print("Rotating right");
        }

        

    }
}

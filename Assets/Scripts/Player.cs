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

    float currentPitch;
    float lerpTime = 1f;
    float currentLerpTime;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        m_PlayerAudioSource = GetComponent<AudioSource>();
        m_PlayerAudioSource.Play();
    }


    // Update is called once per frame
    void Update()
    {
        Thrust();
        Rotate();
        Debug.Log(m_PlayerAudioSource.pitch);
    }


    private void Thrust()
    {
        /*
         * Audio Notes
         * Audio of engine should sound in sync with thrust values
         * At the moment it just shuts off
         * The engine should sound like its ramping up and down
         * i.e lerp through values depending on whether the player is thrust
         * 0 being off 1 being 100% thrust/pitch or volume
         */

        float xMin = 0.5f, xMax = 1.3f;

        m_PlayerAudioSource.pitch = Mathf.Clamp(m_PlayerAudioSource.pitch, xMin, xMax);

        


       

        if (Input.GetKeyDown(KeyCode.Space))
        {

            currentLerpTime = 0f;

        }

        if (Input.GetKey(KeyCode.Space))
        {

           
            rb.AddRelativeForce(Vector3.up);
            

            currentLerpTime += Time.deltaTime;

            float t = currentLerpTime / lerpTime;
            t = 1f - Mathf.Cos(t * Mathf.PI * 0.5f);

            m_PlayerAudioSource.pitch = Mathf.Lerp(m_PlayerAudioSource.pitch, xMax, t);

            
        }
        else
        {
            currentLerpTime = 0f;
            currentLerpTime += Time.deltaTime;

            float t= currentLerpTime / lerpTime;
            t = Mathf.Sin(t * Mathf.PI * 0.5f);

            m_PlayerAudioSource.pitch = Mathf.Lerp(m_PlayerAudioSource.pitch, xMin, t);

        }

       






    }
    private void Rotate()
    {
        rb.freezeRotation = true;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward);

           

        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward );
            
        }


        rb.freezeRotation = false;
    }
    
    
}

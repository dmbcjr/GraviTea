using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{

    //**to be possibly used instead of editing mass 
    //ie force * jumpSpeed * deltaTime

    

    Rigidbody rb;

    AudioSource m_PlayerAudioSource;

    ParticleSystem ps;

    float currentPitch;
    float lerpTime = 1f;
    float currentLerpTime;

    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float jumpSpeed = 5f;

    public float playerMaxHealth = 100;
    public float currentHealth;
    public HealthBar healthBar;
    public float fuelMultiplier = 2f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        m_PlayerAudioSource = GetComponent<AudioSource>();
        m_PlayerAudioSource.Play();
        ps = GetComponent<ParticleSystem>();

        currentHealth = playerMaxHealth;

        healthBar.SetMaxHealth(playerMaxHealth);
        LevelManager.Instance.HideEndMenu();
    }
    

    // Update is called once per frame
    void Update()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotationX| RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
        Thrust();
        Rotate();
        
       
    }

    private void HealthStatus()
    {
        if (playerMaxHealth>0)
        {
            playerMaxHealth -= Time.deltaTime *fuelMultiplier ;
            healthBar.SetHealth(playerMaxHealth);
            //Debug.Log("health is: " + playerMaxHealth);
        }
        else
        {

            playerFailed();

        }

    }

    private void playerFailed()
    {
        //hide player object
        
        //explosion at point of explosion
        ps.Play();
        

        //you died/restart screen
        LevelManager.Instance.ShowEndMenu();
    }

    void OnCollisionEnter(Collision collision)
    {
        
        switch (collision.collider.tag)
        {
            case "friendly":
               // Debug.Log("Okay");
                break;
            case "fuel":
                playerMaxHealth += 20f;
                break;
            default:
                playerFailed();
                break;
        }
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
        m_PlayerAudioSource.volume = 0;
        


       

        if (Input.GetKeyDown(KeyCode.Space))
        {

            currentLerpTime = 0f;

        }

        if (Input.GetKey(KeyCode.Space))
        {
            HealthStatus();

            rb.AddRelativeForce(Vector3.up * jumpSpeed);
            

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

            
        

        float rotationThisFrame = (Input.GetKey(KeyCode.Space)) ? rcsThrust/2 * Time.deltaTime : rcsThrust *  Time.deltaTime;


        //Debug.Log(rotationThisFrame);

        if (Input.GetKey(KeyCode.A))
        {
            rb.freezeRotation = true;

            transform.Rotate(Vector3.forward *rotationThisFrame);

           
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.freezeRotation = true;

            transform.Rotate(-Vector3.forward * rotationThisFrame );
            
        }

        
         /*
         else
         {
             rb.freezeRotation = true;
         }
         */

        
    }
    
    
}

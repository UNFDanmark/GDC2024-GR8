using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    [SerializeField] private GameObject PlayerObject;
    private bool WalkSoundHasPlayed = false;
    private CharacterController PlayerController;
    public AudioClip[] clipListPlayer;
    
    // 1. Footstep 
    // 2. Footstep
    // 3. Footstep

    //public AudioClip[] cliplistEnemy;
    
    private AudioSource WalkingAudio;
    // Start is called before the first frame update
    void Start()
    {
        // some of this is not used
      
        PlayerObject = GameObject.FindWithTag("Player");
        playerMovement = PlayerObject.GetComponent<PlayerMovement>();
        WalkingAudio = PlayerObject.GetComponent<AudioSource>();
        PlayerController = PlayerObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        
        if (playerMovement.PLAYSOUND_Running)
        {
            WalkingAudio.pitch = Random.Range(1.6f, 1.8f); // pitch for variation
            WalkingAudio.volume = Random.Range(0.07f, 0.09f); // Volume for Variation 
        }
        else
        {
            WalkingAudio.pitch = Random.Range(1.2f, 1.4f); // pitch for variation
            WalkingAudio.volume = Random.Range(0.03f, 0.05f); // Volume for Variation 
        }
        
        if (PlayerController.velocity.z != 0 && PlayerController.velocity.x != 0 && WalkSoundHasPlayed == false && PlayerController.isGrounded)
        {

            WalkingAudio.clip = clipListPlayer[Random.Range(0, clipListPlayer.Length)]; 
            // Inserts a random clip from list into player audio source
            
            print("paasdsadsadasd");


            if (playerMovement.PLAYSOUND_Walking)
            {
                WalkingAudio.Play();
                print("pluh");
                WalkSoundHasPlayed = true;

            }
            else
            {
                WalkingAudio.Play();
                print("pluh");
                WalkSoundHasPlayed = true;
            }
            
          
            
            
        }
        else if (PlayerController.velocity.z == 0 || PlayerController.velocity.x == 0)
        {
            WalkingAudio.Stop();
            WalkSoundHasPlayed = false;
        }

        if (PlayerController.isGrounded == false)
        {
            WalkingAudio.Stop();
        }
          
        

        
        
        
/*
        if (PlayerObject.GetComponent<PlayerMovement>().PLAYSOUND_Walking && WalkSoundHasPlayed == false )
        {
            WalkingAudio.Play();
            print("ur walki22n");
            WalkSoundHasPlayed = true;
        }
        else
        {
            WalkingAudio.Stop();
            WalkSoundHasPlayed = false;
        } */
    }
}

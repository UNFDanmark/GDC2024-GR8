using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    [SerializeField] private GameObject PlayerObject;
    private GameObject Shotgun;
    private bool WalkSoundHasPlayed = false;
    public AudioClip[] clipListPlayer;
    public AudioClip[] clipListShotgun;
    public GameObject EmptyGameObjectOnPLayer;

    private Rigidbody PlayerRB;
    // 1. Footstep 
    // 2. Footstep
    // 3. Footstep

    private GameObject Enemy;
    private GameObject Spawner;
    private EnemySpawner SpawnerScript;
    
    private AudioSource PlayerAudioSource;

    private AudioSource ShotgunAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        // some of this is not used
      
        PlayerObject = GameObject.FindWithTag("Player");
        playerMovement = PlayerObject.GetComponent<PlayerMovement>();
        PlayerAudioSource = PlayerObject.GetComponent<AudioSource>();
        Shotgun = GameObject.FindWithTag("Shotgun");
        PlayerRB = PlayerObject.GetComponent<Rigidbody>();
        ShotgunAudioSource = Shotgun.GetComponent<AudioSource>();
        //SpawnerScript = Spawner.GetComponent<EnemySpawner>();

    }

    // Update is called once per frame
    void Update()
    {

        
        
        
        
        
        if (playerMovement.PLAYSOUND_DASH)
        {
            EmptyGameObjectOnPLayer.GetComponent<AudioSource>().PlayOneShot(clipListPlayer[4]);
        }
        

        if (Shotgun.GetComponent<ShotgunTrigger>().PLAYSOUND_BS_ShotgunShoot)
        {

            
            ShotgunAudioSource.PlayOneShot(clipListShotgun[0]);
            print("shotgun just boomed");

        }

        if (playerMovement.PLAYSOUND_Jump)
        {
            print("GAYYYAYAYY");
            PlayerAudioSource.clip = clipListPlayer[3]; // [3] is currently the jump sound
            PlayerAudioSource.Play();
        }
        
        if (playerMovement.PLAYSOUND_Running)
        {
            PlayerAudioSource.pitch = Random.Range(1.6f, 1.8f); // pitch for variation
            PlayerAudioSource.volume = Random.Range(0.42f, 0.54f); // Volume for Variation 
        }
        else
        {
            PlayerAudioSource.pitch = Random.Range(1.2f, 1.4f); // pitch for variation
            PlayerAudioSource.volume = Random.Range(0.3f, 0.39f); // Volume for Variation 
            
        }
        
        
        
        
        if (PlayerRB.velocity.z > 0.05f && PlayerRB.velocity.x > 0.05f && WalkSoundHasPlayed == false)
        {

            PlayerAudioSource.clip = clipListPlayer[Random.Range(0, 2)]; 
            // Inserts a random clip from list into player audio source
            
            print("paasdsadsadasd");
            print(PlayerRB.velocity);


            if (playerMovement.PLAYSOUND_Walking)
            {
                PlayerAudioSource.Play();
                print("pluh");
                WalkSoundHasPlayed = true;

            }
            else
            {
                PlayerAudioSource.Play();
                print("pluh");
                WalkSoundHasPlayed = true;
            }
            
          
            
            
        }
        else if (PlayerRB.velocity.z <= 0.05f || PlayerRB.velocity.x <= 0.05f)
        {
            PlayerAudioSource.Stop();
            WalkSoundHasPlayed = false;
        }

        if (playerMovement.isGrounded == false)
        {
            PlayerAudioSource.Stop();
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

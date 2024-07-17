using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public GameObject PlayerObject;
    public GameObject ShotgunObject;
    public GameObject DashAudioSourceObject;
    public GameObject JumpAudioSourceObject;

    private PlayerMovement _playerMovement;
    private ShotgunTrigger _ShotgunTrigger;
    private Rigidbody _playerRB;

    private AudioSource PlayerAudioSource;
    private AudioSource ShotgunAudioSource;
    private AudioSource DashAudioSource;
    private AudioSource jumpAudioSource;
    
    
    // lists of different clips

    public AudioClip[] PlayerClipList;
    public AudioClip[] ShotgunClipList;
    
    // random stuff

    [SerializeField] private bool WalkSoundHasPlayed;
    
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
        ShotgunObject = GameObject.FindGameObjectWithTag("Shotgun");
        DashAudioSourceObject = GameObject.FindGameObjectWithTag("DashAudioSource");
        JumpAudioSourceObject = GameObject.FindGameObjectWithTag("JumpAudioSource");
        _playerRB = PlayerObject.GetComponent<Rigidbody>();
        // get scripts

        _playerMovement = PlayerObject.GetComponent<PlayerMovement>();
        _ShotgunTrigger = ShotgunObject.GetComponent<ShotgunTrigger>();
        PlayerAudioSource = PlayerObject.GetComponent<AudioSource>();
        ShotgunAudioSource = _ShotgunTrigger.GetComponent<AudioSource>();
        DashAudioSource = DashAudioSourceObject.GetComponent<AudioSource>();
        jumpAudioSource = JumpAudioSourceObject.GetComponent<AudioSource>();

    }

    
    // Update is called once per frame
    void Update()
    {

        if (_playerRB.velocity.x != 0 || _playerRB.velocity.z != 0)
        {

            if (WalkSoundHasPlayed == false && PlayerAudioSource.isPlaying == false && _playerMovement.isGrounded)
            {
                
                PlayerAudioSource.clip = PlayerClipList[Random.Range(0, 3)];
                PlayerAudioSource.pitch = Random.Range(1.2f, 1.4f); // pitch for variation
                PlayerAudioSource.volume = Random.Range(0.3f, 0.39f); // Volume for Variation 
                PlayerAudioSource.Play();
                print("PLUH walk");

                if (_playerMovement.PLAYSOUND_Running)
                {
                    PlayerAudioSource.clip = PlayerClipList[Random.Range(0, 2)];
                    PlayerAudioSource.pitch = Random.Range(1.6f, 1.8f); // pitch for variation
                    PlayerAudioSource.volume = Random.Range(0.42f, 0.54f); // Volume for Variation 
                    PlayerAudioSource.Play();
                       
                }

                WalkSoundHasPlayed = true;
                

            }

        }
        else
        {
            PlayerAudioSource.Stop();
            print("PLEASSEEE stay still");
            WalkSoundHasPlayed = false;

        }


        if (_ShotgunTrigger.PLAYSOUND_BS_ShotgunShoot)
        {

            ShotgunAudioSource.PlayOneShot(ShotgunClipList[0]);
        } else if (_ShotgunTrigger.PLAYSOUND_PiercingLight)
        {
            //ShotgunAudioSource.PlayOneShot(ShotgunClipList[1]);
            ShotgunAudioSource.clip = ShotgunClipList[1];
            ShotgunAudioSource.Play();
        }

        if (_playerMovement.isDashing)
        {
            DashAudioSource.PlayOneShot(PlayerClipList[4]);
           
        }
        if (_playerMovement.PLAYSOUND_Jump)
        {
            DashAudioSource.PlayOneShot(PlayerClipList[3]);
           
        }

        /*if (_playerRB.velocity.x <= 0.05f || _playerRB.velocity.z <= 0.05f)
        {
            
            PlayerAudioSource.Stop();
            WalkSoundHasPlayed = false;
            print("PLEASSEEE stay still");

        }*/

    }
}

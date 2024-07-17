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

    }

    
    // Update is called once per frame
    void Update()
    {

        if (_playerRB.velocity.x > 0 || _playerRB.velocity.z > 0)
        {

            if (_playerMovement.PLAYSOUND_Walking && WalkSoundHasPlayed)
            {

                //PlayerAudioSource.clip = PlayerClipList[Random.Range(0, 3)];
                PlayerAudioSource.PlayOneShot(PlayerClipList[Random.Range(0, 3)]);
                PlayerAudioSource.pitch = Random.Range(0.3f, 0.6f);


            }

        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private PlayerMovement playerMovement;
    [SerializeField] private GameObject PlayerObject;

    private AudioSource WalkingAudio;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        PlayerObject = GameObject.FindWithTag("Player");
        WalkingAudio = PlayerObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.isWalking)
        {
            WalkingAudio.Play();
        }
    }
}

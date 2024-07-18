using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class testscript : MonoBehaviour
{
    NavMeshAgent move;

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        move = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        move.SetDestination(player.transform.position);
    }
}

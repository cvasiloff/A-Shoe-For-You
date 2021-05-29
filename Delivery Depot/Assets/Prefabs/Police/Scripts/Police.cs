using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Police : Entity
{
    [Header("Do Not Set In Inspector")]
    PlayerController player;
    NavMeshAgent myCop;
    ManageGame gm;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        myCop = this.GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerController>();
        gm = FindObjectOfType<ManageGame>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gm.isStarted && !death)
            myCop.destination = player.transform.position;

        if(death)
        {
            myCop.destination = this.transform.position;
        }
    }
}

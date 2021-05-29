using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Police : Entity
{
    [Header("Do Not Set In Inspector")]
    PlayerController player;
    NavMeshAgent myCop;
    bool escape;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        myCop = this.GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        
        if(death && !escape)
        {
            StartCoroutine(RemovePolice());
            escape = true;
        }
    }

    IEnumerator RemovePolice()
    {
        gm.AddScore(100);
        gm.CallPolice();
        this.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        Destroy(this.transform.GetChild(0).gameObject);
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }

    public override void Move()
    {
        if (gm.isStarted && !death)
            myCop.destination = player.transform.position;

        if (death || player.death)
        {
            myCop.destination = this.transform.position;
        }
    }
}

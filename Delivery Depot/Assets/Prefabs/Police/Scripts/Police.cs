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
    bool updateChase = true;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        myCop = this.GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerController>();

        GameObject[] stations = GameObject.FindGameObjectsWithTag("Station");

        for(int i = 0; i < stations.Length; i++)
        {
            Physics.IgnoreCollision(stations[i].GetComponent<Collider>(), this.GetComponent<Collider>());
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        if(updateChase)
        {
            updateChase = false;
            StartCoroutine(ChaseCooldown());
            Move();
        }
        
        
        if(death && !escape)
        {
            StartCoroutine(RemovePolice());
            escape = true;
        }
    }

    IEnumerator ChaseCooldown()
    {
        yield return new WaitForSeconds(.1f);
        updateChase = true;
    }

    IEnumerator RemovePolice()
    {
        gm.AddScore(30);
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

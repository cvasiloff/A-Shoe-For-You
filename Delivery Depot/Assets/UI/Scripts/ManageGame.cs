using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageGame : MonoBehaviour
{
    
    [Header("Set Variables")]
    public Text score;
    public Text countdown;
    public GameObject policePrefab;

    [Header("Do not Set Variables")]
    public bool isStarted;
    private int scoreVar = 0;
    public bool canAdd = true;
    private PlayerController player;
    public GameObject[] policeSpawns;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BeginCountdown());
        player = FindObjectOfType<PlayerController>();
        policeSpawns = GameObject.FindGameObjectsWithTag("Spawn");
    }

    // Update is called once per frame
    void Update()
    {
        
        if(isStarted && canAdd && !player.death)
        {
            if (scoreVar % 12 == 0 && scoreVar != 0)
            {
                CallPolice();
            }

            StartCoroutine(AliveScore());
        }
    }

    public void CallPolice()
    {
        if(!player.death)
        {
            GameObject cop = GameObject.Instantiate(policePrefab, policeSpawns[Random.Range(0, policeSpawns.Length)].transform.position, Quaternion.identity);
        }
    }

    public IEnumerator AliveScore()
    {
        canAdd = false;
        yield return new WaitForSeconds(1);
        canAdd = true;
        AddScore(1);
    }

    public void AddScore(int value)
    {
        if(!player.death)
        {
            scoreVar += value;
            score.text = "Score: " + scoreVar;
        }
        
    }

    public IEnumerator BeginCountdown()
    {
        yield return new WaitForSeconds(1);
        countdown.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(1);
        countdown.text = "2";
        yield return new WaitForSeconds(1);
        countdown.text = "1";
        yield return new WaitForSeconds(1);
        countdown.text = "GO!";

        
        yield return new WaitForSeconds(.5f);
        isStarted = true;
        score.gameObject.SetActive(true);
        countdown.gameObject.SetActive(false);
        

    }
}

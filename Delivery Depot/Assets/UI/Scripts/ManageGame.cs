using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManageGame : MonoBehaviour
{
    
    [Header("Set Variables")]
    public Text score;
    public Text finalScore;
    public Text countdown;
    public Image progressBar;
    public Image brakeBar;
    public GameObject pausePanel;
    public GameObject endPanel;
    public GameObject policePrefab;

    [Header("Do not Set Variables")]
    public bool isStarted;
    public int scoreVar = 0;
    public bool canAdd = true;
    public bool isPaused;
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

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!player.death)
            {
                PauseGame();
            }
            

        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void PauseGame()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            pausePanel.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pausePanel.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void CallPolice()
    {
        if (!player.death)
        {
            int station = FindStation();
            
            GameObject.Instantiate(policePrefab, policeSpawns[station].transform.position, Quaternion.identity);
        }
        
    }

    public int FindStation()
    {
        float temp = 0;
        int station = 0;

        for(int i = 0; i < policeSpawns.Length; i++)
        {
            float dist = DistCheck(player.transform.position, policeSpawns[i].transform.position);

            if(i == 0 || dist <= temp)
            {
                temp = dist;
                station = i;
            }
        }

        return station;
    }

    public float DistCheck(Vector3 playerPos, Vector3 stationPos)
    {
        return (Mathf.Abs((playerPos - stationPos).magnitude));
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

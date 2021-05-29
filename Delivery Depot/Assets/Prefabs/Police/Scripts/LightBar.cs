using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBar : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject redBar;
    public GameObject blueBar;
    public Material redMat;
    public Material blueMat;
    public Material nullMat;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BeginLights());
    }

    IEnumerator BeginLights()
    {
        while(true)
        {
            
            redBar.GetComponent<MeshRenderer>().material = redMat;
            blueBar.GetComponent<MeshRenderer>().material = nullMat;
            yield return new WaitForSeconds(.3f);
            redBar.GetComponent<MeshRenderer>().material = nullMat;
            blueBar.GetComponent<MeshRenderer>().material = blueMat;
            yield return new WaitForSeconds(.3f);
        }
    }
}

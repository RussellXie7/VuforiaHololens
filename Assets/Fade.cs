using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour {

    public GameObject imageTarget;
    public GameObject camera;

    /// <summary>
    /// To track the state about if we should go dim or go solid.
    /// </summary>
    public enum ChangeStates
    {
        goDim,
        goSolid
    }

    public ChangeStates CurrState
    {
        get;
        set;
    }

    private GameObject theCanvas;
    private Vector3 offset;

	// Use this for initialization
	void Start () {
        CurrState = ChangeStates.goDim;
        theCanvas = transform.FindDeepChild("Canvas").gameObject;
        offset = transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if(CurrState == ChangeStates.goDim)
        {
            MakeDim();

        }
        else if(CurrState == ChangeStates.goSolid)
        {
            MakeSolid();
            transform.position = Vector3.Lerp(transform.position, imageTarget.transform.position, 0.2f);
           transform.forward = Vector3.Lerp(transform.forward, imageTarget.transform.forward , 0.2f);

        }
	}

    void OnTrack()
    {
        StopCoroutine(GoDimWait());
        CurrState = ChangeStates.goSolid;
    }

    void LostTrack()
    {
        //CurrState = ChangeStates.goDim;
        StartCoroutine(GoDimWait());
    }

    IEnumerator GoDimWait()
    {
        float counter = 0;

        while (true)
        {
            counter += Time.deltaTime;
            if(counter > 3)
            {
                break;
            }
            yield return null;
        }

        CurrState = ChangeStates.goDim;

        yield return null;

    }

    void MakeDim()
    {
        foreach (MeshRenderer renderer in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            if (renderer.material.color.a > 0.1)
            {
                theCanvas.SetActive(true);
            }
            else
            {
                theCanvas.SetActive(false);
            }

            if (renderer.material.color.a > 0)
            {
                Debug.Log("Getting Dimmer");
                renderer.material.color -= new Color(0, 0, 0, .10f);
            }
        }
    }

    void MakeSolid()
    {
        foreach (MeshRenderer renderer in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            if (renderer.material.color.a > 0.1)
            {
                theCanvas.SetActive(true);
            }
            else
            {
                theCanvas.SetActive(false);
            }

            if (renderer.material.color.a < 1)
            {
                Debug.Log("Getting Solide");
                renderer.material.color += new Color(0, 0, 0, .10f);
            }
        }
    }

}

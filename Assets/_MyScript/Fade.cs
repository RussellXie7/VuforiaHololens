using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour {

    public GameObject imageTarget;
    public GameObject camera;
    public float bufferTime = 1f;

    private bool waiting = false;
    private Coroutine lastRoutine;
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
    private float adjusted_x;
    private float adjusted_y;

	// Use this for initialization
	void Start () {
        CurrState = ChangeStates.goDim;
        theCanvas = transform.FindDeepChild("Canvas").gameObject;
        offset = transform.position;
        lastRoutine = null;
    }
	
	// Update is called once per frame
	void Update () {
        if(CurrState == ChangeStates.goDim && !waiting)
        {
            MakeDim();

        }
        else if(CurrState == ChangeStates.goSolid)
        {
            MakeSolid();
            transform.position = Vector3.Lerp(transform.position, imageTarget.transform.position, 0.2f);
            //Vector3 relativePos = camera.transform.position - transform.position;
            //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(imageTarget.transform.forward), 0.2f);
            //transform.forward = Vector3.Lerp(transform.forward, imageTarget.transform.forward, Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, imageTarget.transform.rotation, 0.2f);
        }
        else if (waiting)
        {
            MakeSolid();
        }

        //FixRotation();

    }

    void FixRotation()
    {
        //if (!(transform.localEulerAngles.x < standard_x_rot_max && transform.localEulerAngles.x > standard_x_rot_min))
        //{
        //    if (transform.localEulerAngles.x >= standard_x_rot_max)
        //    {
        //        adjusted_x = standard_x_rot_max;
        //    }
        //    else
        //    {
        //        adjusted_x = standard_x_rot_min;
        //    }
        //}
        //else
        //{
        //    adjusted_x = transform.localEulerAngles.x;
        //}

        //if (!(transform.localEulerAngles.y < standard_y_rot_max && transform.localEulerAngles.y > standard_y_rot_min))
        //{
        //    if (transform.localEulerAngles.y >= standard_y_rot_max)
        //    {
        //        adjusted_y = standard_y_rot_max;
        //    }
        //    else
        //    {
        //        adjusted_y = standard_y_rot_min;
        //    }
        //}
        //else
        //{
        //    adjusted_y = transform.localEulerAngles.y;
        //}

        transform.localRotation = Quaternion.Euler(-90,0,0);
    }

    void OnTrack()
    {
        if (lastRoutine != null)
        {
            StopCoroutine(lastRoutine);
            lastRoutine = null;
            waiting = false;
        }
        CurrState = ChangeStates.goSolid;
    }
    
    void LostTrack()
    {
        CurrState = ChangeStates.goDim;
        lastRoutine = StartCoroutine(BufferWait());
    }

    IEnumerator BufferWait()
    {
        float counter = 0;
        waiting = true;

        while (counter < bufferTime)
        {
            //Debug.Log(counter);
            counter += Time.deltaTime;
        }

        waiting = false;

        yield return null;

    }

    void MakeDim()
    {
        foreach (MeshRenderer renderer in gameObject.GetComponentsInChildren<MeshRenderer>())
        {


            if (renderer.material.color.a > 0)
            {
                Debug.Log("Getting Dimmer");
                renderer.material.color -= new Color(0, 0, 0, .10f);
            }
            if (renderer.material.color.a > 0.1)
            {
                theCanvas.SetActive(true);
            }
            else
            {
                theCanvas.SetActive(false);
            }
        }
    }

    void MakeSolid()
    {
        foreach (MeshRenderer renderer in gameObject.GetComponentsInChildren<MeshRenderer>())
        {


            if (renderer.material.color.a < 1)
            {
                Debug.Log("Getting Solide");
                renderer.material.color += new Color(0, 0, 0, .10f);
            }

            if (renderer.material.color.a > 0.1)
            {
                theCanvas.SetActive(true);
            }
            else
            {
                theCanvas.SetActive(false);
            }
        }
    }

}

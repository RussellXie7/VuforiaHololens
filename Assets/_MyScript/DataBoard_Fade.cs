using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBoard_Fade : MonoBehaviour {

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

    // To track the position of this Board
    public GameObject imageTarget;
    // to create some animation
    public float bufferTime = 0.5f;

    private bool waiting = false;
    private Coroutine lastRoutine;
    // fix the position of board after click
    [HideInInspector]
    public bool positionFixed = false;
    private GameObject theCanvas;
    private Vector3 offset;
    private float adjusted_x;
    private float adjusted_y;


    // Use this for initialization
    void Start()
    {
        CurrState = ChangeStates.goDim;
        theCanvas = transform.FindDeepChild("AirTapOnMe").gameObject;
        offset = transform.position;
        lastRoutine = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (!positionFixed)
        {
            if (CurrState == ChangeStates.goDim && !waiting)
            {
                MakeDim();

            }
            else if (CurrState == ChangeStates.goSolid)
            {
                MakeSolid();
                transform.position = Vector3.Lerp(transform.position, imageTarget.transform.position, 0.2f);
                transform.rotation = Quaternion.Lerp(transform.rotation, imageTarget.transform.rotation, 0.2f);
                //transform.position = imageTarget.transform.position;
                //transform.rotation = imageTarget.transform.rotation;
            }
            else if (waiting)
            {
                MakeSolid();
            }
        }

        //FixRotation();

    }

    void FixPosition()
    {
        Debug.Log("Fix Position Is Getting Called!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        positionFixed = true;
        gameObject.SendMessageUpwards("FadeText","fromPWC");
    }

    void GoBack()
    {
        Debug.Log("Going Back");
        imageTarget.SetActive(true);
        imageTarget.SendMessageUpwards("CheckFound");
        // send message to fade back and enable transform tracking
        gameObject.SendMessage("FadeText", "fromGoBack");

        // set position fixed to false;
        positionFixed = false;

        // disable the other two image target -- looks no need

    }

    void OnHover()
    {
        Debug.Log("OnHover is called");
        if (lastRoutine != null)
        {
            StopCoroutine(lastRoutine);
            lastRoutine = null;
            waiting = false;
        }
        CurrState = ChangeStates.goSolid;
    }

    void OnExit()
    {
        CurrState = ChangeStates.goDim;
        if (lastRoutine == null)
        {
            lastRoutine = StartCoroutine(BufferWait());
        }
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
                //Debug.Log("Getting Dimmer");
                renderer.material.color -= new Color(0, 0, 0, .15f);
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
                //Debug.Log("Getting Solide");
                renderer.material.color += new Color(0, 0, 0, .05f);
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

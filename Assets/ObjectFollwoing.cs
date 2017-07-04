using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollwoing : MonoBehaviour {

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

    public GameObject imageTarget;


	// Use this for initialization
	void Start () {
        CurrState = ChangeStates.goDim;
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
            transform.position = Vector3.Lerp(transform.position, imageTarget.transform.position, 0.1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, imageTarget.transform.rotation, 0.2f);

        }
	}

    void OnTrack()
    {
        Debug.Log("The object " + gameObject.name + "is tracked. ");
        CurrState = ChangeStates.goSolid;
    }

    void OnLost()
    {
        Debug.Log("The object " + gameObject.name + "is lost. ");
        CurrState = ChangeStates.goDim;
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
        }
    }

}

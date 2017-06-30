using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBoard_ColliderAction : MonoBehaviour {


    private void Start()
    {

        Debug.Log("Collider Action enabled");
    }

    void OnSelect()
    {
        //gameObject.SendMessageUpwards("FixPosition");
        Debug.Log("The hitted Object is  " + gameObject.name);

    }

    void EnableCollider()
    {
        foreach(BoxCollider child in transform.GetComponentsInChildren<BoxCollider>())
        {
            child.enabled = true;
        }
    }
}

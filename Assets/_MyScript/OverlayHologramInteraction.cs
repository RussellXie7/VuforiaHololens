using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayHologramInteraction : MonoBehaviour {

	// Use this for initialization
	void Start () {
        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void EnableHologram()
    {
        foreach (Transform child in gameObject.transform)
        {
            Debug.Log("Entered~~~~~~~~~~~~~~~~" + child.gameObject.name);

            child.gameObject.SetActive(true);

        }
    }

}

using System.Collections;
using System.Collections.Generic;
using Valve.VR.InteractionSystem;
using UnityEngine;

public class Teleport : MonoBehaviour {

    private Hand leftHand;
    private Hand rightHand;

	void Start () {

       leftHand = GameObject.Find("Controller(left)").GetComponent<Hand>();
       rightHand = GameObject.Find("Controller(right)").GetComponent<Hand>();

    }
	
	void Update () {

        TeleportPlayer();
	}

    public void TeleportPlayer()
    {
        if (leftHand.controller.GetHairTriggerDown())
        {
            print("Works");
        }
    }
}

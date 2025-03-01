﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneControl : MultiContolBase
{
    Valve.VR.SteamVR_Action_Single squeezeAction;
    public DroneControl(Transform t, Transform look) : base(t, look)
    {

    }
    float speed = .0f, groundY = .0f;
    Transform  player, wing, wing2;
    Vector3 flyVector, playerOffset;

    //Transform test0, test1;

    // Start is called before the first frame update
    public override void Init()
    {
        wing = transform.Find("wing");
        wing2 = transform.Find("wing2");

       // test0 = GameObject.Find("droneC").transform;
       // test1 = GameObject.Find("droneC2").transform;
    }

    //public override void Awake()
    //{

    //    //player = HUDCamera.parent;
    //    //playerOffset = HUDCamera.localPosition;
    //}

    public void GiveInputAction(Valve.VR.SteamVR_Action_Single squeeze)
    {
        squeezeAction = squeeze;
    }

    // Update is called once per frame
    public override void Update(float dt)
    {

        wing.transform.rotation = leftHand.rotation;
        wing2.transform.rotation = rightHand.rotation;
        //if (test0.transform.up.y > 0) wing.transform.rotation = test0.rotation;
        //if (test1.transform.up.y > 0) wing2.transform.rotation = test1.rotation;
        //Debug.Log(test0.transform.up);


        bool leftInput = squeezeAction.GetAxis(Valve.VR.SteamVR_Input_Sources.LeftHand) > 0.5f || Input.GetKey(KeyCode.A);
        bool rightInput = squeezeAction.GetAxis(Valve.VR.SteamVR_Input_Sources.RightHand) > 0.5f || Input.GetKey(KeyCode.S);
        if (!leftInput && !rightInput)
        {
            flyVector = new Vector3(0, 0, 0);
            speed = .0f;
            Vector3 nextPos = transform.position + dt * new Vector3(0, -5.0f, 0);
            if (nextPos.y < groundY) nextPos = new Vector3(transform.position.x, groundY, transform.position.z);
            transform.position = nextPos;
            //player.position = transform.position - playerOffset;
        }
        else {
            if (leftInput) flyVector += dt * wing.GetChild(0).up;
            if (rightInput) flyVector += dt * wing2.GetChild(0).up;
            speed += dt * 5.0f;
            if(speed > 5.0f)speed = 5.0f;
            transform.position += speed * dt * flyVector;

            // player.position = transform.position - playerOffset;
        }
        Debug.Log(flyVector);
    }
}

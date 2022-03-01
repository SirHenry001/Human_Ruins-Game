using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    //THIS SCRIPT IS ATTACHED TO PLAYER

    // CAMERA TARGETS WHICH IS CHANGED BASED ON WHICH TRIGGER PLAYER ACTIVATES
    public Transform cam1Target;
    public Transform cam2Target;
    public Transform cam3Target;
    public Transform player;

    // VIARIABLES FOR COLLIDERS WHICH IS ACTIVATED TO LIMIT PLAYER MOVEMENT
    public GameObject waweCollider1;
    public GameObject waweCollider2;
    public GameObject bossCollider1;
    public GameObject bossCollider2;
    public GameObject followCamTrigger2;

    // ACCESS TO CAMERA SCRIPT
    public CameraController cameraController;

    // ACCESS TO PARALLAX CAMERA SCRIPTS
    public ParallaxCamera parallaxCamera;
    //public ParallaxCamera parallaxCamera2;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        // PLAYER ACTIVATES STARTCAMERA TARGET
        if (collision.gameObject.tag == "CameraOne")
        {
            cameraController.target = cam1Target;
            parallaxCamera.enabled = false;
            //parallaxCamera2.enabled = false;
        }

        // PLAYER ACTIVATES WAWE CAMERA TARGET
        if (collision.gameObject.tag == "CameraTwo")
        {
            cameraController.target = cam2Target;
            waweCollider1.SetActive(true);
            waweCollider2.SetActive(true);
            parallaxCamera.enabled = false;
            //parallaxCamera2.enabled = false;
        }

        // PLAYER ACTIVATES BOSSCAMERA TARGET
        if (collision.gameObject.tag == "CameraThree")
        {
            cameraController.target = cam3Target;
            parallaxCamera.enabled = false;
            //parallaxCamera2.enabled = false;
        }

        // PLAYER ACTIVATES CAMERATARGER WHICH FOLLOWS PLAYER
        if (collision.gameObject.tag == "FollowCam")
        {
            cameraController.target = player;
            //parallaxCamera.enabled = true;
            //parallaxCamera2.enabled = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        

        // TELL SCRIPT VARIABLES WHERE TO FIND THE SPECIFIC SCRIPT
        cameraController = GameObject.Find("CameraTarget").GetComponent<CameraController>();
        parallaxCamera = GameObject.Find("ForeGround").GetComponent<ParallaxCamera>();
        //parallaxCamera2 = GameObject.Find("BackGround").GetComponent<ParallaxCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

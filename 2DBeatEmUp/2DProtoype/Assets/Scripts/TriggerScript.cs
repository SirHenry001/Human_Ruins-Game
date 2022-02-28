using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{

    public Transform cam1Target;
    public Transform cam2Target;
    public Transform cam3Target;
    public Transform player;

    public GameObject waweCollider1;
    public GameObject waweCollider2;
    public GameObject bossCollider1;
    public GameObject bossCollider2;

    public CameraController cameraController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "CameraOne")
        {
            cameraController.target = cam1Target;
        }

        if (collision.gameObject.tag == "CameraTwo")
        {
            cameraController.target = cam2Target;
        }

        if (collision.gameObject.tag == "CameraThree")
        {
            cameraController.target = cam3Target;
        }

        if (collision.gameObject.tag == "FollowCam")
        {
            cameraController.target = player;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cameraController = GameObject.Find("CameraTarget").GetComponent<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

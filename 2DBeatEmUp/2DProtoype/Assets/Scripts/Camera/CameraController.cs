using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // VARIABLES FOR CAMERA MOVEMENT
    public Vector3 movement;
    public Transform target;
    public float cameraSpeedX;
    public float cameraSpeedY;

    //VARIABLES FOR BG CAMERA
    public BackgroundCameraScript bgScript;

    // Start is called before the first frame update
    void Start()
    {
        bgScript = GameObject.Find("CameraBG").GetComponent<BackgroundCameraScript>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // ADJUSTING CAMERA MOVEMENT
        movement = transform.position;
        movement.x = Mathf.Lerp(movement.x, target.position.x, cameraSpeedX * Time.deltaTime);
        movement.y = Mathf.Lerp(movement.y, target.position.y, cameraSpeedY * Time.deltaTime);

        // TELL CAMERA TO MOVE
        transform.position = movement;

        bgScript.speed = GetComponent<Rigidbody2D>().velocity.x / 3f;
    }

}

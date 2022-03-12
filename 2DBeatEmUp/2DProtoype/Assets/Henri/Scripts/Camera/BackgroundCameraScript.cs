using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCameraScript : MonoBehaviour
{

    public Transform target;

    public float minX;
    public float maxX;
    public float offset;

    public float scrollSpeed;

    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector2.right * speed * Time.deltaTime);
        transform.position = new Vector3(target.position.x / scrollSpeed - offset, transform.position.y, transform.position.z);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), transform.position.y, transform.position.z);

    }
}

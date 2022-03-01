using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCameraScript : MonoBehaviour
{

    public float speed;

    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

    }
}

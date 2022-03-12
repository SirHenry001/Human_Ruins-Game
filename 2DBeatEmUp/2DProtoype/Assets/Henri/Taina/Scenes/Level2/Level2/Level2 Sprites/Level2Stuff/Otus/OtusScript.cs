using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtusScript : MonoBehaviour
{                           
    public float speed;
    public float despawnTime;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, despawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}

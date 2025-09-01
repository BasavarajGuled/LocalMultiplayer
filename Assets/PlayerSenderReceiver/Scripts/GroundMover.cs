using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-Vector3.forward * 5f * Time.deltaTime);
        if (transform.position.z <= -40)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 60f);
            //Time.timeScale = 0f;
        }
    }
}

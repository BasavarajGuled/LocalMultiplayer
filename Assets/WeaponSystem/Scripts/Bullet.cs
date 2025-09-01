using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void OnEnable()
    {
        Invoke("DisableBullet", 1f);
    }

    void DisableBullet()
    {
        gameObject.SetActive(false);
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
    }


}

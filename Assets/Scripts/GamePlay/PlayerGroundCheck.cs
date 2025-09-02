using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundCheck : MonoBehaviour
{
    public bool isGrounded = false;
    void OnCollisionStay(Collision collision)
    {
        if (!isGrounded)
            isGrounded = true;
    }

    void OnCollisionExit(Collision collision)
    {
        if (isGrounded)
            isGrounded = false;
    }
}

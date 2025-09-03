using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMover : MonoBehaviour
{
    public Transform target;
    void Update()
    {
        if (target == null)
            return;

        Vector3 direction = Vector3.forward; // Axis you want sign relative to
        Vector3 toTarget = target.position - transform.position;

        float signedDistance = Vector3.Dot(toTarget, direction);
        Debug.Log(signedDistance);

        if (signedDistance > 20f)
        {
            transform.position += new Vector3(0f, 0f, 100f);
        }
    }
}

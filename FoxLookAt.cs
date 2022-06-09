using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxLookAt : MonoBehaviour
{
    public Transform target;
    private Vector3 defaultPos;
    private Vector3 upwards = new Vector3(0, 0, -30);

    private void Start()
    {
        defaultPos = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookAt = transform.position - target.transform.position;
        transform.right = lookAt.normalized;
        
        transform.Rotate(upwards, Space.Self);
    }
}

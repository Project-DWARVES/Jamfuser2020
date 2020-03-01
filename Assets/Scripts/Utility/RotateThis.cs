using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Rotates Objects
public class RotateThis : MonoBehaviour
{
    public float rotateSpeed;
    public Vector3 rotationAxis;

    private void Update()
    {
        transform.Rotate(new Vector3(rotationAxis.x, rotationAxis.y, rotationAxis.z) * Time.deltaTime * rotateSpeed);
    }
}
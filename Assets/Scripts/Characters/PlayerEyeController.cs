using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEyeController : MonoBehaviour
{
    public Transform leftEye;
    public Transform rightEye;
    public Transform target;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        target = FindObjectOfType<NUT>().transform;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        leftEye.LookAt(target.position);
        rightEye.LookAt(target.position);
    }
}

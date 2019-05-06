using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    public Camera CameraToFace;
    private void Awake()
    {
        if (CameraToFace == null)
        {
            CameraToFace = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
    }
    
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(CameraToFace.transform.forward, CameraToFace.transform.up);
    }
}

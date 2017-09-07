using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorRotation : MonoBehaviour {
    public Camera vrCamera;
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f)) * vrCamera.transform.rotation;
    }
}

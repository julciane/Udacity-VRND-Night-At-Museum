using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorRotation : MonoBehaviour {
    public Camera vrCamera;
    private float _angle = 180;
    private Sides currentSide = Sides.Front;
    private enum Sides
    {
        Front,
        Left,
        Back,
        Right
    };
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Euler(new Vector3(0f, _angle, 0f)) * vrCamera.transform.rotation;
    }

    public void Click()
    {
        //State machine
        switch (currentSide)
        {
            case Sides.Front:
                currentSide = Sides.Left;
                _angle = 90f;
                break;
            case Sides.Left:
                currentSide = Sides.Back;
                _angle = 0f;
                break;
            case Sides.Back:
                currentSide = Sides.Right;
                _angle = 270f;
                break;
            case Sides.Right:
                currentSide = Sides.Front;
                _angle = 180f;
                break;
        }
        
    }
}

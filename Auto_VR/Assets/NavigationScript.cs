using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationScript : MonoBehaviour {

    public GameObject _canvasInfo;
    public Transform _originalPoint;
    public Transform _featurePoint;

    public void DismissCanvas()
    {
        _canvasInfo.SetActive(false);
    }

    public void LoadFeature()
    {
        Camera.main.transform.position = _featurePoint.position;
    }
}

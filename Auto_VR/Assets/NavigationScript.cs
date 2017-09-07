using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationScript : MonoBehaviour {

    public GameObject _canvasInfo;

    public void DismissCanvas()
    {
        _canvasInfo.SetActive(false);
    }
}

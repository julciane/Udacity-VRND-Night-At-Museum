﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class NavigationScript : MonoBehaviour {

    public GameObject _canvasInfo;
    public Transform _originalPoint;
    public Transform _featurePoint;

    private VideoPlayer videoPlayer;

    private void Start()
    {
        videoPlayer = transform.GetComponentInChildren<VideoPlayer>();
    }

    public void DismissCanvas()
    {
        _canvasInfo.SetActive(false);
    }

    public void LoadFeature()
    {
        Camera.main.transform.parent.transform.position = _featurePoint.position;

        //Camera.main.transform.parent.transform.rotation = _featurePoint.rotation;
    }

    private IEnumerator loadAsync()
    {
        Debug.Log("Load Feature!!");
        videoPlayer = transform.GetComponentInChildren<VideoPlayer>();
        if (videoPlayer != null)
        {
            videoPlayer.Stop();
            while (videoPlayer.isPlaying)
            {
                yield return null;
            }
        }

        Camera.main.transform.parent.transform.position = _featurePoint.position;

        //Camera.main.transform.parent.transform.rotation = _featurePoint.rotation;
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitApp()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();         
#endif
    }

    private void OnTriggerEnter(Collider other)
    {
        if(videoPlayer != null)
        {
            videoPlayer.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (videoPlayer != null)
        {
            videoPlayer.Stop();
            while (videoPlayer.isPlaying)
            {
                Debug.Log("Not ready yet");
            }
        }

    }
}

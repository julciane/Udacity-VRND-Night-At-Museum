using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
//using UnityEngine.Video;

public class StreamVideo : MonoBehaviour {

    public string url;
    public GameObject sphere;
    public VideoClip videoToPlay;

    private VideoPlayer videoPlayer;
    private VideoSource videoSource;
    private SphereCollider a;

    private AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        //Application.runInBackground = true;
        //StartCoroutine(play360Video());
    }

    /*override void OnTriggerEnter()
    {

    }*/

    IEnumerator play360Video()
    {
        videoPlayer = gameObject.AddComponent<VideoPlayer>();
        audioSource = gameObject.AddComponent<AudioSource>();

        videoPlayer.playOnAwake = false;
        audioSource.playOnAwake = false;
        audioSource.Pause();

        //videoPlayer.source = VideoSource.Url;
        //videoPlayer.url = url;


        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);

        //videoPlayer.renderer = sphere.GetComponent<MeshRenderer>();

        videoPlayer.source = VideoSource.VideoClip;
        videoPlayer.clip = videoToPlay;
        videoPlayer.Prepare();

        WaitForSeconds waitTime = new WaitForSeconds(1.0f);

        while (!videoPlayer.isPrepared)
        {
            yield return waitTime;
            break;
        }
        //sphere.GetComponent<Renderer>().material.SetTexture("_MainTex", videoPlayer.texture);

        videoPlayer.Play();
        audioSource.Play();


    }
    // Update is called once per frame
    void Update () {
		
	}
}

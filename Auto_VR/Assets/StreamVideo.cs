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
    

    IEnumerator play360Video()
    {
        videoPlayer = gameObject.AddComponent<VideoPlayer>();
        audioSource = gameObject.AddComponent<AudioSource>();

        videoPlayer.playOnAwake = false;
        audioSource.playOnAwake = false;
        audioSource.Pause();
        
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);
        
        videoPlayer.source = VideoSource.VideoClip;
        videoPlayer.clip = videoToPlay;
        videoPlayer.Prepare();

        WaitForSeconds waitTime = new WaitForSeconds(1.0f);

        while (!videoPlayer.isPrepared)
        {
            yield return waitTime;
            break;
        }

        videoPlayer.Play();
        audioSource.Play();
    }
}

using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour
{
	private enum State
	{
		Idle,
		Focused,
		Clicked,
		Approaching,
		Moving,
		Collect,
		Collected,
		Occupied,
		Open,
		Hidden
	}

	[SerializeField]
	private State  		_state					= State.Idle;
	private Color		_color_origional		= new Color(0.0f, 1.0f, 0.0f, 0.5f);
	private Color		_color					= Color.white;
	private float 		_scale					= 1.0f;
	private float 		_animated_lerp			= 1.0f;
	private AudioSource _audio_source			= null;
	private Material	_material				= null;

    public GameObject _canvasInfo;

	[Header("Material")]
	public Material	material					= null;
	public Color color_hilight					= new Color(0.8f, 0.8f, 1.0f, 0.125f);	
	
	[Header("State Blend Speeds")]
	public float lerp_idle 						= 0.0f;
	public float lerp_focus 					= 0.0f;
	public float lerp_hide						= 0.0f;
	public float lerp_clicked					= 0.0f;
	
	[Header("State Animation Scales")]
	public float scale_clicked_max				= 0.0f;
	public float scale_animation				= 3.0f;	
	public float scale_idle_min 				= 0.0f;
	public float scale_idle_max 				= 0.0f;
	public float scale_focus_min				= 0.0f;
	public float scale_focus_max				= 0.0f;

	[Header("Sounds")]
	public AudioClip clip_click					= null;				

	[Header("Hide Distance")]
	public float threshold						= 0.125f;



	void Awake()
	{		
		_material					= Instantiate(material);
		_color_origional			= _material.color;
		_color						= _color_origional;
		_audio_source				= gameObject.GetComponent<AudioSource>();	
		_audio_source.clip		 	= clip_click;
		_audio_source.playOnAwake 	= false;
	}

    public void Click()
	{
		_state = _state == State.Focused ? State.Clicked : _state;

        WaypointManager.Instance.SetCurrent(gameObject.transform.parent.tag);
        WaypointManager.Instance.isMoving = true;

        iTween.MoveTo(Camera.main.transform.parent.gameObject, 
				iTween.Hash (
					"position", gameObject.transform.position, 
					"time", .5F, 
					"easetype", "linear"
				)
			);
    }
    
}

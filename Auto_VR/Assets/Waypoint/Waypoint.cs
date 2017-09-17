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


	/*void Update()
	{
		bool occupied 	= Camera.main.transform.parent.transform.position == gameObject.transform.position;

        /*if (occupied)
        {
            gameObject.transform.GetComponentInChildren<MeshRenderer>().enabled = false;
            gameObject.transform.GetComponentInChildren<MeshCollider>().enabled = false;
        }
        else
        {
            gameObject.transform.GetComponentInChildren<MeshRenderer>().enabled = true;
            gameObject.transform.GetComponentInChildren<MeshCollider>().enabled = true;
            
        }* /
		
		switch(_state)
		{
			case State.Idle:
				Idle();
                _state 		= occupied ? State.Occupied : _state;
				break;

			case State.Focused:
				Focus();
				break;

			case State.Clicked:
				Clicked();

                WaypointManager.Instance.SetCurrent(gameObject.transform.parent.tag);
                //_canvasInfo.SetActive(true);

                bool scaled = _scale >= scale_clicked_max * .95f;
				_state 		= scaled ? State.Approaching : _state;
				break;

			case State.Approaching:
				Hide();	

				_state 		= occupied ? State.Occupied : _state;
				break;
			case State.Occupied:
				Hide();
				_state = !occupied ? State.Idle : _state;
				break;
			
			case State.Hidden:
				Hide();
				break;

			default:
				break;
		}
	}*/


	public void Enter()
	{
		_state = _state == State.Idle ? State.Focused : _state;
	}


	public void Exit()
	{
		_state = State.Idle;
	}


	public void Click()
	{
		_state = _state == State.Focused ? State.Clicked : _state;
		
		_audio_source.Play();

		Camera.main.transform.parent.transform.position 	= gameObject.transform.position;
        WaypointManager.Instance.SetCurrent(gameObject.transform.parent.tag);

        /*gameObject.transform.GetComponentInChildren<MeshRenderer>().enabled = false;
        gameObject.transform.GetComponentInChildren<MeshCollider>().enabled = false;*/
    }


	private void Idle()
	{
		float scale				= Mathf.Lerp(scale_idle_min, scale_idle_max, _animated_lerp);
		Color color				= Color.Lerp(_color_origional, 	  color_hilight, _animated_lerp);

		_scale					= Mathf.Lerp(_scale, scale, lerp_idle);
		_color					= Color.Lerp(_color, color, lerp_idle);
	}


	public void Focus()
	{
		float scale				= Mathf.Lerp(scale_focus_min, scale_focus_max, _animated_lerp);
		Color color				= Color.Lerp(   _color_origional,   color_hilight, _animated_lerp);

		_scale					= Mathf.Lerp(_scale, scale, lerp_focus);
		_color					= Color.Lerp(_color, color,	lerp_focus);
	}


	public void Clicked()
	{	
		_scale					= Mathf.Lerp(_scale, scale_clicked_max, lerp_clicked);
		_color					= Color.Lerp(_color,     color_hilight, lerp_clicked);
	}


	public void Hide()
	{
		_scale					= Mathf.Lerp(_scale, 		0.0f, lerp_hide);
		_color					= Color.Lerp(_color, Color.clear, lerp_hide);
	}
}

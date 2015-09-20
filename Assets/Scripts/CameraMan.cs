using UnityEngine;
using System.Collections;

public class CameraMan : MonoBehaviour {

	public bool isGameOn;
	public float camDistX;
	public float camDistY;
	public float camDistZ;
	public float camSpeed = 0.1f;
	public float camDampTime = 0.15f;
	
	public float camOrthoDistX;
	public float camOrthoDistY;
	public float camOrthoDistZ;
	public float camOrthoDistZoomOut;
	public float camOrthoDistZoomIn;
	public float camOrthoDistYAwayFromFloor;
	public float camOrthoDistYAwayFromFloorBind;

	public Vector3 camOrthoZoom = Vector3.zero;
	private Vector3 velocity = Vector3.zero;
	public GameObject kanonMan;
	public ControlPanel cPanel;

	// Use this for initialization
	void Start () {
		cPanel = GameObject.Find ("Control Panel").GetComponent<ControlPanel> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (kanonMan) 
		{
			if (Camera.main.orthographic)
				UpdateOrthograpicMode ();
			else
				UpdatePerspectiveMode ();
		}	

		// Check if game is on
		isGameOn = cPanel.isGameOn;
	}

	void UpdateOrthograpicMode ()
	{
		// Ortho Cam x posiion (Zoomed in or out)
		float xDist 	= (isGameOn) ? camOrthoDistZoomOut : camOrthoDistZoomIn;
		float curDist	= Camera.main.orthographicSize;
		float dist 		= Mathf.Lerp(curDist, xDist, camDampTime);

		Camera.main.orthographicSize = dist;

		// Ortho Cam z/y Position
		if(isGameOn)
		{
			float yFloorDist = kanonMan.transform.root.position.y;
			float yCamDistFromFloor = (yFloorDist > camOrthoDistYAwayFromFloor) ? kanonMan.transform.root.position.y + camOrthoDistY : camOrthoDistYAwayFromFloorBind;
			
			//print ("yFloorDist: "+yFloorDist+", yCamDisFromFloor:"+yCamDistFromFloor);
			Vector3 destination = new Vector3(camOrthoDistX, yCamDistFromFloor, kanonMan.transform.root.position.z + camOrthoDistZ);
			Vector3 destinationTweened = Vector3.SmoothDamp(Camera.main.transform.position, destination, ref velocity, camDampTime);
			Camera.main.transform.position = destinationTweened;
		}
	}

	void UpdatePerspectiveMode ()
	{
		if (isGameOn) 
		{
			Vector3 destination = new Vector3(camDistX, kanonMan.transform.root.position.y + camDistY, kanonMan.transform.root.position.z + camDistZ);
			Vector3 destinationTweened = Vector3.SmoothDamp(Camera.main.transform.position, destination, ref velocity, camDampTime);
			Camera.main.transform.position = destinationTweened;
		}
	}
}

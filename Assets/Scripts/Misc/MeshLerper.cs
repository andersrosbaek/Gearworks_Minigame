using UnityEngine;
using System.Collections;

public class MeshLerper : MonoBehaviour
{
	public Vector3 scaleTarget = new Vector3 (0.5f, 0.5f, 0.5f);
	public float scaleChangeSpeed = 0.05f;
	private float scaleRemoveLimit = 0.1f;
	private MeshRenderer mRenderer;
	private Rigidbody rBody;
	private Vector3 velocityTarget = new Vector3 (0,0,0);
	private Color colorCurrent;
	public Color colorNext;
	public float colorChangeSpeed = 0.05f;

	void Start()
	{
		rBody 			= GetComponent<Rigidbody> ();
		mRenderer 		= GetComponent<MeshRenderer> ();
		colorCurrent 	= mRenderer.material.color;
		colorNext 		= new Color (1, 1, 1, 0);
	}

	void Update()
	{
		ScaleDown ();
		SpeedDown ();
		ColorOut ();
	}

	void ColorOut ()
	{
		// Lerp Color
		mRenderer.material.color = Color.Lerp(mRenderer.material.color, new Color(0,0,0,0), colorChangeSpeed * Time.timeScale); //dissolveColors[currentColor], changeColorSpeed * Time.timeScale);
		
		// Lerp Emission
		mRenderer.material.EnableKeyword ("_EMISSION");
		mRenderer.material.SetColor("_EmissionColor", Color.Lerp(mRenderer.material.color, new Color(1, 1, 1, 0), colorChangeSpeed * Time.timeScale));

		// Remove if scale is zero or below
		if (mRenderer.material.color.a <= 0.1f) 
		{
			Destroy(gameObject);
		}
	}

	void SpeedDown ()
	{
		rBody.velocity = Vector3.Lerp(rBody.velocity, scaleTarget, scaleChangeSpeed * Time.timeScale);
	}

	void ScaleDown ()
	{
		// Well, scale down..
		transform.localScale = Vector3.Lerp(transform.localScale, scaleTarget, scaleChangeSpeed * Time.timeScale);

//		// Remove if scale is zero or below
//		if (transform.localScale.x <= scaleRemoveLimit 
//		    || transform.localScale.y <= scaleRemoveLimit
//		    || transform.localScale.z <= scaleRemoveLimit) 
//		{
//			Destroy(gameObject);
//		}
	}
}

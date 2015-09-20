using UnityEngine;
using System.Collections;

public class ParticlesRotateWrapper : MonoBehaviour 
{
	public float xRotateSpeed = 1.0f;
	public float yRotateSpeed = 1.0f;
	public float zRotateSpeed = 1.0f;

	// Update is called once per frame
	void Update () 
	{
		transform.Rotate (new Vector3(xRotateSpeed, yRotateSpeed, zRotateSpeed));
	}
}

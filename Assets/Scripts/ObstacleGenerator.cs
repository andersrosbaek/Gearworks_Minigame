using UnityEngine;
using System.Collections;

public class ObstacleGenerator : MonoBehaviour {

	public ArrayList gObjects = new ArrayList ();
	public ControlPanel SCRIPT_ControlPanel;
	public KanonMan SCRIPT_KanonMan;
	public GameObject kanonMan;
	public GameObject kegle;

	public float CD_Spawn;
	public float CD_Spawn_Max;
	public float CD_Spawn_Min;

	public float zDistMax;
	public float zDistMin;
	public float yDistMax;
	public float yDistMin;

	// Update is called once per frame
	void Update () 
	{
		UpdateSpawnTime ();
		UpdateDisposableObjects ();
	}

	void UpdateSpawnTime ()
	{
		// Spawn obstacles when not grounded . .
		// . . and has left the Start Platform
		if (SCRIPT_ControlPanel.isGameOn && SCRIPT_KanonMan.isGrounded == false) 
		{
			if (CD_Spawn < 0) 
			{
				Spawn ();
				CD_Spawn = Random.Range (CD_Spawn_Min, CD_Spawn_Max);
			} 
			else 
			{
				CD_Spawn -= Time.deltaTime;
			}
		}
	}

	void Spawn ()
	{
		// Set z- and y position
		float zPosKanon = kanonMan.transform.root.position.z;
		float yPosKanon = kanonMan.transform.root.position.y;
		float zPos = zPosKanon + Random.Range (zDistMin, zDistMax); 
		float yPos = yPosKanon + Random.Range (yDistMin, yDistMax); 
			  yPos = (yPos < 0) ? 0.2f + Random.Range (0, yDistMax) : yPos; // If the object is beneath earth.
		GameObject gObj = Instantiate (kegle);
		gObj.transform.root.position = new Vector3 (0, yPos, zPos);

		// Keep reference in array properly remove it later..
		gObjects.Add (gObj);
	}

	void UpdateDisposableObjects ()
	{
		foreach (GameObject g in gObjects)
		{
			if (g != null)
			{
				if (g.transform.root.position.z < kanonMan.transform.root.position.z - 5)
				{
					Destroy(g.transform.root.gameObject);
				}
			}
		}
	}
}

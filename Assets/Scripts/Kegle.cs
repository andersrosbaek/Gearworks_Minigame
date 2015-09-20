using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Kegle : MonoBehaviour {

	public GameObject PFX_KegleHit;
	KanonMan SCRIPT_KanonMan;
	int kegleScore = 50;

	// Use this for initialization
	void Start () {
		SCRIPT_KanonMan = GameObject.Find("KanonManHolder").transform.Find("KanonMan").gameObject.GetComponent<KanonMan>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider col)
	{
		// Give points to KanonMan
		SCRIPT_KanonMan.scoreKegler += kegleScore;

		// Play Random Tenpin(Kegle) Hit Sound
		float volume = 0.5f;
		SoundsTriggered sTrig = GetComponent<SoundsTriggered> ();
		int ran = Random.Range (0, 3);
		if (ran == 0) 
			sTrig.playSound1 (true, volume);
		else if (ran == 1)
			sTrig.playSound2 (true, volume);
		else if (ran == 2)
			sTrig.playSound3 (true, volume);

		// Show Hit Particles
		GameObject gObj = Instantiate (PFX_KegleHit);
		gObj.transform.position = transform.Find("Animator").Find("IMG_SunColumns").position;
		gObj.transform.Find ("TXT_Points").gameObject.GetComponent<TextMesh> ().text = "+" + kegleScore;

		// Destroy this
		Destroy (transform.root.gameObject);
	}
}

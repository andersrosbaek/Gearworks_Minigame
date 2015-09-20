using UnityEngine;
using System.Collections;

public class KanonManCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col)
	{
		transform.Find ("KanonMan").GetComponent<KanonMan> ().CollisionEnter (col);
	}
}

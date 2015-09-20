using UnityEngine;
using System.Collections;

public class Utils {
	public static float pushPowerScale = 900;

	public static void Push(Component with, GameObject on,float power) {
//		with.transform.LookAt(on.transform.position, Vector3.back);
//		// TODO set toughness by ship type
//		float toughness = on.GetComponent<Player> ().toughness;
//		on.GetComponent<Rigidbody>().AddForce(with.transform.forward * (1.0f/toughness) * power * pushPowerScale * (1 / (1-(1-Time.timeScale)))); //, ForceMode.Acceleration);
	}
	public static float EucliDist(float x1,float y1,float x2,float y2){
		return Mathf.Sqrt (Mathf.Pow (x2 - x1, 2) + Mathf.Pow (y2 - y1, 2));
	}
	public static float EucliDist2(float dx, float dy){
		return Mathf.Sqrt (Mathf.Pow (dx, 2) + Mathf.Pow (dy, 2));
	}
}

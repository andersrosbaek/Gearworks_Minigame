using UnityEngine;
using System.Collections;

public class BtnRetry : MonoBehaviour {

	public void Retry()
	{
		Application.LoadLevel ("Play");
		print ("Retrying");
	}
}

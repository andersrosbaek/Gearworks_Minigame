using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_Highscore : MonoBehaviour {

	private ScoreBoard sBoard;

	// Use this for initialization
	void Start () {
		sBoard = GameObject.Find ("GUI").transform.Find ("Retry + ScoreBoard").gameObject.GetComponent<ScoreBoard> ();
	}

	public void SaveEditChanges()
	{
		sBoard.SaveEditChanges (transform.gameObject.GetComponent<InputField>().text);
	}
}

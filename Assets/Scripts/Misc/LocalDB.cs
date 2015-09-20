using UnityEngine;
using System.Collections;

public class LocalDB : MonoBehaviour 
{
	// Use this for initialization
	void Start () 
	{
		// Reset Score when Game is opened anew.
		CheckScoreReset();
	}

	public static void CheckScoreReset()
	{
		if (SessionStarted == false)
		{
			Green_Score = 0;
			Red_Score = 0;
			PlayerDead = 0;

			SessionStarted = true;
		}
	}

	private static bool sessionStarted = false;
	private static bool SessionStarted
	{
		get { return sessionStarted; }
		set { sessionStarted = value; }
	}

	public static int Green_Score
	{
		get { return PlayerPrefs.GetInt("Green_Score"); }
		set { PlayerPrefs.SetInt("Green_Score", value); }
	}
	
	public static int Red_Score
	{
		get { return PlayerPrefs.GetInt("Red_Score"); }
		set { PlayerPrefs.SetInt("Red_Score", value); }
	}

	public static int PlayerDead
	{
		get { return PlayerPrefs.GetInt("PlayerDead"); }
		set { PlayerPrefs.SetInt("PlayerDead", value); }
	}
	
	public static string JoystickStyle
	{
		get { return PlayerPrefs.GetString("JoystickStyle"); }
		set { PlayerPrefs.SetString("JoystickStyle", value); }
	}
}

using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

public class ScoreBoard : MonoBehaviour 
{
	private Color colorBest = new Color((float)59/255, (float)223/255, (float)94/255);
	private Color colorWorst = new Color((float)223/255, (float)205/255, (float)59/255);
	float redLerp;
	float greenLerp;
	float blueLerp;

	private int rankOfNewScore;
	private InputField uiNew;
	public GameObject UI_Highscore;
	public GameObject UI_Highscore_Panel;
	private static int maxScores = 10;
	private static string fileName = "Highscores.xml";
	public static string HighscoresDataDirectory
	{
		get { return Application.persistentDataPath + "/"; }
		set { Debug.Log("LevelsDirectory Cannot be changed"); }
	}

	void ShowHighScores ()
	{
		List<Highscore> highscores = HighscoresXML.GetInstance ().highscores;
		highscores = highscores.OrderBy(o=>o.rank).ToList();

		int length = highscores.Count;
		for (int i = 0; i<length; i++)
		{			
			// Create UI for highscore
			GameObject uiScore = Instantiate(UI_Highscore);
			uiScore.transform.SetParent(UI_Highscore_Panel.transform);

			// Set values of highscore
			Highscore highscore = highscores[i] as Highscore;
			uiScore.transform.Find("TXT_Rank").gameObject.GetComponent<Text>().text = "#"+highscore.rank;
			uiScore.transform.Find("INPUT_Name").gameObject.GetComponent<InputField>().text = highscore.name+"";
			uiScore.transform.Find("TXT_Score").gameObject.GetComponent<Text>().text = highscore.score+"";

			// Make new score editable
			if (i == rankOfNewScore-1) {
				uiNew = uiScore.transform.Find("INPUT_Name").gameObject.GetComponent<InputField>();
				uiNew.text = "<Insert Name>";
				uiNew.textComponent.color = colorBest;
				uiNew.interactable = true;
			}

			// Set color of rank
			uiScore.transform.Find("TXT_Score").gameObject.GetComponent<Text>().color = new Color(Mathf.Abs(redLerp), 1 - Mathf.Abs(greenLerp), Mathf.Abs(blueLerp));
			redLerp = redLerp + ((colorBest.r - colorWorst.r) / maxScores * 3);
			greenLerp = greenLerp + ((colorBest.g - colorWorst.g) / maxScores);
			blueLerp = blueLerp + ((colorBest.b - colorWorst.b) / maxScores);
		}
	}

	public void SaveEditChanges(string nameValue)
	{
		(HighscoresXML.GetInstance().highscores[rankOfNewScore-1] as Highscore).name = nameValue;
		SaveScoreBoard();
	}

	void LoadHighscores()
	{
		// Check if highscore-file exists, and create it otherwise.
		try {
			string filePath = HighscoresDataDirectory + fileName;
			HighscoresXML.Load(filePath);
		}
		catch(Exception e)
		{
			// Notify developer about missing file about to be created
			print ("The highscore file doesn't exist and one will be created!");

			// Create level file and save it
			HighscoresXML.GetInstance().highscores.Add(new Highscore(1, "Hank", 1600));
			HighscoresXML.GetInstance().highscores.Add(new Highscore(2, "Arnold", 800));
			HighscoresXML.GetInstance().highscores.Add(new Highscore(3, "Taylor", 400));
			HighscoresXML.GetInstance().highscores.Add(new Highscore(4, "Bob", 200));
			HighscoresXML.GetInstance().highscores.Add(new Highscore(5, "Jennifer", 100));
			SaveScoreBoard();
		}
	}

	void SaveScoreBoard ()
	{
		HighscoresXML.GetInstance().Save(HighscoresDataDirectory, fileName);
	}

	// Update is called once per frame
	public void SubmitScore (int score) 
	{
		// Load scores
		LoadHighscores ();

		// Check if score is higher than the ten highest
		List<Highscore> highscores = HighscoresXML.GetInstance ().highscores;
		highscores = highscores.OrderBy(o=>o.rank).ToList();
		int length = highscores.Count;
		rankOfNewScore = length + 1;
		for (int i = 0; i<length; i++)
		{
			// Check if better than any other highscore
			// For each lower score, raise rank++
			Highscore highscore = highscores[i] as Highscore;
			if (score > highscore.score){
				if (rankOfNewScore > highscore.rank){
					rankOfNewScore = highscore.rank;
				}
				
				// Raise their rank and remove if over maxScores
				highscore.rank++;
			}
		}

		// Add the new highscore to the XML
		highscores.Add (new Highscore(rankOfNewScore, "Unknown", score));

		// Sort the highscores and remove those above MaxScores
		highscores = highscores.OrderBy(o=>o.rank).ToList();
		length = highscores.Count;
		for (int u = length-1; u > maxScores-1; u--)
		{
			highscores.RemoveAt(u);
		}

		// Override previous highscorelist
		HighscoresXML.GetInstance ().highscores = highscores;

		// Save
		SaveScoreBoard ();

		// Show scores
		ShowHighScores ();
	}
}

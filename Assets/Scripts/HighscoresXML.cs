using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System;
using System.ComponentModel;
using System.Reflection;
using System.IO;

[XmlRoot("GearworksMinigameData")]
public class HighscoresXML 
{
	[XmlArray("HighscoresXML")]
	[XmlArrayItem("Highscores")]
	public List<Highscore> highscores = new List<Highscore>();

	/**
	 * Singleton
	 */ 
	[XmlIgnore]
	private static HighscoresXML highscoreXML;
	public static HighscoresXML GetInstance()
	{
		if (highscoreXML == null)
		{
			highscoreXML = new HighscoresXML();
		}

		return highscoreXML;
	}

	/**
	 * Save
	 */
	public void Save(string directory, string fileName)
	{
		// Check for directory and make it if doesn't exist
		Directory.CreateDirectory(directory);

		// Write file
		System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(HighscoresXML));
		System.IO.StreamWriter file = new System.IO.StreamWriter(System.IO.Path.Combine(directory, fileName)); //"Save.xml"));
		ser.Serialize(file, GetInstance());
		file.Close();
	}

	/**
	 * Load
	 */
	public static HighscoresXML Load(string path)
	{
		var serializer = new XmlSerializer(typeof(HighscoresXML));
		using(var stream = new FileStream(path, FileMode.Open))
		{
			highscoreXML = serializer.Deserialize(stream) as HighscoresXML;
			return highscoreXML;
		}
	}
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System;

[Serializable]
public class Highscore
{
	[XmlArrayItem("rank")]
	public int rank;

	[XmlArrayItem("name")]
	public string name;
	
	[XmlArrayItem("score")]
	public int score;

	
	public Highscore()
	{
	}
	
	public Highscore(
		int rank,
		string name,
		int score)
	{
		this.rank = rank;
		this.name = name;
		this.score = score;
	}
}

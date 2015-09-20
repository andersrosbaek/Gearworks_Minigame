using UnityEngine;
using System.Collections;

public class MusicGearworksMinigame : MonoBehaviour 
{
	private static MusicGearworksMinigame instance = null;
	public static MusicGearworksMinigame Instance {
		get { return instance; }
	}
	void Awake() {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}

	public AudioClip audioClip1;
	public AudioClip audioClip2;
	public float volume;
	private bool stopMusic;
	
	float switchSongPieceTime = 15.98f; 
	public GameObject gameobjectAudioSource1;
	public GameObject gameobjectAudioSource2;
	private AudioSource audioSource1;
	private AudioSource audioSource2;

	// Use this for initialization
	void Start () 
	{
		// Give AudioSource to the first
		audioSource1 = gameobjectAudioSource1.AddComponent<AudioSource>();
		audioSource1.clip = audioClip1;
		audioSource1.volume = volume;
		audioSource1.Play();
		audioSource1.time = 13;
		
		// Give AudioSource to the first
		audioSource2 = gameobjectAudioSource2.AddComponent<AudioSource>();
		audioSource2.clip = audioClip2;
		audioSource2.volume = volume;
		audioSource2.playOnAwake = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		UpdatePitchAndFading ();
		UpdateTransitioningBetweenMusicPieces ();
	}

	void UpdateTransitioningBetweenMusicPieces ()
	{
		if (audioSource2.isPlaying == false && audioSource1.time >= switchSongPieceTime)
		{
			audioSource2.Play();
			audioSource1.time = 0;
			audioSource1.Stop();
		}

		if (audioSource1.isPlaying == false && audioSource2.time >= switchSongPieceTime)
		{
			audioSource1.Play();
			audioSource2.time = 0;
			audioSource2.Stop();
		}

		//print ("1 time: "+audioSource1.time+ ", 2 time:"+audioSource2.time);
		//print ("1: "+audioSource1.isPlaying+", 2:"+audioSource2.isPlaying);
	}

	void UpdatePitchAndFading ()
	{
		audioSource1.pitch = 1 - (1 - Time.timeScale) / 2;
		audioSource2.pitch = 1 - (1 - Time.timeScale) / 2;
		
//		if(audioSource1.isPlaying == false)
//		{
//			audioSource1.Play();
//		}
		
		if(stopMusic == true)
		{
			if(audioSource1.volume > 0)
			{
				audioSource1.volume -= 0.02f;
				audioSource2.volume -= 0.02f;
			}
		}
		else
		{
			if(audioSource1.volume < volume)
			{
				audioSource1.volume += 0.02f;
				audioSource2.volume += 0.02f;
			}
		}
	}

	public void stopTheMusic()
	{
		stopMusic = true;
	}
	
	public void startTheMusic()
	{
		stopMusic = false;
	}
}

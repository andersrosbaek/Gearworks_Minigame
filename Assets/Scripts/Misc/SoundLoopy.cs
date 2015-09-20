using UnityEngine;
using System.Collections;

public class SoundLoopy : MonoBehaviour 
{
	private AudioSource audioSource = null;
	public AudioClip audioClip;
	public float volumeMax = 1f;
	public float volumeRate = 0.05f;
	public bool isPlaying;

	// Use this for initialization
	void Start () 
	{
		audioSource = transform.root.gameObject.AddComponent<AudioSource>();
		audioSource.clip = audioClip;
		audioSource.volume = volumeMax;
		audioSource.loop = true;
	}

	public void StartPlaying()
	{
		isPlaying = true;
		audioSource.Play();
	}

	public void StopPlaying()
	{
		isPlaying = false;
	}

	// Update is called once per frame
	void Update () 
	{
		// Turn volume up/down
		if (isPlaying) {
			// Slo-mo pitching
			audioSource.pitch = Time.timeScale;

			// Crank it up!
			if (audioSource.volume < volumeMax) {
				audioSource.volume += volumeRate;
			}
		} 
		else if (audioSource.volume > 0)
		{
			audioSource.volume -= volumeRate;
		}

		// Reset sound if volume is zero
		if (audioSource.volume <= 0) {
			audioSource.time = 0;
		}
	}
}

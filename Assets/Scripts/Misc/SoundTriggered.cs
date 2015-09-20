using UnityEngine;
using System.Collections;

public class SoundTriggered : MonoBehaviour 
{
	private AudioSource audioSource = null;
	public AudioClip audioClip1;
	public float volume = 1.0f;
	public bool detached = false;

	public void playSound()
	{ 
		playSoundz(audioClip1); 
	}

	private void playSoundz(AudioClip audioClip)
	{	
		if (detached)
		{
			transform.parent = null;
		}

		if(audioSource == null) audioSource = transform.gameObject.AddComponent<AudioSource>();
		audioSource.clip = audioClip;
		audioSource.volume = volume;
		audioSource.Play ();
	}

	void Update () 
	{
		if (audioSource != null)
		{
			audioSource.pitch = Time.timeScale;
		}

		if (detached == true)
		{

		}
	}
}

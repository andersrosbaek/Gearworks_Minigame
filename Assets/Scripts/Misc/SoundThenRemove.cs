using UnityEngine;
using System.Collections;

public class SoundThenRemove : MonoBehaviour 
{
	private AudioSource audioSource = null;
	public AudioClip audioClip;
	public float volume = 1f;

	// Use this for initialization
	void Start () 
	{
		// Only make audioSource if clip is provided
		// else find existing audioSource in "Update" 
		// and remove after it played
		if (audioClip != null) {
			audioSource = transform.root.gameObject.AddComponent<AudioSource> ();
			audioSource.clip = audioClip;
			audioSource.volume = volume;
			audioSource.Play ();
		} else {
			audioSource = gameObject.GetComponent<AudioSource> ();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		audioSource.pitch = Time.timeScale;

		if(audioSource.isPlaying == false)
		{
			Destroy(transform.root.gameObject);
		}
	}
}

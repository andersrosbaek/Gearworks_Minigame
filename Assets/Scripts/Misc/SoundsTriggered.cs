using UnityEngine;
using System.Collections;

public class SoundsTriggered : MonoBehaviour 
{
	private AudioSource audioSource = null;
	public AudioClip audioClip1;
	public AudioClip audioClip2;
	public AudioClip audioClip3;
	public AudioClip audioClip4;
	public AudioClip audioClip5;
	public AudioClip audioClip6;
	public AudioClip audioClip7;
	public AudioClip audioClip8;
	public AudioClip audioClip9;
	public AudioClip audioClip10;
	public AudioClip audioClip11;
	public AudioClip audioClip12;

	public void playSound1(bool detach = false, float volume = 1f) { playSound(audioClip1, detach, volume); }
	public void playSound2(bool detach = false, float volume = 1f) { playSound(audioClip2, detach, volume); }
	public void playSound3(bool detach = false, float volume = 1f) { playSound(audioClip3, detach, volume); }
	public void playSound4(bool detach = false, float volume = 1f) { playSound(audioClip4, detach, volume); }
	public void playSound5(bool detach = false, float volume = 1f) { playSound(audioClip5, detach, volume); }
	public void playSound6(bool detach = false, float volume = 1f) { playSound(audioClip6, detach, volume); }
	public void playSound7(bool detach = false, float volume = 1f) { playSound(audioClip7, detach, volume); }
	public void playSound8(bool detach = false, float volume = 1f) { playSound(audioClip8, detach, volume); }
	public void playSound9(bool detach = false, float volume = 1f) { playSound(audioClip9, detach, volume); }
	public void playSound10(bool detach = false, float volume = 1f) { playSound(audioClip10, detach, volume); }
	public void playSound11(bool detach = false, float volume = 1f) { playSound(audioClip11, detach, volume); }
	public void playSound12(bool detach = false, float volume = 1f) { playSound(audioClip12, detach, volume); }

	public static void asdasd()
	{
	}

	bool detached = false;

	private void playSound(AudioClip audioClip, bool detach, float volume = 1f)
	{	
		if (detach) 
		{
			GameObject go = new GameObject ("Detached Triggered Sound");
			AudioSource goAudio = go.AddComponent<AudioSource> ();
			goAudio.clip = audioClip;
			goAudio.volume = volume;
			go.AddComponent<SoundThenRemove>();
			goAudio.Play ();
		} 
		else 
		{
			if(audioSource == null) audioSource = transform.gameObject.AddComponent<AudioSource>();
			audioSource.clip = audioClip;
			audioSource.volume = volume;
			audioSource.Play ();
		}
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

using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

[System.Serializable]
public class Sound
{
	public SoundEnums soundName;

	public AudioClip soundClip;

	public AudioMixerGroup audioMixerGroup;

	[Range(0, 1)]
	public float volume;

	[Range(-3, 3)]
	public float pitch;

	[Range(0, 1)]
	public float spatialBlend;

	public bool loop, playOnAwake;


	public AudioSource audioSource { get { return _audioSource; } set { _audioSource = value; } }
	private AudioSource _audioSource;

	public void Play()
	{
		audioSource?.Play();
	}

	public void Stop()
	{
		audioSource?.Stop();
	}

	public void Pause()
	{
		audioSource?.Pause();
	}

	public bool HasAudioSource()
	{
		return audioSource != null;
	}
}


using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

[System.Serializable]

//Clase para definir sonidos y crear sus audioSources al mismo tiempo.
public class Sound
{
	public string soundName;

	public AudioClip soundClip;

	public AudioMixerGroup audioMixerGroup;

	[Range(0, 1)]
	public float volume = 1;

	[Range(-3, 3)]
	public float pitch = 1;

	[Range(0, 1)]
	public float spatialBlend;

	public float minDistance = 1, maxDistance = 10;

	public bool loop, playOnAwake;

	public AudioSource audioSource { get { return _audioSource; } set { _audioSource = value; } }
	private AudioSource _audioSource;

	/// <summary>
	/// Función para saber si el sonido tiene un audiosource asignado
	/// </summary>
	/// <returns>true si lo tiene, false si no</returns>
	public bool HasAudioSource()
	{
		return audioSource != null;
	}

	public void SetAudioSource()
	{
		audioSource.outputAudioMixerGroup = audioMixerGroup;
		audioSource.clip = soundClip;
		audioSource.volume = volume;
		audioSource.pitch = pitch;
		audioSource.spatialBlend = spatialBlend;
		audioSource.loop = loop;
		audioSource.playOnAwake = playOnAwake;
		audioSource.minDistance = minDistance;
		audioSource.maxDistance = maxDistance;
	}
}


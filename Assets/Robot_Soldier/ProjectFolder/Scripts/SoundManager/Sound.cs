using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

[System.Serializable]

//Clase para definir sonidos y crear sus audioSources al mismo tiempo.
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

	public bool loop, playOnAwake, FadeInOnAwake;

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
}


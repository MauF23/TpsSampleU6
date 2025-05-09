using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random; //Importar DOTween

public class SoundManager : MonoBehaviour
{
	public List<Sound> soundList;
	private const float DEFAULT_TWEEN_TIME = 3;

	private void OnValidate()
	{
		if (Application.isPlaying)
		{
			EditSounds();
		}
	}

	private void Awake()
	{
		SetupSounds();

		for (int i = 0; i < soundList.Count; i++)
		{
			if (soundList[i].playOnAwake)
			{
				PlaySound(soundList[i].soundName);
			}
		}
	}

	public void PlaySound(string soundName)
	{
		Sound sound = FindSound(soundName);
		sound?.audioSource?.Play();
	}

	public void FadeIn(string soundName)
	{
		Sound sound = FindSound(soundName);

		if (sound == null)
		{
			return;
		}

		sound.audioSource.volume = 0;
		sound.audioSource.DOFade(sound.volume, DEFAULT_TWEEN_TIME);
		PlaySound(soundName);
	}

	public void FadeOut(string soundName)
	{
		Sound sound = FindSound(soundName);

		if (sound == null)
		{
			return;
		}

		sound.audioSource.DOFade(0, DEFAULT_TWEEN_TIME).SetUpdate(true);
	}

	public void PlayRandomPitch(string soundName, float minPitch, float maxPitch)
	{
		Sound sound = FindSound(soundName);

		if (sound == null)
		{
			return;
		}

		sound.audioSource.pitch = Random.Range(minPitch, maxPitch);
		PlaySound(soundName);
	}

	public void StopSound(string soundName)
	{
		Sound sound = FindSound(soundName);
		sound?.audioSource?.Pause();
	}

	public void PauseSound(string soundName)
	{
		Sound sound = FindSound(soundName);
		sound?.audioSource?.Pause();
	}

	/// <summary>
	/// Encuentra un sonido si el nombre indicado existe
	/// </summary>
	/// <param name="soundName">el nombre del sonido a buscar</param>
	/// <returns>el sonido si es que existe bajo el nombre indicado</returns>
	private Sound FindSound(string soundName)
	{
		for (int i = 0; i < soundList.Count; i++)
		{
			if (soundList[i].soundName == soundName)
			{
				return soundList[i];
			}
		}

		return null;
	}

	/// <summary>
	/// Funci�n para instancear los audioSources dependiendo de la lista de sonidos especificada.
	/// </summary>
	private void SetupSounds()
	{
		for (int i = 0; i < soundList.Count; i++)
		{
			AudioSource source;

			source = gameObject.AddComponent<AudioSource>();
			soundList[i].audioSource = source;

			soundList[i].SetAudioSource();
		}
	}

	/// <summary>
	/// Funci�n para editar en runtime los valores de los sonidos.
	/// </summary>
	private void EditSounds()
	{
		for (int i = 0; i < soundList.Count; i++)
		{
			if (!soundList[i].HasAudioSource())
			{
				return;
			}

			soundList[i].SetAudioSource();
		}
	}
}

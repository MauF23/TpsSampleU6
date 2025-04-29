using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random; //Importar DOTween

public class SoundManager : MonoBehaviour
{
	public List<Sound> soundList;
	private const float DEFAULT_TWEEN_TIME = 3;
	private const float VOLUME_MULTIPLIER_MIN = 0.5f;
	private const float VOLUME_MULTIPLIER_MAX = 1;


	private void OnValidate()
	{
		if (Application.isPlaying)
		{
			EditSounds();
		}
	}

	private void Start()
	{
		SetupSounds();

		for (int i = 0; i < soundList.Count; i++)
		{
			if (soundList[i].playOnAwake)
			{
				PlaySound(soundList[i].soundName);
			}
		}

		FadeSoundIn(SoundEnums.BgMusic);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			FadeSoundOut(SoundEnums.BgMusic);
		}
	}

	public void PlaySound(SoundEnums soundName)
	{
		Sound sound = FindSound(soundName);
		sound?.audioSource?.Play();
	}

	/// <summary>
	/// Reproduce un sonido variando el pitch en un rango establecido, haciendo que el sonido varie ligeramente
	/// </summary>
	/// <param name="soundName">el enumerador del sonido a reproducir</param>
	/// <param name="minPitch">el rango mínmo del pitch mínimo</param>
	/// <param name="maxPitch">el rando máximo del pitch</param>
	public void PlayAudioRandomPitch(SoundEnums soundName, float minPitch, float maxPitch)
	{
		Sound sound = FindSound(soundName);

		if(sound == null)
		{
			return;
		}

		sound.audioSource.pitch = Random.Range(minPitch, maxPitch);	
		sound.audioSource.Play();
	}

	public void PlaySoundWithRandomVolume(SoundEnums soundName)
	{
		Sound sound = FindSound(soundName);

		if (sound == null)
		{
			return;
		}

		float randomMultiplier = Random.Range(VOLUME_MULTIPLIER_MIN, VOLUME_MULTIPLIER_MAX);

		sound.audioSource.volume = sound.volume * randomMultiplier;
		sound.audioSource.Play();
	}


	public void FadeSoundIn(SoundEnums soundName)
	{
		Sound sound = FindSound(soundName);

		if(sound == null)
		{
			return;
		}

		sound.audioSource.volume = 0;
		sound.audioSource.DOFade(sound.volume, DEFAULT_TWEEN_TIME);
	}

	public void FadeSoundOut(SoundEnums soundName)
	{
		Sound sound = FindSound(soundName);

		if (sound == null)
		{
			return;
		}

		sound.audioSource.DOFade(0, DEFAULT_TWEEN_TIME);
	}

	public void StopSound(SoundEnums soundName)
	{
		Sound sound = FindSound(soundName);
		sound?.audioSource?.Pause();
	}

	public void PauseSound(SoundEnums soundName)
	{
		Sound sound = FindSound(soundName);
		sound?.audioSource?.Pause();
	}

	/// <summary>
	/// Encuentra un sonido si el nombre indicado existe
	/// </summary>
	/// <param name="soundName">el nombre del sonido a buscar</param>
	/// <returns>el sonido si es que existe bajo el nombre indicado</returns>
	private Sound FindSound(SoundEnums soundName)
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
	/// Función para instancear los audioSources dependiendo de la lista de sonidos especificada.
	/// </summary>
	private void SetupSounds()
	{
		for (int i = 0; i < soundList.Count; i++)
		{
			AudioSource source;

			source = gameObject.AddComponent<AudioSource>();
			soundList[i].audioSource = source;

			SetAudioSources(soundList[i].audioSource, soundList[i]);
		}
	}

	/// <summary>
	/// Función para editar en runtime los valores de los sonidos.
	/// </summary>
	private void EditSounds()
	{
		for (int i = 0; i < soundList.Count; i++)
		{
			if (!soundList[i].HasAudioSource())
			{
				return;
			}

			SetAudioSources(soundList[i].audioSource, soundList[i]);
		}
	}

	/// <summary>
	/// Funcion para setear/actualizar los valores de los audiosources creados.
	/// </summary>
	/// <param name="audioSource">el audiosource a modificar</param>
	/// <param name="souund">el sonido que manipalara los valores del audiosource</param>
	private void SetAudioSources(AudioSource audioSource, Sound souund)
	{
		audioSource.outputAudioMixerGroup = souund.audioMixerGroup;
		audioSource.clip = souund.soundClip;
		audioSource.volume = souund.volume;
		audioSource.pitch = souund.pitch;
		audioSource.loop = souund.loop;
		audioSource.playOnAwake = souund.playOnAwake;
	}
}

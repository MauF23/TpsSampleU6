using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public List<Sound> soundList;
	private List<AudioSource> audioSourceList = new List<AudioSource>();
	private const float DEFAULT_TWEEN_TIME = 1;

	private void Awake()
	{
		audioSourceList.Clear();
		SetupSounds();
	}

	public void PlaySound(SoundEnums soundName)
	{
		Sound sound = FindSound(soundName);
		sound.Play();
	}

	public void StopSound(SoundEnums soundName)
	{
		Sound sound = FindSound(soundName);
		sound.Pause();
	}

	public void PauseSound(SoundEnums soundName)
	{
		Sound sound = FindSound(soundName);
		sound.Pause();
	}

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
	private void SetupSounds()
	{
		for (int i = 0; i < soundList.Count; i++)
		{
			AudioSource source;

			if (!soundList[i].HasAudioSource())
			{
				source = gameObject.AddComponent<AudioSource>();
				soundList[i].audioSource = source;
			}
			else
			{
				source = soundList[i].audioSource;
			}

			source.outputAudioMixerGroup = soundList[i].audioMixerGroup;
			source.clip = soundList[i].soundClip;
			source.volume = soundList[i].volume;
			source.pitch = soundList[i].pitch;
			source.loop = soundList[i].loop;
			source.playOnAwake = soundList[i].playOnAwake;

			if (soundList[i].playOnAwake)
			{
				soundList[i].Play();
			}

			if (!audioSourceList.Contains(source))
			{
				audioSourceList.Add(source);	
			}
		}
	}
}

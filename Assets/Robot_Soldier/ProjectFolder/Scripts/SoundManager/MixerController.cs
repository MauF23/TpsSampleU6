using UnityEngine;
using UnityEngine.Audio;

public class MixerController : MonoBehaviour
{
	public AudioMixer musicMixer;
	private const string MUSIC_VOLUME = "MusicVolume";
	private const string SFX_VOLUME = "SFXVolume";

	public void setMusicVolume(float music)
	{
		musicMixer.SetFloat(MUSIC_VOLUME, music);
	}

	public void setSFXVolume(float sfxVolmune)
	{
		musicMixer.SetFloat(SFX_VOLUME, sfxVolmune);
	}
}

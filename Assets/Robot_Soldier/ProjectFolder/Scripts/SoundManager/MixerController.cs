using UnityEngine;
using UnityEngine.Audio;

public class MixerController : MonoBehaviour
{
    public AudioMixer musicMixer;
    private const string MUSIC_VOLUME = "VolumeMusic";

	public void setMusicVolume(float music)
    {
        musicMixer.SetFloat(MUSIC_VOLUME, music);

	}
}

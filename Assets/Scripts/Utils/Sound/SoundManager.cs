using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager> {
    public static string MUSIC_OPTION = "MUSIC_OPTION";
    public static string SOUND_FX_OPTION = "SOUND_FX_OPTION";
    private List<AudioClip> allClips;

    public List<AudioClip> allMusicClips;
    public List<AudioClip> allSoundFXClips;

    public AudioSource musicAudioSource;

    public AudioMixer masterAudioMixer;

    private int loadedAudioClips = 0;

    private void Awake () {
        this.allMusicClips = new List<AudioClip> (this.allMusicClips);
        this.allMusicClips.AddRange (this.allSoundFXClips);
    }

    protected override void OnLoadSync () { }

    protected override bool IsAsync () => true;

    override public void LoadOnUpdateIntervall () {
        if (this.loadedAudioClips != this.allMusicClips.Count) {
            this.allMusicClips[this.loadedAudioClips].LoadAudioData ();
            this.loadedAudioClips++;
        } else {
            isReady = true;
        }
    }

    public void ChangeSoundFx (float value) {
        this.masterAudioMixer.SetFloat ("soundFxVolume", value);
        PlayerPrefs.SetFloat (SoundManager.SOUND_FX_OPTION, value);
    }

    public void ChangeMusic (float value) {
        this.masterAudioMixer.SetFloat ("musicVolume", value);
        PlayerPrefs.SetFloat (SoundManager.MUSIC_OPTION, value);
    }
    public void PlayMusic (int musicIndex) {
        this.musicAudioSource.clip = allMusicClips[musicIndex];
        this.musicAudioSource.Play ();
    }

    public void PauseMusic () => this.musicAudioSource.Pause ();

}
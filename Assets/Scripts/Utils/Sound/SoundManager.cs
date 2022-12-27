using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Utils.Singleton;

namespace Utils.Sound
{
    public class SoundManager : Singleton<SoundManager>
    {
        private const string MusicOption = "MUSIC_OPTION";
        private const string SoundFXOption = "SOUND_FX_OPTION";

        public List<AudioClip> allMusicClips;
        public List<AudioClip> allSoundFXClips;

        public AudioSource musicAudioSource;

        public AudioMixer masterAudioMixer;
        private List<AudioClip> allClips;

        private int loadedAudioClips;

        private void Awake()
        {
            allMusicClips = new List<AudioClip>(allMusicClips);
            allMusicClips.AddRange(allSoundFXClips);
        }

        protected override void OnLoadSync()
        {
        }

        protected override bool IsAsync()
        {
            return true;
        }

        public override void LoadOnUpdateInterval()
        {
            if (loadedAudioClips != allMusicClips.Count)
            {
                allMusicClips[loadedAudioClips].LoadAudioData();
                loadedAudioClips++;
            }
            else
            {
                isReady = true;
            }
        }

        public void ChangeSoundFx(float value)
        {
            masterAudioMixer.SetFloat("soundFxVolume", value);
            PlayerPrefs.SetFloat(SoundFXOption, value);
        }

        public void ChangeMusic(float value)
        {
            masterAudioMixer.SetFloat("musicVolume", value);
            PlayerPrefs.SetFloat(MusicOption, value);
        }

        public void PlayMusic(int musicIndex)
        {
            musicAudioSource.clip = allMusicClips[musicIndex];
            musicAudioSource.Play();
        }

        public void PauseMusic()
        {
            musicAudioSource.Pause();
        }
    }
}
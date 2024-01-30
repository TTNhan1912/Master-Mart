using System;
using UnityEngine;

public class SoundMN : Singleton<SoundMN>
{

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;


    public void StopMusic(string name)
    {
        // Tìm đối tượng Sound tương ứng với tên nhạc
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Không có sound với tên " + name);
        }
        else
        {
            // Nếu tìm thấy, kiểm tra xem có đang phát không
            if (musicSource.clip == s.clip && musicSource.isPlaying)
            {
                // Dừng phát nhạc nền
                musicSource.Stop();
            }
        }
    }



    // nhạc nền
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("không có sound");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    // Tiếng game
    public void PlaySfx(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("không có sound");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    // bấm nút tắt nhạc nền
    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    // bấm nút tắt âm thanh
    public void ToggleSfx()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SfxVolume(float volume)
    {
        sfxSource.volume = volume;
    }

}

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[System.Serializable]
public class Clip
{
    public string Name;
    public AudioClip clip;
}
public class SoundManager : Singleton<SoundManager>
{
    public AudioSource audioSource;
    public List<Clip> clips;
    public Button BGMBar;
    public Button SFXBar;

    public bool BGMOn = true;
    public bool SFXOn = true;

    public List<Sprite> sprites;
    public Image BGMImg;
    public Image SFXImg;

    float SEvolume = 1;
    protected SoundManager() { }

    public void Playbgm(string name)
    //���� SoundManager.Instance.Playbgm("string");
    {
        Clip find = clips.Find((o) => { return o.Name == name; });
        if (find != null)
        {
            audioSource.Stop();
            audioSource.clip = find.clip;
            audioSource.loop = true;
            audioSource.Play();
        }


    }

    public void PlaySound(string _clip)
    {
        Clip find = clips.Find((o) => { return o.Name == _clip; });
        if (find != null)
        {
            GameObject audio_object = new GameObject();
            AudioSource object_source = audio_object.AddComponent<AudioSource>();
            object_source.volume = SEvolume;
            object_source.clip = find.clip;
            object_source.loop = false;
            object_source.Play();

            Destroy(audio_object, find.clip.length);
        }
    }
    public void PlaySound(AudioClip _clip)
    {
        GameObject audio_object = new GameObject();
        AudioSource object_source = audio_object.AddComponent<AudioSource>();
        object_source.volume = SEvolume;
        object_source.clip = _clip;
        object_source.loop = false;
        object_source.Play();

        Destroy(audio_object, _clip.length);
    }

    public void BGMOnOff()
    {
        BGMOn = !BGMOn;
        BGMImg.sprite = sprites[BGMOn ? 0 : 1];
        audioSource.volume = BGMOn ? 100 : 0;
        PlayerPrefs.SetInt("BGMOn", BGMOn ? 0 : 1);
    }
    public void SFXOnOff()
    {
        SFXOn = !SFXOn;
        SFXImg.sprite = sprites[SFXOn ? 0 : 1];
        SEvolume = SFXOn ? 100 : 0;
        PlayerPrefs.SetInt("SFXOn", SFXOn ? 0 : 1);
    }
    private void Start()
    {
        BGMOn = PlayerPrefs.GetInt("BGMOn") == 0 ? true : false;
        SFXOn = PlayerPrefs.GetInt("SFXOn") == 0 ? true : false;
        BGMImg.sprite = sprites[BGMOn ? 0 : 1];
        SFXImg.sprite = sprites[SFXOn ? 0 : 1];
        audioSource.volume = BGMOn ? 100 : 0;
        SEvolume = SFXOn ? 100 : 0;
    }
}

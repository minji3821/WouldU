using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    // BGM Á¾·ùµé
    public enum EBgm {  BGM_GAME }
    public enum TileESfx { SFX_Boom }

    [Serializable]
    public struct BgmClip
    {
        public EBgm bgmType;
        public AudioClip clip;
        [Range(0f, 1f)] public float volume;
    }

    [Serializable]
    public struct SoundClip<T>
    {
        public T soundType;
        public AudioClip clip;
        [Range(0f, 1f)] public float volume;
    }

    [SerializeField] private List<BgmClip> bgmClips = new List<BgmClip>();
    [SerializeField] private List<SoundClip<TileESfx>> tileSounds = new List<SoundClip<TileESfx>>();

    private Dictionary<EBgm, BgmClip> bgmDict = new Dictionary<EBgm, BgmClip>();
    private Dictionary<TileESfx, SoundClip<TileESfx>> tileSoundDict = new Dictionary<TileESfx, SoundClip<TileESfx>>();

    [SerializeField] private AudioSource audioBgm;
    [SerializeField] private AudioSource audioSfx;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeDictionaries();
            PlayBGM(EBgm.BGM_GAME);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeDictionaries()
    {
        foreach (var bgm in bgmClips)
        {
            if (!bgmDict.ContainsKey(bgm.bgmType))
            {
                bgmDict.Add(bgm.bgmType, bgm);
            }
        }

        AddToDictionary(tileSounds, tileSoundDict);
    }

    private void AddToDictionary<T>(List<SoundClip<T>> list, Dictionary<T, SoundClip<T>> dictionary)
    {
        foreach (var sound in list)
        {
            if (!dictionary.ContainsKey(sound.soundType))
            {
                dictionary.Add(sound.soundType, sound);
            }
        }
    }

    public void PlayBGM(EBgm bgmType)
    {
        if (bgmDict.TryGetValue(bgmType, out BgmClip bgm))
        {
            audioBgm.clip = bgm.clip;
            audioBgm.volume = bgm.volume;
            audioBgm.Play();
        }
        else
        {
            Debug.LogWarning($"BGM {bgmType} not found!");
        }
    }

    public void PlayTileESfx(TileESfx esfx)
    {
        PlaySound(tileSoundDict, esfx);
    }


    private void PlaySound<T>(Dictionary<T, SoundClip<T>> dictionary, T key)
    {
        if (dictionary.TryGetValue(key, out SoundClip<T> sound))
        {
            audioSfx.PlayOneShot(sound.clip, sound.volume);
        }
        else
        {
            Debug.LogWarning($"Sound {key} not found!");
        }
    }
}

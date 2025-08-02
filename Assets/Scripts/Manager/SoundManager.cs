using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; } = null;

    [Range(0.0f, 1.0f)]
    [SerializeField] float m_bgmVolume = 0.6f;

    [Range(0.0f, 1.0f)]
    [SerializeField] float m_seVolume = 0.8f;

    [SerializeField] AudioMixer m_audioMixer = null; // 오디오 믹서
    [SerializeField] AudioSource m_bgmAudio = null; // BGM 오디오 소스

    [Header("SE Pooling")]
    [SerializeField] int m_sePoolSize = 10;

    Tween m_bgmFadeTween = null;
    Queue<AudioSource> m_seAudioPool = new Queue<AudioSource>();

    void Awake()
    {
        if (SoundManager.Instance == null)
        {
            Instance = this;
            Setting();
            InitSEPool();
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    void Setting()
    {
        m_audioMixer.SetFloat("BGMVolume", Mathf.Log10(m_bgmVolume) * 20.0f);
        m_audioMixer.SetFloat("SEVolume", Mathf.Log10(m_seVolume) * 20.0f);
    }

    void InitSEPool()
    {
        for (int i = 0; i < m_sePoolSize; i++)
        {
            m_seAudioPool.Enqueue(CreateAudioSource());
        }
    }

    AudioSource CreateAudioSource()
    {
        GameObject _obj = new GameObject("SE");
        _obj.transform.parent = transform;

        AudioSource _source = _obj.AddComponent<AudioSource>();
        _source.outputAudioMixerGroup = m_audioMixer.FindMatchingGroups("SE")[0];
        _source.loop = false;
        _source.playOnAwake = false;

        return _source;
    }

    public void PlayBGM(AudioClip argClip, float argDuration)
    {
        if (m_bgmAudio.clip == argClip) return;

        if (m_bgmFadeTween != null) m_bgmFadeTween.Kill();

        float _originalVolume = m_bgmAudio.volume;

        m_bgmFadeTween = m_bgmAudio.DOFade(0.0f, argDuration).SetUpdate(true).OnComplete(() =>
        {
            m_bgmAudio.Stop();
            m_bgmAudio.clip = argClip;
            m_bgmAudio.Play();

            m_bgmFadeTween = m_bgmAudio.DOFade(_originalVolume, argDuration).SetUpdate(true);
        });
    }

    public void PlaySE(AudioClip argClip)
    {
        AudioSource _source = m_seAudioPool.Count > 0 ? m_seAudioPool.Dequeue() : CreateAudioSource();
        _source.gameObject.SetActive(true);
        _source.PlayOneShot(argClip);

        StartCoroutine(ReleaseAudioSourceAfterFinish(_source, argClip));
    }

    IEnumerator ReleaseAudioSourceAfterFinish(AudioSource argSource, AudioClip argClip)
    {
        yield return new WaitForSeconds(argClip.length);

        argSource.gameObject.SetActive(false);
        m_seAudioPool.Enqueue(argSource);
    }
}

using UnityEngine;

public class StageSound : MonoBehaviour
{
    [SerializeField] AudioClip m_stageBGMClip = null;
    [SerializeField] float m_fadeDuration = 0.0f;

    void Start()
    {
        if (m_stageBGMClip != null) SoundManager.Instance.PlayBGM(m_stageBGMClip, m_fadeDuration);
    }
}
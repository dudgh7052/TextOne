using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneMoveManager : MonoBehaviour
{
    public static SceneMoveManager Instance { get; private set; } = null;

    [SerializeField] Image m_fadeImg = null;

    [Range(0.5f, 1.5f)]
    [SerializeField] float m_fadeSpeed = 0.0f;

    [SerializeField] Color[] m_color = null;

    Color m_curColor = Color.black;

    #region Properties
    /// <summary>
    /// 씬 이동 기다리는 플래그
    /// </summary>
    public bool IsWaitFlag { get; set; } = false;

    /// <summary>
    /// 씬 이동 플래그
    /// </summary>
    public bool IsSceneMoveFlag { get; set; } = false;
    #endregion

    void Awake()
    {
        if (SceneMoveManager.Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public async void ChangeScene(string argSceneName, bool argWaitFlag = false)
    {
        if (IsSceneMoveFlag) return;

        IsSceneMoveFlag = true;
        IsWaitFlag = argWaitFlag;

        await FadeInOut(argSceneName);
    }

    async Task FadeInOut(string argSceneName)
    {
        m_curColor = m_color[1];
        m_fadeImg.color = m_curColor;

        while(m_fadeImg.color.a < 1.0f)
        {
            m_curColor.a += m_fadeSpeed * Time.unscaledDeltaTime;
            m_curColor.a = Mathf.Clamp01(m_curColor.a);
            m_fadeImg.color = m_curColor;
            await Task.Yield();
        }

        await SceneManager.LoadSceneAsync(argSceneName);
        await Task.Yield();

        while (IsWaitFlag) await Task.Yield();

        m_curColor = m_color[0];
        m_fadeImg.color = m_curColor;

        while (m_fadeImg.color.a > 0.0f)
        {
            m_curColor.a -= m_fadeSpeed * Time.unscaledDeltaTime;
            m_curColor.a = Mathf.Clamp01(m_curColor.a);
            m_fadeImg.color = m_curColor;
            await Task.Yield();
        }

        IsSceneMoveFlag = false;
    }
}
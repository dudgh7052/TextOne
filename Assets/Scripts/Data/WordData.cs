using UnityEngine;

[CreateAssetMenu(fileName = "NewWordData", menuName = "Scriptable Objects/WordData", order = 1)]
public class WordData : ScriptableObject
{
    [Header("단어 문자열")]
    [SerializeField] string m_word = string.Empty;

    [Header("스테이지 인덱스 - 스테이지에 맞게 설정")]
    [SerializeField] int m_stageIndex = -1;

    /// <summary>
    /// 단어 문자열
    /// </summary>
    public string IsWord { get { return m_word; } }

    /// <summary>
    /// 스테이지 인덱스
    /// </summary>
    public int IsStageIndex { get { return m_stageIndex; } }
}
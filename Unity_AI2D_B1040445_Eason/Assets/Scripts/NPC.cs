using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPC : MonoBehaviour
{
    public enum state
    {
        start,notComplete,Complete
    }

    public state _state;

    [Header("對話")]
    public string sayStart = "歡迎來到任務地點";
    public string NotComplete = "任務尚未完成";
    public string Complete = "任務已完成";
    [Header("對話速度")]
    public float speed = 1.5f;
    [Header("任務相關")]
    public bool complete;
    public int countPlayer;
    public int countFinish = 10;
    [Header("介面")]
    public GameObject objCanvas;
    public Text textsay;

    private AudioSource aud;

    private void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "玩家")
            Say();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "玩家")
            SayClose();
    }

    /// <summary>
    /// 對話
    /// </summary>
    private void Say()
    {
        objCanvas.SetActive(true);
        StopAllCoroutines();

        if (countPlayer >= countFinish) _state = state.Complete;

        switch (_state)
        {
            case state.start:
                StartCoroutine(ShowDialog(sayStart));
                _state = state.notComplete;
                break;
            case state.notComplete:
                StartCoroutine(ShowDialog(NotComplete));
                break;
            case state.Complete:
                StartCoroutine(ShowDialog(Complete));
                break;
        }
    }

    /// <summary>
    /// 關閉對話
    /// </summary>
    private void SayClose()
    {
        StopAllCoroutines();
        objCanvas.SetActive(false);
    }

    private IEnumerator ShowDialog(string say)
    {
        textsay.text = "";

        for (int i = 0; i < say.Length; i++)
        {
            textsay.text += say[i].ToString();
            yield return new WaitForSeconds(speed);
        }
    }

    public void PlayerGet()
    {
        countPlayer++;
    }
}

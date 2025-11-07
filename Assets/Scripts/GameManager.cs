using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isGameOver = false;
    private bool isGameOverStarted = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver == true) {GameOver();}
    }

    //게임오버 되면 실행하는 함수
    public void GameOver()
    {
        if (isGameOverStarted) { return; }
        isGameOverStarted = true;
        StartCoroutine(GameOverCoroutine());
    }

    IEnumerator GameOverCoroutine()
    {
        yield return new WaitForSeconds(5f);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isGameOver = false;
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
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

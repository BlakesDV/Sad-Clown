using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    public float timer = 6;
    public TextMeshProUGUI numbersDisplayed;
    public GameObject gameOverPanel;

    void Update()
    {
        timer -= Time.deltaTime;

        numbersDisplayed.text = Mathf.Ceil(timer).ToString();

        if (timer <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}

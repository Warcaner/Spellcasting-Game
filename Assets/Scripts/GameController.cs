using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public float currScore = 0;

    [SerializeField] Text scoreAmount;
    public Text healthAmount; 
    public Text manaAmount;



    void Start()
    {
        currScore = 0;
        UpdateScoreUI();

    }

    public void AddScore(float amount)
    {
        currScore += amount;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        scoreAmount.text = currScore.ToString("0");


    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
    }

}

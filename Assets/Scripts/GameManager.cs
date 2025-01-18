using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameManager(){
        if (Instance != null)
        {
            Debug.LogWarning("Replacing existing instance");
        }
        Instance=this;
    }


    public void GameOver(){
        // 表示需要加一条新的
        PlayerPrefs.SetInt("New", 1);
        float time = GameObject.Find("Timmer").GetComponent<Timmer>().GetTime();
        PlayerPrefs.SetFloat("Time", time);
        SceneManager.LoadScene("Grade");
    }
}

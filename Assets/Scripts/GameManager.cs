using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G)){
            GameOver();
        }
    }

    public void GameOver(){
        // 表示需要加一条新的
        PlayerPrefs.SetInt("New", 1);
        float time = GameObject.Find("Timmer").GetComponent<Timmer>().GetTime();
        PlayerPrefs.SetFloat("Time", time);
        SceneManager.LoadScene("Grade");
    }
}

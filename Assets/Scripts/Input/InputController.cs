using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    public InputField input;

    public void ClickButton(){
        // 默认名字为刘德华
        string name = input.text != "" ? input.text : "刘德华";
        PlayerPrefs.SetString("Name", name);
        SceneManager.LoadScene("main");
    }
}

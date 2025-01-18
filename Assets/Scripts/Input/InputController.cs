using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    public InputField input;

    public void ClickButton(){
        if(input.text != null){
            PlayerPrefs.SetString("Name", input.text);
            SceneManager.LoadScene("main");
        }
    }
}

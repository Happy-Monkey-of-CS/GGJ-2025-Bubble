using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//引用命名空间
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;

public class StoreManager : MonoBehaviour
{

    //创建Data对象，并向其中添加游戏需要保存的数据
    private Grades GetGameGrades()
    {
        Grades grades = new Grades();
        grades.list = GradeController._instance.list;

        return grades; 
    }
    //向游戏中加载Grades中保存的数据的方法
    private void SetGameGrades( Grades grades)
    {
        GradeController._instance.list = grades.list;
    }


    //用二进制方法对数据进行保存和读档
    private void SaveByBin()
    {
        try
        {
            Grades grades = GetGameGrades();
            BinaryFormatter bf = new BinaryFormatter();//创建二进制格式化程序
            using (FileStream fs = File.Create(Application.dataPath + "/byBin.txt"))  //创建文件流
            {
                bf.Serialize(fs, grades);
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    private void LoadByBin()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = File.Open(Application.dataPath + "/byBin.txt", FileMode.Open))
            {
                Grades grades = (Grades)bf.Deserialize(fs);
                SetGameGrades(grades);
            }
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    //点击保存和加载游戏的方法
    public void ClickSaveGame()
    {
        SaveByBin();//通过二进制的方式存储
    }
    public void ClickLoadGame()
    {
        LoadByBin();//通过二进制的方式读取
    }
}
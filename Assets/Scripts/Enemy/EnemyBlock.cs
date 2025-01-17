using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlock : MonoBehaviour
{
public GameObject[] enemiesPrefab;

List<Enemy> enemies=new();

public int count;

    public void Init(Vector2Int pos){
        for(int i=0;i<count;i++){
            GameObject go=GetRandomElement(enemiesPrefab);
            go=Instantiate(go,transform);
            go.transform.localPosition=new Vector3(Random.Range(0,10f),Random.Range(0,10f));
            enemies.Add(go.GetComponent<Enemy>());
        }
    }
    T GetRandomElement<T>(T[] list)
    {
        int randomIndex = Random.Range(0, list.Length);
        return list[randomIndex];
    }
}

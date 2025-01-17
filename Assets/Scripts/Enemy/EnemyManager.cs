using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    Dictionary<Vector2Int, EnemyBlock> enemyBlocks=new();

    public Transform target;

    void Check()
    {
        int x = Mathf.FloorToInt(target.position.x / 10);
        int y = Mathf.FloorToInt(target.position.y / 10);

        for (int xx = -1; xx < 2; xx++)
        {
            for (int yy = -1; yy < 2; yy++)
            {
                if (!enemyBlocks.ContainsKey(new Vector2Int(x + xx, y + yy)))
                {
                    NewEnemy(new Vector2Int(x + xx, y + yy));
                }
            }
        }
    }

    public GameObject[] enemyBlockPrefabs;

    void NewEnemy(Vector2Int pos)
    {
       GameObject go= GetRandomElement(enemyBlockPrefabs);
       go=Instantiate(go,transform);
       Vector3 v=new Vector3(pos.x*10,pos.y*10);
       go.transform.position=v;
       EnemyBlock enemyBlock= go.GetComponent<EnemyBlock>();
       enemyBlock.Init(pos);
       enemyBlocks.Add(pos,enemyBlock);
    }
    T GetRandomElement<T>(T[] list)
    {
        int randomIndex = Random.Range(0, list.Length);
        return list[randomIndex];
    }

    void Update()
    {
        Check();
    }
}

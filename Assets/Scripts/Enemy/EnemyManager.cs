using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    Dictionary<Vector2Int, EnemyBlock> enemyBlocks = new();

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
        GameObject go = GetRandomElement(enemyBlockPrefabs);
        go = Instantiate(go, transform);
        Vector3 v = new Vector3(pos.x * 10, pos.y * 10);
        go.transform.position = v;
        EnemyBlock enemyBlock = go.GetComponent<EnemyBlock>();
        enemyBlock.Init(pos);
        enemyBlocks.Add(pos, enemyBlock);
    }
    T GetRandomElement<T>(T[] list)
    {
        int randomIndex = UnityEngine.Random.Range(0, list.Length);
        return list[randomIndex];
    }

    float specialTimmer = 0;
    public float specialRate;
    LayerMask mask;

    void OnBeat(){
    }

     void Update()
    {
        Check();

        specialTimmer += Time.deltaTime;
        if (specialTimmer >= specialRate)
        {
            specialTimmer = 0;
            Special();
        }
    }

    void Start()
    {
        mask = LayerMask.GetMask("Player");
        MusicManager.Instance.OnChange+=ChangeSpecial;
        MusicManager.Instance.OnBeat+=OnBeat;
        specialTimmer=999;
    }

    [Serializable]
    public class SpecialPair
    {
        public string name;
        public GameObject gameObject;
    }

    public SpecialPair[] specialPair;

    List<Enemy> specialEnemies=new();

    void Special()
    {
        foreach (var pair in specialPair)
        {
            if (MusicManager.Instance.nowPlaying == pair.name)
            {
                GameObject go = Instantiate(pair.gameObject, transform);
                Enemy e = go.GetComponent<Enemy>();
                specialEnemies.Add(e);

                Vector3 p = Vector3.zero;
                Vector3 blockP = new Vector3(target.position.x, target.position.y );
                bool hasPlayer = true;
                while (hasPlayer)
                {
                    p = new Vector3(UnityEngine.Random.Range(-7f, 7f), UnityEngine.Random.Range(-4f, 4f));
                    hasPlayer = CheckPlayer(blockP + p, e.spawnR, mask);
                }
                go.transform.position = blockP + p;
            }
        }
    }

    void ChangeSpecial(string name)
    {
        foreach(var go in specialEnemies)
        {
            go.DestroySlowly();
        }
        specialEnemies.Clear();
        specialTimmer=999;
    }

    public static bool CheckPlayer(Vector3 pos, float r, LayerMask mask)
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(pos, r, mask);
        bool hasPlayer = false;
        foreach (var c in collider)
        {
            if (c != null && c.CompareTag("Player"))
            {
                hasPlayer = true;
                break;
            }
        }
        return hasPlayer;
    }
}

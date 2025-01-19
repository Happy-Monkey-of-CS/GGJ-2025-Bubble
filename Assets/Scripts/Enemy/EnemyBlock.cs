using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlock : MonoBehaviour
{
    public GameObject[] enemiesPrefab;

    List<Enemy> enemies = new();

    public int count;
    List<Vector2> enemiesPos=new();

    public void Init(Vector2Int pos)
    {
        LayerMask mask = LayerMask.GetMask("Player");

        for (int i = 0; i < count; i++)
        {
            GameObject go = GetRandomElement(enemiesPrefab);
            go = Instantiate(go, transform);
            Enemy e = go.GetComponent<Enemy>();
            enemies.Add(e);

            Vector3 p = Vector3.zero;
            Vector3 blockP = new Vector3(pos.x * 10, pos.y * 10);
            bool hasPlayer = true;
            int tryCount = 0;
            while (hasPlayer && tryCount < 100)
            {
                p = new Vector3(Random.Range(0, 10f), Random.Range(0, 10f));
                hasPlayer = CheckPlayer(blockP + p, e.spawnR, mask);
                if (CheckEnemy(blockP + p, e.spawnR / 2, mask,p))
                {
                    hasPlayer = true;
                }
                tryCount += 1;
            }
            if (tryCount >= 100)
            {
                Destroy(go);
            }
            else
            {
                go.transform.localPosition = p;
                enemiesPos.Add(p);
            }
        }
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

    T GetRandomElement<T>(T[] list)
    {
        int randomIndex = Random.Range(0, list.Length);
        return list[randomIndex];
    }
    public bool CheckEnemy(Vector3 pos, float r, LayerMask mask,Vector2 localpos)
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(pos, r, mask);
        bool hasPlayer = false;
        foreach (var c in collider)
        {
            if (c != null && c.CompareTag("Killer"))
            {
                hasPlayer = true;
                break;
            }
        }
        foreach(var p in enemiesPos){
            if((p-localpos).magnitude<r*2){
                return true;
            }
        }
        return hasPlayer;
    }
}

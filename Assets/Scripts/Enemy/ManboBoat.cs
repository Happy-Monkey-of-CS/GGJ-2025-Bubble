using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManboBoat : Boat
{

    public int moreBoat=1;
    public GameObject killbox;

    protected override void Start()
    {
        base.Start();
        killbox.SetActive(true);
        bool enemy=true;

        float random=0;

        int tryCount=0;

        while(enemy&&tryCount<100){
            random=Random.Range(0,2*Mathf.PI);
            enemy=CheckEnemy(Camera.main.transform.position+new Vector3(Mathf.Sin(random)*10,Mathf.Cos(random)*10),1.5f,LayerMask.GetMask("Player"));
            tryCount+=1;
        }
        if(tryCount>=100){
            Destroy(this);
            return;
        }
        transform.position=Camera.main.transform.position+new Vector3(Mathf.Sin(random)*10,Mathf.Cos(random)*10);

        RotateUpTowards(transform,Camera.main.transform);

        if(moreBoat>0){
            Instantiate(gameObject).GetComponent<ManboBoat>().moreBoat=moreBoat-1;
        }
    }


    public static bool CheckEnemy(Vector3 pos, float r, LayerMask mask)
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
        return hasPlayer;
    }

    // 函数：让物体的 up 向量对准目标，只在 Z 轴旋转
    public static void RotateUpTowards(Transform self, Transform target)
    {
        if (self == null || target == null) return;

        // 获取目标方向 (X-Y 平面)
        Vector2 direction = target.position - self.position;

        // 计算角度
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // 设置物体的旋转
        self.rotation = Quaternion.Euler(0, 0, angle);
    }

    protected override IEnumerator Force(Vector2 f, float t)
    {
                yield break;
    }
    public override void DestroySlowly()
    {
        StartCoroutine(Des());
    }
    public SpriteRenderer[] sprites;
    IEnumerator Des()
    {
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime;
            if (sprites == null)
            {
                break;
            }
            foreach (var s in sprites)
            {
                if (s != null)
                {
                    s.color = new(s.color.r-Time.deltaTime*2, s.color.g-Time.deltaTime*2, s.color.b-Time.deltaTime*2, s.color.a-Time.deltaTime*2);
                }
            }
            yield return null;
        }
        Destroy(gameObject);
    }
}

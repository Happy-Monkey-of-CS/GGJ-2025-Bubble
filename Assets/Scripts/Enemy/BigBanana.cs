using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBanana : DropEnemy
{
    protected override void Update()
    {
        base.Update();
        Vector3 target=Camera.main.transform.position;
        Vector3 self=transform.position;
        target.z=0;
        self.z=0;
        if((target-self).magnitude>10){
            Destroy(gameObject);
        }
    }
}

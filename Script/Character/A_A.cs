using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_A : Unit
{
    public float range_0;
    public float speed_0;



    void Start()
    {
        
    }


    void Update()
    {
        ViewRotate();
    }


    public override void Skillcontent_0(RaycastHit hit)
    {
        NoneTargetSkill(ZeroY((hit.point - transform.position).normalized), skills[0], range_0, speed_0, 0, true, 1);
    }
    public override void Skillcontent_1(RaycastHit hit)
    {
       

    }
    public override void Skillcontent_2(RaycastHit hit)
    {
        

    }
    public override void Skillcontent_3(RaycastHit hit)
    {
       

    }

    public override void ViewOff()
    {
        Colliders[0].transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
    }

    public override bool ViewSkill_0()
    {
        ViewOff();
        if (stat.GetCool(0) <= 0)
        {
            if (Colliders[0] != null)
                Colliders[0].transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
            return true;
        }
        return false;
    }
}

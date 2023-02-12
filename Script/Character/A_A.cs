using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_A : Unit
{
    float q_time;
    bool q_hit;

    void Start()
    {
        stat.SetUnit(this);
    }


    void Update()
    {
        ViewRotate();
        stat.Flow();

        if(q_hit)
        {
            q_time -= Time.deltaTime;
        }
        if(q_time <= 0)
        {
            q_hit = false;
        }
    }

    public override void UseSkill_0(RaycastHit hit)
    {
        ViewOff();
        if(q_hit)
        {
            Debug.Log("qhit상태로 다시 씀");
            if (!isCC)
            {
                if(q_time <= 1.5f)
                {
                    SkillContent_5(true);           //날라가기
                }
                else
                {
                    SkillContent_5(false);          //당기기
                }
                
                q_hit = false;
            }
        }
        else
        {
            if (!isCC && (stat.GetCool(0) <= 0))
            {
                stat.UseSkill(0);
                Skillcontent_0(hit);
            }
        }
        
        SkillUseCallBack_0();
    }
    public override void Skillcontent_0(RaycastHit hit)
    {
        NoneTargetSkill(ZeroY((hit.point - transform.position).normalized), skills[0], ranges[0], speeds[0], 0, true, 1);
    }
    protected void SkillContent_5(bool b)
    {
        if(b)
        {

        }
        else
        {
            Debug.Log("당기기");
            skills[0].transform.parent.GetComponent<Unit>().CompulsionMove((transform.position - skills[0].transform.parent.transform.position).normalized * 1, 3, 0.2f, true);
        }
    }
    public override void SkillHitCallBack_0(Unit unit)
    {
        base.SkillHitCallBack_0(unit);
        skills[0].transform.SetParent(unit.transform, false);
        skills[0].transform.localEulerAngles = Vector3.zero;
        skills[0].transform.localPosition = Vector3.zero;
        skills[0].transform.GetChild(0).gameObject.SetActive(false);
        skills[0].transform.GetChild(1).gameObject.SetActive(true);
        q_hit = true;
        q_time = 3;
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
        if(q_hit)
        {
            UseSkill_0(new RaycastHit());
        }
        else
        {
            if (stat.GetCool(0) <= 0)
            {
                if (Colliders[0] != null)
                    Colliders[0].transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
                return true;
            }
        }
        
        return false;
    }
}

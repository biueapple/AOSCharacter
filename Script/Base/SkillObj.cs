using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillObj : MonoBehaviour
{
    bool isMove;

    Unit parent;        //스킬 쓴사람
    Unit target;
    Vector3 direction;
    float speed;
    int teamNum;
    bool isEnemy;       //true면 teamNum이 다른 false면 같은
    int index;
    int count = 1;          //몇명 맞출건지

    void Start()
    {
        
    }


    void Update()
    {
        if(isMove)
        {
            if(target != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, direction, speed * Time.deltaTime);
            }
        }
    }

    public void TargetSkill(Unit p, Unit unit, float s,int i)
    {
        target = unit;
        speed = s;
        index = i;
        parent = p;

        gameObject.SetActive(true);
        isMove = true;
    }
    public void NoneTargetSkill(Unit p, Vector3 vector,float range, float s,int teamNum, bool enemy,int i, int c)
    {
        direction = transform.position + vector * range;
        speed = s;
        this.teamNum = teamNum;
        isEnemy = enemy;
        index = i;
        parent = p;
        count = c;

        gameObject.SetActive(true);
        isMove = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Unit>() != null)
        {
            if(target != null)
            {
                if(other.name.Equals(target.name))
                {
                    CallHitBack(other.GetComponent<Unit>());
                    count--;
                }
            }
            else
            {
                if(isEnemy)
                {
                    if(other.GetComponent<Unit>().teamNum != teamNum)
                    {
                        CallHitBack(other.GetComponent<Unit>());
                        count--;
                    }
                }
                else
                {
                    if(other.GetComponent<Unit>().teamNum == teamNum)
                    {
                        CallHitBack(other.GetComponent<Unit>());
                        count--;
                    }
                }
            }
        }

        if(count <= 0)
        {
            gameObject.SetActive(false);
            transform.SetParent(parent.transform, false);
            transform.localEulerAngles = Vector3.zero;
            transform.localPosition = Vector3.zero;
        }
    }

    private void CallHitBack(Unit unit)
    {
        switch(index)
        {
            case -1:
                parent.AttackHitCallBack(unit);
                break;
            case 0:
                parent.SkillHitCallBack_0(unit);
                break;
            case 1:
                parent.SkillHitCallBack_1(unit);
                break;
            case 2:
                parent.SkillHitCallBack_2(unit);
                break;
            case 3:
                parent.SkillHitCallBack_3(unit);
                break;
        }
    }
}

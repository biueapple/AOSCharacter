using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
    public Stat stat;
    public NavMeshAgent agent;

    protected float attackDelay;
    public ColliderBack nomalAttackColl;
    public ColliderBack[] Colliders;

    public SkillObj[] skills;

    protected Coroutine EnemyFindCoroutine;        //���� ã��  (�⺻����ã��)
    protected Coroutine Coroutine_CC;              //cc�⿡ �ɷ� �ƹ��͵� ����
    protected bool isCC;
    protected Coroutine FindTargetCoroutine;             //Ÿ���� �Ѹ� ã�� (��ųã��)

    public int teamNum;

    protected Vector3 movePoint;
    protected Unit enemy;

    protected Vector3 mousePosition;
    
    public void MovePoint(Vector3 vector, Unit unit = null)
    {
        AllStop();

        enemy = unit;
        movePoint = vector;

        attackDelay = stat.GetAttackDelay();


        Moving();
    }
    public void Moving()
    {
        if(!isCC)
        {
            agent.destination = movePoint;
            transform.LookAt(movePoint);
        }
    }
    public virtual void Attack(Vector3 vector)      //�����ϰ� ���� ����
    {
        MovePoint(vector, null);
        FindEnemy();
    }
    public virtual void Attack(Unit unit)           //�����ϰ� ������ ����
    {
        MovePoint(unit.transform.position, null);
        if (unit.teamNum != teamNum)
        {
            enemy = unit;
        }
    }
    protected virtual void Attacking()                     
    {
        
    }

    

    public void TargetSkill(Unit unit, SkillObj skillObj, float speed, int index)       //���, ���� ������Ʈ�� ������, ���ǵ�, ���° ��ų����
    {
        skillObj.TargetSkill(this, unit, speed, index);
    }
    public void NoneTargetSkill(Vector3 vector, SkillObj skillObj, float range, float speed, int index, bool enemy,int count)   //����, ���� ������. �Ÿ�, �ӵ�, ���° ��ų����, true�� �ڽŰ� �ٸ���, ������ ������, 
    {
        skillObj.NoneTargetSkill(this, vector, range, speed, teamNum, enemy, index, count);
    }
    public void RangeSkill(ColliderBack coll, float t, bool enemy, int index)       //����, �����Ŀ� �ߵ�����, true�� �ڽŰ� �ٸ���, ���° ��ų����
    {
        StartCoroutine(WatingRangeSkill(coll, t, enemy, index));
    }
    protected IEnumerator WatingRangeSkill(ColliderBack coll, float f, bool enemy, int index)
    {
        yield return new WaitForSeconds(f);

        if(enemy)
        {
            for (int i = 0; i < coll.GetEnemyUnits(teamNum).Count; i++)
            {
                HitCallBack(index, coll.GetEnemyUnits(teamNum)[i]);
            }
        }
        else
        {
            for (int i = 0; i < coll.GetUnits(teamNum).Count; i++)
            {
                HitCallBack(index, coll.GetUnits(teamNum)[i]);
            }
        }
    }

    public void UseSkill_0(RaycastHit hit)
    {
        ViewOff();
        if (!isCC && (stat.GetCool(0) <= 0))
        {
            stat.UseSkill(0);
            Skillcontent_0(hit);
        }
        SkillUseCallBack_0();
    }
    public virtual void Skillcontent_0(RaycastHit hit)
    {

    }
    public void UseSkill_1(RaycastHit hit)
    {
        ViewOff();
        if (!isCC && (stat.GetCool(1) <= 0))
        {
            stat.UseSkill(1);
            Skillcontent_1(hit);
        }
        SkillUseCallBack_1();
    }
    public virtual void Skillcontent_1(RaycastHit hit)
    {

    }
    public void UseSkill_2(RaycastHit hit)
    {
        ViewOff();
        if (!isCC && (stat.GetCool(2) <= 0))
        {
            stat.UseSkill(2);
            Skillcontent_2(hit);
        }
        SkillUseCallBack_2();
    }
    public virtual void Skillcontent_2(RaycastHit hit)
    {

    }
    public void UseSkill_3(RaycastHit hit)
    {
        ViewOff();
        if (!isCC && (stat.GetCool(3) <= 0))
        {
            stat.UseSkill(3);
            Skillcontent_3(hit);
        }
        SkillUseCallBack_3();
    }
    public virtual void Skillcontent_3(RaycastHit hit)
    {

    }
    public virtual void ViewNomalAttack()
    {
        ViewOff();
        nomalAttackColl.GetComponent<MeshRenderer>().enabled = true;
    }

    public void ViewRotate()
    {
        for(int i = 0; i < Colliders.Length; i++)
        {
            if (Colliders[i] != null)
            {
                Colliders[i].transform.LookAt(ZeroY(mousePosition));
            }
        }
    }
    public void SetMousePositon(Vector3 vector)
    {
        mousePosition = vector;
    }

    public virtual bool ViewSkill_0()
    {
        ViewOff();
        if (stat.GetCool(0) <= 0)
        {
            if(Colliders[0] != null)
                Colliders[0].GetComponent<MeshRenderer>().enabled = true;
            return true;
        }
        return false;
    }
    public virtual bool ViewSkill_1()
    {
        ViewOff();
        if (stat.GetCool(1) <= 0)
        {
            if (Colliders[1] != null)
                Colliders[1].GetComponent<MeshRenderer>().enabled = true;
            return true;
        }
        return false;       
    }
    public virtual bool ViewSkill_2()
    {
        ViewOff();
        if (stat.GetCool(2) <= 0)
        {
            if (Colliders[2] != null)
                Colliders[2].GetComponent<MeshRenderer>().enabled = true;
            return true;
        }
        return false;
    }
    public virtual bool ViewSkill_3()
    {
        ViewOff();
        if (stat.GetCool(3) <= 0)
        {
            if (Colliders[3] != null)
                Colliders[3].GetComponent<MeshRenderer>().enabled = true;
            return true;
        }
        return false;
    }
    public virtual void ViewOff()
    {
        if(nomalAttackColl != null)
            nomalAttackColl.transform.GetComponent<MeshRenderer>().enabled = false;
        for(int i = 0; i < Colliders.Length; i++)
        {
            if(Colliders[i] != null)
            {
                Colliders[i].GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
    //
    public void FindEnemy()
    {
        EnemyFindCoroutine = StartCoroutine(EnemyFind());
    }
    public void StopEnemyFind()
    {
        if(EnemyFindCoroutine != null)
        {
            StopCoroutine(EnemyFindCoroutine);
            EnemyFindCoroutine = null;
        }
    }
    protected IEnumerator EnemyFind()
    {
        while(true)
        {
            if(nomalAttackColl.GetEnemyUnits(teamNum).Count > 0)
            {
                List<Unit> list = new List<Unit>();
                list = nomalAttackColl.GetEnemyUnits(teamNum);
                enemy = list[0];
                for(int i = 1; i < list.Count; i++)
                {
                    if(Vector3.Distance(transform.position, list[i].transform.position) < Vector3.Distance(transform.position, enemy.transform.position))
                    {
                        enemy = list[i];
                    }
                }
                break;
            }

            yield return null;
        }
    }
    
    //
    public void FindTarget(Unit unit)
    {
        FindTargetCoroutine = StartCoroutine(TargetFind(unit));
    }
    public void StopTargetFind()
    {
        if(FindTargetCoroutine != null)
        {
            StopCoroutine (FindTargetCoroutine);
            FindTargetCoroutine = null;
        }
    }
    protected IEnumerator TargetFind(Unit unit)
    {
        while (true)
        {
            yield return null;
        }
    }
    
    //
    public void StartCC(float f)
    {
        Coroutine_CC = StartCoroutine(enumerator_CC(f));
        isCC = true;
    }
    public void EndCC()
    {
        if(Coroutine_CC != null)
        {
            StopCoroutine(Coroutine_CC);
            Coroutine_CC = null;
        }
        isCC = false;
    }
    protected IEnumerator enumerator_CC(float f)
    {
        float t = 0;
        while (true)
        {
            t += Time.deltaTime;

            if(t >= f)
            {
                isCC = false;
                break;
            }
            yield return null;
        }
    }
    
    public virtual void AllStop()
    {
        if(EnemyFindCoroutine != null)
        {
            StopCoroutine(EnemyFindCoroutine);
        }
        if(FindTargetCoroutine != null)
        {
            StopCoroutine(FindTargetCoroutine);
        }
        ViewOff();
    }

    public virtual void AttackUseCallBack()
    {
        Debug.Log("�⺻������");
    }
    public virtual void AttackHitCallBack(Unit unit)
    {
        Debug.Log("�⺻���� ����");
    }
    public virtual void AttackOnCallBack()
    {
        Debug.Log("�⺻���� ������");
    }

    public virtual void SkillUseCallBack_0()
    {
        Debug.Log("Q��ų �����");
    }
    public virtual void SkillUseCallBack_1()
    {
        Debug.Log("W��ų �����");
    }
    public virtual void SkillUseCallBack_2()
    {
        Debug.Log("E��ų �����");
    }
    public virtual void SkillUseCallBack_3()
    {
        Debug.Log("R��ų �����");
    }

    public void HitCallBack(int index, Unit unit)
    {
        switch(index)
        {
            case 0:
                SkillHitCallBack_0(unit);
                break;
            case 1:
                SkillHitCallBack_1(unit);
                break;
            case 2:
                SkillHitCallBack_2(unit);
                break;
            case 3:
                SkillHitCallBack_3(unit);
                break;
        }
    }

    public virtual void SkillHitCallBack_0(Unit unit)
    {
        Debug.Log("Q��ų ����");
    }
    public virtual void SkillHitCallBack_1(Unit unit)
    {
        Debug.Log("W��ų ����");
    }
    public virtual void SkillHitCallBack_2(Unit unit)
    {
        Debug.Log("E��ų ����");
    }
    public virtual void SkillHitCallBack_3(Unit unit)
    {
        Debug.Log("R��ų ����");
    }


    public virtual void SkillOnCallBack_0()
    {
        Debug.Log("Q��ų ��밡��");
    }
    public virtual void SkillOnCallBack_1()
    {
        Debug.Log("W��ų ��밡��");
    }
    public virtual void SkillOnCallBack_2()
    {
        Debug.Log("E��ų ��밡��");
    }
    public virtual void SkillOnCallBack_3()
    {
        Debug.Log("R��ų ��밡��");
    }

    public Vector3 ZeroY(Vector3 vector3)
    {
        return new Vector3(vector3.x, 0, vector3.z);
    }
}

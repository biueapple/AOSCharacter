using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "unitData", menuName = "ScriptableObj/CreateUnitStat", order = int.MaxValue)]
public class Stat : ScriptableObject
{
    [SerializeField]
    private float originHp;
    public float GetOriginHp()
    {
        return originHp;
    }

    [SerializeField]
    private float hp;
    public float GetHp()
    {
        return hp;
    }
    public void SetHp(float f)
    {
        hp = f;
    }

    [SerializeField]
    private float originMp;
    public float GetOriginMp()
    {
        return originMp;
    }

    [SerializeField]
    private float mp;
    public float GetMp()
    {
        return mp;
    }
    public void SetMp(float f)
    {
        mp = f;
    }

    [SerializeField]
    private float originAd;
    public float GetOriginAd()
    {
        return originAd;
    }

    [SerializeField]
    private float ad;
    public float GetAd()
    {
        return ad;
    }
    public void SetAd(float f)
    {
        ad = f;
    }

    [SerializeField]
    private float originAp;
    public float GetOriginAp()
    {
        return originAp;
    }

    [SerializeField]
    private float ap;
    public float GetAp()
    {
        return ap;
    }
    public void SetAp(float f)
    {
        ap = f;
    }

    [SerializeField]
    private float range;
    public float GetRange()
    {
        return range;
    }

    [SerializeField]
    private float originAdDefence;
    public float GetOriginAdDefence()
    {
        return originAdDefence;
    }

    [SerializeField]
    private float adDefence;
    public float GetAdDefence()
    {
        return adDefence;
    }
    public void SetAdDefence(float f)
    {
        adDefence = f;
    }

    [SerializeField]
    private float originApDefence;
    public float OriginApDefence()
    {
        return originApDefence;
    }

    [SerializeField]
    private float apDefence;
    public float GetApDefence()
    {
        return apDefence;
    }
    public void SetApDefence(float f)
    {
        apDefence = f;
    }

    [SerializeField]
    private Sprite[] skillSprites;
    public Sprite[] GetSprites()
    {
        return skillSprites;
    }

    [SerializeField]
    private float[] originCool;
    public float GetOriginCool(int index)
    {
        return originCool[index];
    }

    [SerializeField]
    private float[] cool;
    public float GetCool(int index)
    {
        return cool[index];
    }
    public void SetCool(float f, int index)
    {
        cool[index] = f;
    }
    [SerializeField]
    private float shield;
    public float GetShield()
    {
        return shield;
    }
    public void SetShield(float f)
    {
        shield = f;
    }

    [SerializeField]
    private float attackDelay;
    public float GetAttackDelay()
    {
        return attackDelay;
    }

    [SerializeField]
    private Unit unit;
    public void SetUnit(Unit u)
    {
        unit = u;
    }

    public void Flow()
    {
        for(int i = 0; i < cool.Length; i++)
        {
            if (cool[i] > 0)
            {
                if (cool[i] > 0 && cool[i] - Time.deltaTime <= 0)
                {
                    if(i == 0)
                        unit.SkillOnCallBack_0();
                    else if(i == 1)
                        unit.SkillOnCallBack_1();
                    else if (i == 2)
                        unit.SkillOnCallBack_2();
                    else if (i == 3)
                        unit.SkillOnCallBack_3();
                }
                cool[i] -= Time.deltaTime;
            }
        }
    }
    public void UseSkill(int index)
    {
        if(index >= 0 && index < cool.Length)
        {
            cool[index] = originCool[index];
        }
    }

    public void GetAdDamage(float f)
    {
        Debug.Log($"받은 대미지{f * (100 / (100 + adDefence))}, 받기 전 체력 {hp}, 받은 후 체력 {hp - f * (100 / (100 + adDefence))}");
        hp -= f * (100 / (100 + adDefence));
    }
    public void GetApDamage(float f)
    {
        Debug.Log($"받은 대미지{f * (100 / (100 + apDefence))}, 받기 전 체력 {hp}, 받은 후 체력 {hp - f * (100 / (100 + apDefence))}");
        hp -= f * (100 / (100 + apDefence));
    }
}



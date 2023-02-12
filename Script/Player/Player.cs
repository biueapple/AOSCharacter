using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Unit unit;

    bool isAkey;
    int isSkill;
    Ray ray;
    RaycastHit hit;
    public LayerMask mask;

    void Start()
    {
        
    }


    void Update()
    {
        InputMouse();
        InputButtons();
        InputUnitMousePosition();
    }

    public void InputMouse()
    {
        ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 15f));
        if (Input.GetMouseButtonDown(0))             //¿Þ
        {
            if (Physics.Raycast(ray, out hit, float.MaxValue, mask))
            {
                if (isAkey)
                {

                }
                else if(isSkill != -1)
                {
                    UnitSkills(isSkill, hit);
                }
                else
                {

                }
            }
            
            isAkey = false;
            isSkill = -1;
        }
        else if (Input.GetMouseButtonDown(1))        //¿ì
        {
            ray = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 15f));

            if (Physics.Raycast(ray, out hit, float.MaxValue, mask))
            {
                if (isAkey)
                {
                    if(hit.transform.GetComponent<Unit>() != null)
                    {

                    }
                    else
                    {
                        unit.MovePoint(ZeroY(hit.point));
                    }
                }
                else
                {
                    if (hit.transform.GetComponent<Unit>() != null)
                    {

                    }
                    else
                    {
                        unit.MovePoint(ZeroY(hit.point));
                    }
                }
            }

            isAkey = false;
            isSkill = -1;
        }
    }

    public void InputButtons()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            isAkey = true;
            isSkill = -1;
            unit.ViewNomalAttack();
        }
        else if(Input.GetKeyDown(KeyCode.S))
        {
            isAkey = false;
            isSkill = -1;
            unit.MovePoint(transform.position);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            isAkey = false;
            if(unit.ViewSkill_0())
            {
                isSkill = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            isAkey = false;
            if (unit.ViewSkill_1())
            {
                isSkill = 1;
            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            isAkey = false;
            if (unit.ViewSkill_2())
            {
                isSkill = 2;
            }
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            isAkey = false;
            if (unit.ViewSkill_3())
            {
                isSkill = 3;
            }
        }
    }

    public void UnitSkills(int index, RaycastHit hit)
    {
        switch(index)
        {
            case 0:
                unit.UseSkill_0(hit);
                break;
            case 1:
                unit.UseSkill_1(hit);
                break;
            case 2:
                unit.UseSkill_2(hit);
                break;
            case 3:
                unit.UseSkill_3(hit);
                break;
        }
    }

    public void InputUnitMousePosition()
    {
        if (Physics.Raycast(ray, out hit, float.MaxValue, mask))
        {
            unit.SetMousePositon(hit.point);
        }
    }

    public Vector3 ZeroY(Vector3 vector)
    {
        return new Vector3(vector.x, 0, vector.z);
    }
}

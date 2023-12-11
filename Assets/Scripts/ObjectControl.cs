using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class ObjectControl : MonoBehaviour

{
    public HexCell targetCell;
    Ship targetShip;

    // public void OnMouseDown()
    // {
    //     Debug.Log("按下");
    //     targetShip.setVector3(targetCell.transform.position);
    //     Debug.Log(targetShip.getVector3());
    // }

    public ObjectControl()
    {
    }

    public void move()
    {

        int layerMask = 1 << 1;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool b = Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask);
        if (Input.GetMouseButtonDown(2))
        {
            if (b)
            {
                targetShip = hit.collider.gameObject.GetComponent<Ship>();
                Debug.Log("Hit object name: " + hit.collider.gameObject.name);
                targetShip.Movable = (!targetShip.Movable);

            }
        }
        if (targetShip.isMovable())
        {
            Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit2;
            if (Input.GetMouseButtonDown(0))
            {
                if (targetShip.isfriend)
                {
                    if (Physics.Raycast(ray2, out hit2))
                    {

                        Debug.Log("按下左键，移动友军");
                        targetShip.move(hit2.transform.position);
                    }
                }



            }
            if (Input.GetMouseButtonDown(1))
            {
                if (!targetShip.isfriend)
                {
                    Debug.Log("右键");
                    if (Physics.Raycast(ray, out hit2))
                    {
                        Debug.Log("按下右键，移动敌军");
                        targetShip.move(hit2.transform.position);
                    }
                }
            }
        }
    }

    void Update()
    {
        move();
    }
}

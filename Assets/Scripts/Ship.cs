using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public Vector3 v3;
    public bool isfriend =true;
    public bool Movable = false;
    public int HP;
    public int MF;//Moving force
    public int Aggressivity;//攻击力
    public Ship targetObject;
    public void move(Vector3 position)
    {
        targetObject.transform.position = position;
    }
    public void setVector3(Vector3 v3)
    {
        this.v3 = v3;
    }
    public Vector3 getVector3()
    {
        return v3;
    }
    public bool getFriendly()
    {
        return isfriend;
    }
    public Ship()
    {
    }

    public bool isMovable()
    {
        return Movable;
    }

  

    //攻击范围
    public void attack(Ship enemyShip){
        enemyShip.HP=enemyShip.HP-this.Aggressivity;
    } 
    
    
    
    
     void Update()
    {

    }
}

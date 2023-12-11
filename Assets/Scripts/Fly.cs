using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Fly : MonoBehaviour

{
	private GameObject gameObject;
    public double speed;
    public double power;
    public double weight;
    public double lift;

    public void move(){
        if (Input.GetKey(KeyCode.Space))
		{
			transform.position = new Vector3();
		}


    }
}
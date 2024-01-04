using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLight : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 MousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        //Vector3 difference = Camera.main.ScreenToWorldPoint(MousePos) - transform.position;
        //difference.Normalize();

        transform.position = Camera.main.ScreenToWorldPoint(MousePos);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {
    public int RotationOffset;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 MousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 17);
        Vector3 difference = Camera.main.ScreenToWorldPoint(MousePos) - transform.position;
        difference.Normalize();

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + RotationOffset);
    }
}

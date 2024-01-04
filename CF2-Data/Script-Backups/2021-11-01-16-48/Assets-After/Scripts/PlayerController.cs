using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = speed * ControlFreak2.CF2Input.GetAxis("Mouse X");
        float vertical = speed * ControlFreak2.CF2Input.GetAxis("Mouse Y");

        transform.Translate(horizontal, 0, vertical);

        //if (ControlFreak2.CF2Input.GetKey(KeyCode.A))
        //{
        //    transform.Translate(Vector3.left * speed);
        //}
        //else if (ControlFreak2.CF2Input.GetKey(KeyCode.D))
        //{
        //    transform.Translate(Vector3.right * speed);
        //}

        //if (ControlFreak2.CF2Input.GetKey(KeyCode.W))
        //{
        //    transform.Translate(Vector3.forward * speed);
        //} 
        //else if (ControlFreak2.CF2Input.GetKey(KeyCode.S))
        //{
        //    transform.Translate(Vector3.back * speed);
        //}
    }
}

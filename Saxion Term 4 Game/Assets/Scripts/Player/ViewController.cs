using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    private float sensitivity = 5;
    float rotY;

	void Start()
    {
        //Cursor.visible = false;
    }

    void Update()
    {
        Camera.main.transform.position = this.transform.position + new Vector3(0,0.85f,0);

        transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivity, 0);       

        rotY += Input.GetAxis("Mouse Y") * sensitivity;
        rotY = Mathf.Clamp(rotY, -90f, 90f);
        Camera.main.transform.localRotation = Quaternion.Euler(-rotY, transform.eulerAngles.y, 0f);
	}
}

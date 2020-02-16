using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl: MonoBehaviour
{
    public Camera camera;
	public GameObject overCamera;
    public float theta;
	private GameObject center;
    private float rotateSpeed;
	private int lineDisappearControl;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
		overCamera = GameObject.FindWithTag ("overCamera");
        center = GameObject.Find("OPoint");
        theta = 0;
		rotateSpeed = 5.0f;
		lineDisappearControl = 0;

    }

    // Update is called once per frame
    void Update()
    {
        //camera.gameObject.transform.Translate(new Vector3(0.0f, 0.0f, -0.1f));
		if (lineDisappearControl == 0) {
			transform.RotateAround (Vector3.zero, -1 * transform.right, 2);
			overCamera.transform.RotateAround (Vector3.zero, -1 * transform.right, 2);

			lineDisappearControl = 1;
		}
        
        if (Input.GetKey(KeyCode.LeftArrow))
		{
			transform.RotateAround (Vector3.zero, -1 * Vector3.up , 3);
			overCamera.transform.RotateAround (Vector3.zero, -1 * Vector3.up , 3);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
			transform.RotateAround(Vector3.zero, Vector3.up, 3);
			overCamera.transform.RotateAround(Vector3.zero, Vector3.up, 3);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.RotateAround(Vector3.zero, transform.right, 2);
			overCamera.transform.RotateAround(Vector3.zero, transform.right, 2);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.RotateAround(Vector3.zero, -1 * transform.right, 2);
			overCamera.transform.RotateAround(Vector3.zero, -1 * transform.right, 2);
        }


        /*
        Vector3 angle = new Vector3(Input.GetAxis("Mouse X") * rotateSpeed, Input.GetAxis("Mouse Y") * rotateSpeed, 0);

        //transform.RotateAround()をしようしてメインカメラを回転させる
        camera.transform.RotateAround(center.transform.position, Vector3.up, angle.x);
        camera.transform.RotateAround(center.transform.position, transform.right, angle.y);
        */

    }

	/*
	 * mystery error ocured.
	 * When Camera position is around Vector3(0-1,0-1,0-infinity), sometimes line will disappear.
	 * I can not understand why the error hapend.
	 */
}

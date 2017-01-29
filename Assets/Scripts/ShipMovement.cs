using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour {

    private Camera myCam;
    private Rigidbody myRB;
    private ThrusterParticles myTP;

    public float fwdThrust;
    public float aftThrust;
    public float strafeThrust;

	// Use this for initialization
	void Start () {
        myCam = GameObject.Find("Main Camera").GetComponent<Camera>();
        myRB = GetComponent<Rigidbody>();
        myTP = transform.Find("ThrusterParticles").GetComponent<ThrusterParticles>();
	}
	
	// Update is called once per frame
	void Update () {
        RotateShip();
        CheckThrust();
	}

    void RotateShip()
    {
        Vector3 cursorPoint = myCam.ScreenToWorldPoint(Input.mousePosition);
        //Debug.Log(cursorPoint);
        transform.rotation = Quaternion.Euler(0, (Mathf.Atan2(cursorPoint.x - transform.position.x, cursorPoint.z - transform.position.z) * Mathf.Rad2Deg), 0);
    }
    void CheckThrust()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            myRB.AddForce(transform.forward * fwdThrust, ForceMode.Impulse);
            myTP.ThrustAnim(ThrustDir.fwd);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            myRB.AddForce(transform.forward * -aftThrust, ForceMode.Impulse);
            myTP.ThrustAnim(ThrustDir.aft);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            myRB.AddForce(transform.right * strafeThrust, ForceMode.Impulse);
            myTP.ThrustAnim(ThrustDir.stbd);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            myRB.AddForce(transform.right * -strafeThrust, ForceMode.Impulse);
            myTP.ThrustAnim(ThrustDir.port);
        }
    }
}

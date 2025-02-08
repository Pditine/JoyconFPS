using System;
using System.Collections;
using System.Collections.Generic;
using PurpleFlowerCore;
using UnityEngine;

public class Gun : MonoBehaviour {
	
	[SerializeField]private List<Joycon> joycons;

	private Joycon Joycon => joycons.Count >= jcInd + 1 ? joycons[jcInd] : null;

	public float[] stick;
    public Vector3 gyro;
    public Vector3 accel;
    public int jcInd = 0;
    public Quaternion orientation;

    void Start ()
    {
        gyro = new Vector3(0, 0, 0);
        accel = new Vector3(0, 0, 0);
        // get the public Joycon array attached to the JoyconManager in scene
        joycons = JoyconManager.Instance.j;
		if(joycons.Count < jcInd+1)
		{
			Destroy(gameObject);
		}
	}
    
    private void Update ()
    {
	    if(Joycon == null) return;
		GetInput();
		
        // gameObject.transform.rotation = new Quaternion(orientation.x, orientation.y, orientation.z, orientation.w);
        gameObject.transform.rotation = new Quaternion(orientation.x, -orientation.z, orientation.y, orientation.w);
        
		if(Joycon.GetButtonDown(Joycon.Button.SHOULDER_2)) Fire();
		if(Joycon.GetButtonDown(Joycon.Button.SHOULDER_1)) ResetGun();
        
    }

    private void GetInput()
    {
	    stick = Joycon.GetStick();

	    // Gyro values: x, y, z axis values (in radians per second)
	    gyro = Joycon.GetGyro();

	    // Accel values:  x, y, z axis values (in Gs)
	    accel = Joycon.GetAccel();
            
	    orientation = Joycon.GetVector();
    }

    private void ResetGun()
    {
	    Joycon.Recenter();
    }

    public void Fire()
    {
	    PFCLog.Debug("Shoulder button 2 pressed - Fire");

	    //震动
	    // Rumble for 200 milliseconds, with low frequency rumble at 160 Hz and high frequency rumble at 320 Hz. For more information check:
	    // https://github.com/dekuNukem/Nintendo_Switch_Reverse_Engineering/blob/master/rumble_data_table.md

	    Joycon.SetRumble (160, 220, 0.6f, 100);

	    // The last argument (time) in SetRumble is optional. Call it with three arguments to turn it on without telling it when to turn off.
	    // (Useful for dynamically changing rumble values.)
	    // Then call SetRumble(0,0,0) when you want to turn it off.
	    
	    
    }
}
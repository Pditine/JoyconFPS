using System.Collections.Generic;
using PurpleFlowerCore;
using UnityEngine;

namespace GamePlay
{
	public class Grenade : MonoBehaviour 
	{
		private List<Joycon> _joycons;

		private Joycon Joycon => _joycons.Count >= jcInd + 1 ? _joycons[jcInd] : null;

		public float[] stick;
		public Vector3 gyro;
		public Vector3 accel;
		public int jcInd = 0;
		public Quaternion orientation;
		private bool _hadThrown = false;

		[SerializeField] private GameObject grenadeGameObject;
		
		[SerializeField] private float xforce  = 1;
		[SerializeField] private float yforce  = 1;
		[SerializeField] private float zforce  = 1;
		[SerializeField] private float xoffset = 0;
		[SerializeField] private float yoffset = 1;
		[SerializeField] private float zoffset = 1;
		[SerializeField] private Vector2 xRange = new Vector2(-1, 1);
		[SerializeField] private Vector2 yRange = new Vector2(-1, 1);
		[SerializeField] private Vector2 zRange = new Vector2(-1, 1);
		[SerializeField] private float cd = 0.5f;
		private float _currentCd;
		
		void Start ()
		{
			gyro = new Vector3(0, 0, 0);
			accel = new Vector3(0, 0, 0);
			// get the public Joycon array attached to the JoyconManager in scene
			_joycons = JoyconManager.Instance.j;
			if(_joycons.Count < jcInd+1)
			{
				Destroy(gameObject);
			}
		}
    
		private void Update ()
		{
			if(Joycon == null) return;
			GetInput();
			_currentCd += Time.deltaTime;
			gameObject.transform.rotation = new Quaternion(orientation.x, -orientation.z, orientation.y, orientation.w);
			Throw();
			ResetGun();
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

		private void Throw()
		{
			if(!Joycon.GetButtonDown(Joycon.Button.SHOULDER_2) || _currentCd < cd ) return;
			_currentCd = 0;
			var obj = Instantiate(grenadeGameObject, transform.position, transform.rotation);
			var rigidbody = obj.GetComponent<Rigidbody>();
			rigidbody.useGravity = true;
			float x = accel.x * xforce + xoffset;
			float y = accel.y * yforce + yoffset;
			float z = accel.z * zforce + zoffset;
			x = Mathf.Clamp(x, xRange.x, xRange.y);
			y = Mathf.Clamp(y, yRange.x, yRange.y);
			z = Mathf.Clamp(z, zRange.x, zRange.y);
			rigidbody.AddForce(x, y, z, ForceMode.Impulse);
		}
		
		private void ResetGun()
		{
			if (!Joycon.GetButtonDown(Joycon.Button.SHOULDER_1)) return; 
			Joycon.Recenter();
		}
	}
}
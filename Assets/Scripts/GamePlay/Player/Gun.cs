using System.Collections.Generic;
using PurpleFlowerCore;
using UnityEngine;

namespace GamePlay
{
	public class Gun : MonoBehaviour 
	{
		private List<Joycon> _joycons;

		private Joycon Joycon => _joycons.Count >= jcInd + 1 ? _joycons[jcInd] : null;

		public float[] stick;
		public Vector3 gyro;
		public Vector3 accel;
		public int jcInd = 0;
		public Quaternion orientation;
    
		[SerializeField]private float reloadTime = 1f;
		private float _currentReloadTime = 0;
		[SerializeField] private float fireCD = 0.1f;
		private float _currentFireCD;
		[SerializeField] private Bullet bullet;
		[SerializeField] private Transform laser;
		[SerializeField] private Animator animator;
		private bool _isLoading = false;
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
			_currentReloadTime += Time.deltaTime;
			_currentFireCD += Time.deltaTime;
			
			SyncOrientation();
			Fire();
			ResetGun();
			Reload();
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
		
		private void SyncOrientation()
		{
			// gameObject.transform.rotation = orientation;
			gameObject.transform.rotation = new Quaternion(orientation.x, -orientation.z, orientation.y, orientation.w);
		}

		private void ResetGun()
		{
			if (!Joycon.GetButtonDown(Joycon.Button.SHOULDER_1)) return; 
			Joycon.Recenter();
		}

		private void Fire()
		{
			if (!Joycon.GetButtonDown(Joycon.Button.SHOULDER_2)) return;
			PFCLog.Debug("Shoulder button 2 pressed - Fire");

			Joycon.SetRumble (160, 320, 0.6f, 100);
	    
			var theBullet = Instantiate(bullet, laser.position, laser.rotation);
			theBullet.Init(-laser.up);  
		}
    
		private void Reload()
		{
			if(!(orientation.y > 0.4f && orientation.z > 0.5f))
			{
				_currentReloadTime = 0;
				_isLoading = false;
				animator.Play("Idle", 0, 0);
				return;
			}

			if (!_isLoading)
			{
				animator.Play("Reload", 0, 0);
			}
			_isLoading = true;
			_currentReloadTime += Time.deltaTime;
			if (_currentReloadTime >= reloadTime)
			{
				DoReload();
				_currentReloadTime = 0;
			}
		}

		private void DoReload()
		{
			PFCLog.Debug("Reloaded");
		}
	}
}
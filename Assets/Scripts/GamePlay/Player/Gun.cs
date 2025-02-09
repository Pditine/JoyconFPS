using System;
using System.Collections.Generic;
using DefaultNamespace;
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
    
		[SerializeField]private float reloadCD = 3f;
		private float _currentReloadCD = 0;
		[SerializeField] private float fireCD = 0.1f;
		private float _currentFireCD;
		[SerializeField] private Bullet bullet;
		[SerializeField] private Transform laser;
		[SerializeField] private Animator animator;
		private bool _isLoading = false;
		[SerializeField] private int maxAmmo = 10;
		[SerializeField] private float recoil = -10;
		[SerializeField] private AudioClip fireSound;
		private float _currentRecoil;
		private int _currentAmmo = 10;
		public event Action<int, int> OnAmmoChange;
		public event Action OnReload;
		public event Action EndReload;
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
			// _currentReloadCD += Time.deltaTime;
			_currentFireCD += Time.deltaTime;
			_currentReloadCD += Time.deltaTime;
			SyncOrientation();
			Fire();
			ResetGun();
			Reload();
		}

		private void FixedUpdate()
		{
			_currentRecoil = Mathf.Lerp(_currentRecoil, 0, 0.1f);
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
			gameObject.transform.Rotate(_currentRecoil, 0, 0);
		}

		private void ResetGun()
		{
			if (!Joycon.GetButtonDown(Joycon.Button.SHOULDER_1)) return; 
			Joycon.Recenter();
		}

		private void Fire()
		{
			if (!Joycon.GetButtonDown(Joycon.Button.SHOULDER_2) || _currentFireCD < fireCD) return;
			if (_currentAmmo <= 0) return;
			AudioSystem.PlayEffect(fireSound,transform);
			_currentRecoil += recoil;
			_currentAmmo--;
			OnAmmoChange?.Invoke(_currentAmmo, maxAmmo);
			PFCLog.Debug("Shoulder button 2 pressed - Fire");
			_currentFireCD = 0;
			if(GameManager.Instance.rumbleOn)
				Joycon.SetRumble (160, 320, 0.6f, 100);
	    
			var theBullet = Instantiate(bullet, laser.position, laser.rotation);
			theBullet.Init(-laser.up);
		}
    
		private void Reload()
		{
			if(!(orientation.y > 0.4f && orientation.z > 0.5f))
			{
				_isLoading = false;
				animator.Play("Idle", 0, 0);
				EndReload?.Invoke();
				OnAmmoChange?.Invoke(_currentAmmo, maxAmmo);
				return;
			}

			if (!_isLoading && _currentReloadCD > reloadCD)
			{
				OnReload?.Invoke();
				animator.Play("Reload", 0, 0);
			}
			_isLoading = true;
		}

		private void DoReload()
		{
			_currentAmmo = maxAmmo;
			_currentReloadCD = 0;
			animator.Play("Idle", 0, 0);
			OnAmmoChange?.Invoke(_currentAmmo, maxAmmo);
			PFCLog.Debug("Reloaded");
		}
	}
}
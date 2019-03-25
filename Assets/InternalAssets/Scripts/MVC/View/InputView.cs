using System;
using InternalAssets.Scripts.Extensions;
using UnityEngine;

namespace InternalAssets.Scripts.MVC.View
{
	public class InputView : MonoBehaviour
	{
		public event Action<float> HorizontalAxis;
		public event Action<float> VerticalAxis;
		public event Action<float> WeaponChange;
		public event Action Fire;

		private bool _weaponPress;
		private bool _firePress;
		
		private void Start()
		{
			
		}

		private void Update()
		{
			var horizontal = Input.GetAxis("Horizontal");
			if (!Mathf.Approximately(horizontal, 0f))
			{
				//Debug.Log("Horizontal " + horizontal);
				HorizontalAxis.SafeInvoke(horizontal);
			}
			
			var vertical = Input.GetAxis("Vertical");
			if (!Mathf.Approximately(vertical, 0f))
			{
				//Debug.Log("Vertical " + vertical);
				VerticalAxis.SafeInvoke(vertical);
			}
			
			var fire = Input.GetAxis("Fire1");
			if (!Mathf.Approximately(fire, 0f) && !_firePress)
			{
				_firePress = true;
				Fire.SafeInvoke();
			}
			
			if (Mathf.Approximately(fire, 0f))
			{
				_firePress = false;
			}
			
			var weapon = Input.GetAxis("WeaponChange");
			if (!Mathf.Approximately(weapon, 0f) && !_weaponPress)
			{
				_weaponPress = true;
				WeaponChange.SafeInvoke(weapon);
			}
			
			if (Mathf.Approximately(weapon, 0f))
			{
				_weaponPress = false;
			}

			if (Input.GetKeyDown(KeyCode.X))
			{
				Fire.SafeInvoke();
			}
			
			
		}
	}
}
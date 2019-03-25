using System;
using InternalAssets.Scripts.Extensions;
using InternalAssets.Scripts.MVC.Model;
using UnityEngine;

namespace InternalAssets.Scripts.MVC.Controller
{
	public class DamageComponent
	{		
		private AbstractObjectModel _model;

		public DamageComponent(AbstractObjectModel model)
		{
			_model = model;
		}

		public bool TakeDamage(int damage)
		{
			var incomeDamage = damage * (1 - _model.Armor);
			_model.Health -= (int)incomeDamage;
			
			Debug.Log("take damage " + incomeDamage + " " + _model.Health);
			
			return _model.Health > 0;
		}
	}
}
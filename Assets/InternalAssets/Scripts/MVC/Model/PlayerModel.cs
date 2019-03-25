using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using InternalAssets.Data;
using InternalAssets.Scripts.Common.Custom;
using InternalAssets.Scripts.Common.Enum;
using InternalAssets.Scripts.Common.Weapon;
using UnityEngine;

namespace InternalAssets.Scripts.MVC.Model
{
	public class PlayerModel : AbstractObjectModel
	{
		private readonly CustomIterator<IWeapon> _weaponIterator;
		private IWeapon _currentWeapon;

		public PlayerModel(PlayerRecord record)
		{
			var weapon = WeaponFactory.GetList(record.Weapon);
			_weaponIterator = new CustomIterator<IWeapon>(weapon);
			
			//Debug.Log("Weapon inited " + _weaponIterator.Length);

			Health = record.Health;
			Armor = record.Armor;
			Speed = record.Speed;
			_currentWeapon = _weaponIterator.Current();
		}

		public void NextWeapon()
		{
			_weaponIterator.Next();
			_currentWeapon = _weaponIterator.Current();
		}
		
		public void PrevWeapon()
		{
			_weaponIterator.Prev();
			_currentWeapon = _weaponIterator.Current();
		}

		public IWeapon CurrentWeapon()
		{
			return _currentWeapon;
		}
		
		public override int GetDamage()
		{
			return _currentWeapon.GetDamage();
		}

		public override string GetName()
		{
			return "Player";
		}
	}
}
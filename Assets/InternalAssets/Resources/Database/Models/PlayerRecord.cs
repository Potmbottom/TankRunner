using System;
using System.Collections.Generic;
using InternalAssets.Scripts.Common.Enum;
using InternalAssets.Scripts.MVC.View;
using UnityEngine;

namespace InternalAssets.Data
{
	[Serializable]
	public class PlayerRecord
	{
		public int Speed;
		[Range(0,1)]public float Armor;
		public int Health;
		public List<WeaponType> Weapon;
		public PlayerType Type;
		public PlayerView PlayerPrefab;
	}
}
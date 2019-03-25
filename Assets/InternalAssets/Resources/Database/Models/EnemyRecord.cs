using System;
using InternalAssets.Scripts.Common.Enum;
using InternalAssets.Scripts.MVC.View;
using UnityEngine;

namespace InternalAssets.Data
{
	[Serializable]
	public class EnemyRecord
	{
		public EnemyType Type;
		public int Speed;
		public int Health;
		[Range(0,1)]public float Armor;
		public int Damage;
		public float Cooldown;
		public CommandType Command;
		public CollisionView Prefab;
	}
}
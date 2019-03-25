using System;
using InternalAssets.Data;
using InternalAssets.Scripts.Common.AiCommand;
using InternalAssets.Scripts.Common.Enum;

namespace InternalAssets.Scripts.MVC.Model
{
	public sealed class SmartZombieModel : AbstractEnemyModel
	{
		public SmartZombieModel(EnemyType type, IAiCommand command, EnemyRecord record)
		{
			EnemyType = type;
			Health = record.Health;
			Armor = record.Armor;
			Speed = record.Speed;
			Damage = record.Damage;
			Cooldown = record.Cooldown;
			Command = command;
		}
		
		public override int GetDamage()
		{
			return Damage;
		}

		public override string GetName()
		{
			return EnemyType.ToString();
		}
	}
}
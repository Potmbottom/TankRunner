using System;
using InternalAssets.Scripts.Common.AiCommand;
using InternalAssets.Scripts.Common.Enum;

namespace InternalAssets.Scripts.MVC.Model
{
	public abstract class AbstractEnemyModel : AbstractObjectModel
	{
		public float Cooldown;
		public EnemyType EnemyType;
		public IAiCommand Command;
	}
}
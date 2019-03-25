using InternalAssets.Scripts.MVC.Model;
using UnityEngine;

namespace InternalAssets.Scripts.MVC.Controller
{
	public class UnitController
	{
		private readonly MoveComponent _moveComponent;
		private readonly DamageComponent _damageComponent;

		public UnitController(AbstractObjectModel model)
		{
			_moveComponent = new MoveComponent(model);
			_damageComponent = new DamageComponent(model);
		}

		public void InitMoveComponent(Rigidbody rigidbody)
		{
			_moveComponent.Init(rigidbody);
		}

		public virtual bool TakeDamage(int damage)
		{
			return _damageComponent.TakeDamage(damage);
		}

		public virtual void Move(float value)
		{
			_moveComponent.MoveForward(value);
		}
		
		public virtual void Rotate(Vector3 direction, float value)
		{
			_moveComponent.Rotate(direction, value);
		}

		private void OnDie()
		{
			
		}
	}
}
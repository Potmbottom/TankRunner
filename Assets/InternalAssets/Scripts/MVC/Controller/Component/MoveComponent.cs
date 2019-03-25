using InternalAssets.Scripts.MVC.Model;
using UnityEngine;

namespace InternalAssets.Scripts.MVC.Controller
{
	public class MoveComponent
	{
		private AbstractObjectModel _model;
		private Rigidbody _rigidbody;
		
		public MoveComponent(AbstractObjectModel model)
		{
			_model = model;
		}

		public void Init(Rigidbody rigidbody)
		{
			_rigidbody = rigidbody;
		}

		public void MoveForward(float value)
		{
			var transform = _rigidbody.transform;
			//var direction = value > 0 ? transform.forward : -transform.forward;
			_rigidbody.velocity = (transform.forward * _model.Speed) * value;
		}

		public void MoveInDirection(Vector3 direction)
		{
			_rigidbody.velocity = (direction * _model.Speed);
		}
		
		public void Rotate(Vector3 direction, float value)
		{
			_rigidbody.transform.Rotate(direction, value * 3);
		}
	}
}
using UnityEngine;

namespace InternalAssets.Scripts.MVC.View
{
	public class BoxView : CollisionView
	{
		private MeshRenderer _renderer;
		private float _time;
		
		private void Start()
		{
			_renderer = GetComponent<MeshRenderer>();
		}
		
		private void Update()
		{		
			if (!_renderer.isVisible)
			{
				_time += Time.deltaTime;
				if(_time > 3)
					InvokeOutOfCamera();
			}
			else
			{
				_time = 0;
			}
		}
		
		public override void Dispose()
		{
			_time = 0;
		}
	}
}
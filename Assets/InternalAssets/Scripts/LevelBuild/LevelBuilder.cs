using InternalAssets.Scripts.MVC.Controller;
using InternalAssets.Scripts.MVC.Model;
using UnityEngine;

namespace InternalAssets.Scripts.LevelBuild
{
	public class LevelBuilder : MonoBehaviour
	{
		public LevelModel Container;

		private LevelController _controller;
		
		private void Start()
		{
			_controller = new LevelController(Container);
			_controller.BuildLevel();
		}		
	}
}
using InternalAssets.Data;
using UnityEngine;

namespace InternalAssets.Scripts.CommonManagers
{
	public class RestrictionsSpawnManager
	{
		private Vector3 _center;
		private Vector3 _size;
		
		public RestrictionsSpawnManager(Vector3 center)
		{
			_center = center;
		}

		public void Spawn()
		{
			SpawnTop();
			SpawnBot();
			SpawnLeft();
			SpawnRight();
		}

		private void SpawnTop()
		{
			var common = ScriptableUtils.GetCommonElements();
			var point = _center.z + common.RestrictionH / 2f;
			var element1 = Object.Instantiate(common.RestrictionObject);
			element1.transform.localScale = new Vector3(common.RestrictionH, 10, 10);
			element1.transform.position = new Vector3(_center.x, _center.y, point);
		}
		
		private void SpawnBot()
		{
			var common = ScriptableUtils.GetCommonElements();
			var point = _center.z - common.RestrictionH / 2f;
			var element1 = Object.Instantiate(common.RestrictionObject);
			element1.transform.localScale = new Vector3(common.RestrictionH, 10, 10);
			element1.transform.position = new Vector3(_center.x, _center.y, point);
		}
		
		private void SpawnLeft()
		{
			var common = ScriptableUtils.GetCommonElements();
			var point = _center.x + common.RestrictionW / 2f;
			var element1 = Object.Instantiate(common.RestrictionObject);
			element1.transform.localScale = new Vector3(10, 10, common.RestrictionW);
			element1.transform.position = new Vector3(point, _center.y, _center.z);
		}
		
		private void SpawnRight()
		{
			var common = ScriptableUtils.GetCommonElements();
			var point = _center.x - common.RestrictionW / 2f;
			var element1 = Object.Instantiate(common.RestrictionObject);
			element1.transform.localScale = new Vector3(10, 10, common.RestrictionW);
			element1.transform.position = new Vector3(point, _center.y, _center.z);
		}
	}
}
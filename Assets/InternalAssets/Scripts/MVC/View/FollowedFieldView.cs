using UnityEngine;

namespace InternalAssets.Scripts.MVC.View
{
	public class FollowedFieldView : MonoBehaviour
	{
		private const int TileSize = 40;
		
		private PlayerView _player;
		
		private void Start()
		{
			
		}
		
		private void Update()
		{
			if(_player == null) return;


			var size = _player.GetMeshSize();
			var playerTransform = _player.transform;
			if (Vector3.Distance(playerTransform.position, transform.position) > TileSize)
			{
				transform.position = GetPosition(_player.transform, size.y);
			}
		}
		

		public void SetPlayer(PlayerView player)
		{
			_player = player;
			
			var size = _player.GetMeshSize();
			transform.position = GetPosition(_player.transform, size.y);
		}

		private Vector3 GetPosition(Transform playerTransform, float ySize)
		{
			var x = playerTransform.position.x > transform.position.x
				? transform.position.x + TileSize
				: transform.position.x - TileSize;
			var z = playerTransform.position.z > transform.position.z
				? transform.position.z + TileSize
				: transform.position.z - TileSize;
			return new Vector3(x, playerTransform.position.y - ySize/2, z);
		}
		
		public Vector3 GetSpawnPosition()
		{
			return new Vector3();
		}
	}
}
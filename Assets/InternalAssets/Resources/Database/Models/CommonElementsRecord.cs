using InternalAssets.Scripts.Common.Camera;
using UnityEngine;

namespace InternalAssets.Resources.Database
{
	//[CreateAssetMenu(fileName = "CommonElementsRecord", menuName = "CommonElementsRecord", order = 51)]
	public class CommonElementsRecord : ScriptableObject
	{
		public GameObject RestrictionObject;
		public CameraFollower Camera;
		public int SpawnInnerRange;
		public int SpawnOutRange;
		public int RestrictionH;
		public int RestrictionW;
		public float EnemySpawnTime;
		public float EnvironmentSpawnTime;
	}
}
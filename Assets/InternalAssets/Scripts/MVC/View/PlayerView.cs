using InternalAssets.Scripts.MVC.Model;
using UnityEngine;

namespace InternalAssets.Scripts.MVC.View
{
	public class PlayerView : MonoBehaviour
	{
		public Transform CannonPivot;
		public Transform MachineGunPivot1;
		public Transform MachineGunPivot2;
		
		public ParticleSystem Cannon;
		public ParticleSystem MachineGun1;
		public ParticleSystem MachineGun2;
		
		private MeshFilter _mesh;
		
		public void Awake()
		{
			_mesh = GetComponent<MeshFilter>();
		}

		public void ModelUpdate(PlayerModel model)
		{
			
		}

		public Vector3 GetMeshSize()
		{
			var size = _mesh.mesh.bounds.size;
			var scale = transform.localScale;
			return new Vector3(size.x * scale.x, size.y * scale.y, size.z * scale.z);
		}
		
		
	}
}
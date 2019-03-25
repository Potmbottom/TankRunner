using System.Collections.Generic;
using UnityEngine;

namespace InternalAssets.Resources.Database.Models
{
	[CreateAssetMenu(fileName = "EnvironmentTable", menuName = "EnvironmentTable", order = 51)]
	public class EnvironmentTable : ScriptableObject
	{
		public List<EnvironmentRecord> Environment;
	}
}
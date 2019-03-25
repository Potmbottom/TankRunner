using System.Collections.Generic;
using UnityEngine;

namespace InternalAssets.Resources.Database.Models
{
	[CreateAssetMenu(fileName = "WeaponTable", menuName = "WeaponTable", order = 51)]
	public class WeaponTable : ScriptableObject
	{
		public List<WeaponRecord> WeaponRecords;
	}
}
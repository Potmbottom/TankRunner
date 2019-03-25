using Common.EventsSystem;
using InternalAssets.Scripts.Common.Enum;
using TMPro;
using UnityEngine;

namespace InternalAssets.Scripts.MVC.Ui
{
	public class KillCountView : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _killCount;

		private int _count;
		
		private void Start()
		{
			Subscribe();
		}

		private void Subscribe()
		{
			EventHolder<GameEventType>.Dispatcher.AddListener(GameEventType.PlayerDie,
				(string type, int killCount) =>
				{
					gameObject.SetActive(false);
				});
			EventHolder<GameEventType>.Dispatcher.AddListener(GameEventType.EnemyDie, SingleKill);
		}


		private void SingleKill()
		{
			_count++;
			_killCount.text = _count.ToString();
		}
		
	}
}
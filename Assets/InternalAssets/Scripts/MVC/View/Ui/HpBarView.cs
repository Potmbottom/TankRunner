using System;
using Common.EventsSystem;
using InternalAssets.Data;
using InternalAssets.Scripts.Common.Enum;
using UnityEngine;
using UnityEngine.UI;

namespace InternalAssets.Scripts.MVC.Ui
{
	public class HpBarView : MonoBehaviour
	{
		[SerializeField] private Image _hpBar;
		
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
			EventHolder<GameEventType>.Dispatcher.AddListener(GameEventType.HpChange, (int x) => { HpChange(x); });
		}

		private void HpChange(int currentHp)
		{
			var playerRecord = ScriptableUtils.GetCurrentPlayerRecord();
			var percent = (float)currentHp / playerRecord.Health;
			_hpBar.fillAmount = percent;
		}
	}
}
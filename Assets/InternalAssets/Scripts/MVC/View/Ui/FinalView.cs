using Common.EventsSystem;
using Core.Management;
using InternalAssets.Scripts.Common.Enum;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace InternalAssets.Scripts.MVC.Ui
{
	public class FinalView : MonoBehaviour
	{
		[SerializeField] private Transform _pivot;
		[SerializeField] private Button _restart;
		[SerializeField] private TextMeshProUGUI _killCount;
		[SerializeField] private TextMeshProUGUI _deathFrom;
		
		private void Awake()
		{
			gameObject.SetActive(false);
			_restart.onClick.AddListener(Restart);
			Subscribe();
		}

		private void Subscribe()
		{
			EventHolder<GameEventType>.Dispatcher.AddListener(GameEventType.PlayerDie,
				(string type, int killCount) =>
				{
					_killCount.text = "Kill count : " + killCount;
					_deathFrom.text = "Death from " + type;
					
					Debug.Log("Player diew");
					
					_pivot.gameObject.SetActive(true);
				});
		}

		private void Restart()
		{
			CoreManager.Instance.ClearAllData();
			EventHolder<GameEventType>.Clear();
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}		
	}
}
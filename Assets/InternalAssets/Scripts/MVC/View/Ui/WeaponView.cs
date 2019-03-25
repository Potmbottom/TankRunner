using System.Collections.Generic;
using System.Linq;
using Common.EventsSystem;
using InternalAssets.Data;
using InternalAssets.Scripts.Common.Enum;
using InternalAssets.Scripts.Common.Weapon;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InternalAssets.Scripts.MVC.Ui
{
	public class WeaponView : MonoBehaviour
	{
		[SerializeField] private Sprite[] _weaponSprites;
		[SerializeField] private Image _weapon;
		[SerializeField] private Image _bulletPrefab;
		[SerializeField] private Transform _bulletPivot;

		private List<Image> _bullets;

		private int _bulletCount;
		private WeaponType _currentType;

		private void Start()
		{
			_bulletCount = 25;
			_bullets = new List<Image>();
			CreateBullets();
			
			Subscribe();
		}

		private void Subscribe()
		{
			EventHolder<GameEventType>.Dispatcher.AddListener(GameEventType.PlayerDie,
				(string type, int killCount) =>
				{
					gameObject.SetActive(false);
				});
			EventHolder<GameEventType>.Dispatcher.AddListener(GameEventType.WeaponSwap,
				(IWeapon weapon) => { OnWeaponChange(weapon);});
			EventHolder<GameEventType>.Dispatcher.AddListener(GameEventType.AmmoChange,
				(WeaponType weapon, int current) => { OnShot(weapon, current);});
		}

		private void SetImage(WeaponType type)
		{
			var sprite = GetSprite(type);
			_weapon.sprite = sprite;
		}

		private Sprite GetSprite(WeaponType type)
		{
			return _weaponSprites.FirstOrDefault(x => x.name == type.ToString());
		}

		private void OnShot(WeaponType weapon, int current)
		{
			if(_currentType != weapon) return;
			
			var record = ScriptableUtils.GetWeaponRecord(weapon);
			var bullets = record.FireRate - current;
			EnableBullets(bullets);
		}

		private void OnWeaponChange(IWeapon weapon)
		{
			_currentType = weapon.GetWeaponType();
			SetImage(weapon.GetWeaponType());
			EnableBullets(weapon.GetFireRate());
		}

		private void EnableBullets(int count)
		{
			
			DisableAllBullets();
			for (var i = 0; i < count; i++)
			{
				_bullets[i].gameObject.SetActive(true);
			}
		}

		private void DisableAllBullets()
		{
			foreach (var item in _bullets)
			{
				item.gameObject.SetActive(false);
			}
		}

		private void CreateBullets()
		{
			for (var i = 0; i < _bulletCount; i++)
			{
				var bullet = Instantiate(_bulletPrefab);
				bullet.transform.SetParent(_bulletPivot, false);
				bullet.gameObject.SetActive(false);
				_bullets.Add(bullet);
			}
		}

	}
}
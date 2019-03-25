using System;
using InternalAssets.Scripts.Common.Enum;
using InternalAssets.Scripts.Common.Pool;
using InternalAssets.Scripts.Extensions;
using UniRx;
using UnityEngine;

namespace InternalAssets.Scripts.MVC.View
{
	[RequireComponent(typeof(Rigidbody))]
	public class ProjectileView : CollisionView
	{
		[SerializeField] private GameObject _body;
		[SerializeField] private ParticleSystem _particle;
		
		public event Action<ProjectileView, WeaponType> OnTimeEnd;
		public event Action<ProjectileView, WeaponType> EnemyCollision;
		
		private Rigidbody _rigidbody;
		private CompositeDisposable _disposable;

		private WeaponType _type;

		private void Start()
		{
			
		}

		private void Update()
		{
			if (_rigidbody.velocity == Vector3.zero) return;
			
			if(_rigidbody == null) return;
			transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
		}
		
		public void Init(WeaponType type)
		{
			_type = type;
		}

		public Rigidbody GetRigidbody()
		{
			if (_rigidbody == null)
			{
				_rigidbody = GetComponent<Rigidbody>();
			}

			return _rigidbody;
		}

		public void StartTimer()
		{
			_disposable = new CompositeDisposable();
			Observable.Timer (TimeSpan.FromSeconds (2))
				.Subscribe (_ => {
					Dispose();
					_particle.Play();
					OnTimeEnd.SafeInvoke(this, _type);
				}).AddTo (_disposable);
		}

		public override void Disable()
		{
			_body.SetActive(true);
			if(_rigidbody != null)
			_rigidbody.velocity = Vector3.zero;
			gameObject.SetActive(false);
		}

		public void Hide()
		{
			_body.SetActive(false);
		}
		
		private void OnCollisionEnter(Collision collision)
		{
			if (collision.gameObject.CompareTag("enemy"))
			{
				_particle.Play();
				EnemyCollision.SafeInvoke(this, _type);
			}
		}

		public override void Dispose()
		{
			base.Dispose();
			
			_disposable.Dispose();
		}
	}
}
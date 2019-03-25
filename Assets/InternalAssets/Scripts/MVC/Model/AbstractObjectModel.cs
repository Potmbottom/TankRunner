namespace InternalAssets.Scripts.MVC.Model
{
	public abstract class AbstractObjectModel
	{
		public int Health;
		public float Armor;
		public int Speed;

		protected int Damage;

		public virtual int GetDamage()
		{
			return Damage;
		}

		public abstract string GetName();
	}
}
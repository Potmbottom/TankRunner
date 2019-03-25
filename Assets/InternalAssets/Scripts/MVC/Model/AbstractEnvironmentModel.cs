using InternalAssets.Scripts.Common.Enum;

namespace InternalAssets.Scripts.MVC.Model
{
	public class AbstractEnvironmentModel : AbstractObjectModel
	{
		public EnvironmentType Type;


		public override string GetName()
		{
			return Type.ToString();
		}
	}
}
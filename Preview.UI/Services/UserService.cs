namespace Xylia.Preview.UI.Services;
internal class UserService : IService
{
	#region Methods
	public static UserService? Instance { get; private set; } = new();

	public bool Register()
	{
		throw new NotImplementedException();
	}
	#endregion

	#region Fields
	public UserRole Role;
	#endregion
}

public enum UserRole
{
	None,
	Normal,
	Advanced,
}
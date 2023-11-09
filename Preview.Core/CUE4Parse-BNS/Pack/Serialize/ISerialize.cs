namespace CUE4Parse.UE4.Serialize;
/// <summary>
/// this interface should used in CUE4Parse
/// </summary>
public interface ISerialize
{
	void Serialize(BinaryWriter writer, SerializeOption option = null);
}

namespace CUE4Parse.UE4.Objects.UObject;
public static class FString
{
	public static bool IsUnicode(this string String) => String.Any(c => c > 255);

	public static void WriteFString(this BinaryWriter writer, string String)
	{
		// > 0 for ANSICHAR, < 0 for UCS2CHAR serialization
		if (String.IsUnicode())
		{
			int Length = -(String.Length + 1);

			writer.Write(Length);
			writer.Write(System.Text.Encoding.Unicode.GetBytes(String));
			writer.Write((short)0);
		}
		else
		{
			int Length = String.Length + 1;

			writer.Write(Length);
			writer.Write(System.Text.Encoding.ASCII.GetBytes(String));
			writer.Write((byte)0);
		}
	}
}
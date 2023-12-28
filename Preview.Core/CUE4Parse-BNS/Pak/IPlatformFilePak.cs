namespace CUE4Parse.UE4.Pak;
public class IPlatformFilePak
{
	public static byte[] Signature;

	public static void DoSignatureCheck()
	{          
		// ComputeHash
		if (Signature == null) throw new Exception("package error");
	}
}
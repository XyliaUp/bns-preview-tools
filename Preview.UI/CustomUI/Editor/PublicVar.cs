namespace Crypto_Notepad
{
	static class PublicVar
	{
		public static EncryptedString encryptionKey = new EncryptedString();
		public static bool randomizeSalts = true;
		public static bool keyChanged = false;
		public static bool settingsChanged = false;
		public static bool okPressed = false;
		public const string appName = "文本编辑";
		public static string openFileName;
	}

	static class TypedPassword
	{
		public static string Value { get; set; }
	}
}
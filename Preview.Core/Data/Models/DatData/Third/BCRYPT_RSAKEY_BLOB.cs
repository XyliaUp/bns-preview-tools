using System;

// Token: 0x02000003 RID: 3
public struct BCRYPT_RSAKEY_BLOB
{
	// Token: 0x04000004 RID: 4
	public KeyBlobMagicNumber Magic;

	// Token: 0x04000005 RID: 5
	public int BitLength;

	// Token: 0x04000006 RID: 6
	public int cbPublicExp;

	// Token: 0x04000007 RID: 7
	public int cbModulus;

	// Token: 0x04000008 RID: 8
	public int cbPrime1;

	// Token: 0x04000009 RID: 9
	public int cbPrime2;
}

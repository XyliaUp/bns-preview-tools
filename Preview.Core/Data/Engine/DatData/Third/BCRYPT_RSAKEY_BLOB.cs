namespace Xylia.Preview.Data.Engine.DatData;
public struct BCRYPT_RSAKEY_BLOB
{
    public KeyBlobMagicNumber Magic;
    public int BitLength;
    public int cbPublicExp;
    public int cbModulus;
    public int cbPrime1;
    public int cbPrime2;
}

public enum KeyBlobMagicNumber
{
    BCRYPT_RSAPUBLIC_MAGIC = 826364754,
    BCRYPT_RSAPRIVATE_MAGIC = 843141970
}
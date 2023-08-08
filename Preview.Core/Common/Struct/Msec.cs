namespace Xylia.Preview.Common.Struct;
public struct Msec
{
    public int value;           

    public Msec(int value) => this.value = value;


	public static implicit operator Msec(int value) => new(value);
}
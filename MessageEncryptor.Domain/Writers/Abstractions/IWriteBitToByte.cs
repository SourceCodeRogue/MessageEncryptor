

namespace MessageEncryptor.Domain.Writers.Abstractions
{
    public interface IWriteBitToByte
    {
        byte WriteBit(byte targetByte, bool bit, ushort bitPosition);
    }
}

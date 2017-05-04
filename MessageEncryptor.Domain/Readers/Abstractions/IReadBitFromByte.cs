namespace MessageEncryptor.Domain.Readers.Abstractions
{
    public interface IReadBitFromByte
    {
        bool Read(byte sourceByte, ushort bitPosition);
    }
}

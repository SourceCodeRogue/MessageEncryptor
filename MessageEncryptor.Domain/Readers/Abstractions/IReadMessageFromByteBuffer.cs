namespace MessageEncryptor.Domain.Readers.Abstractions
{
    public interface IReadMessageFromByteBuffer
    {
        string Read(byte[] sourceBuffer, ushort numberOfBitsUsedInByte, uint totalNumberOfBytesUsed);
    }
}

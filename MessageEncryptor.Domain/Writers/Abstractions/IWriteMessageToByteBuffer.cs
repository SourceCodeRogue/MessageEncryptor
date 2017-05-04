using MessageEncryptor.Domain.Writers.Dtos;

namespace MessageEncryptor.Domain.Writers.Abstractions
{
    public interface IWriteMessageToByteBuffer
    {
        MessageWriteResult WriteMessage(string message, byte[] targetArray, ushort numberOfBitsToUse);
    }
}

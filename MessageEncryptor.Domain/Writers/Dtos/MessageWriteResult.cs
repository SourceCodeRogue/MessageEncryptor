
using System;

namespace MessageEncryptor.Domain.Writers.Dtos
{
    public class MessageWriteResult
    {
        public byte[] Buffer { get; }

        public uint TotalNumberOfBytesUsed { get; }

        public ushort NumberOfBitsUsedInByte { get; }

        public MessageWriteResult(byte[] buffer, uint totalNumberOfBytesUsed, ushort numberOfBitsUsedInByte)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            if (numberOfBitsUsedInByte > 8)
                throw new ArgumentOutOfRangeException(nameof(numberOfBitsUsedInByte));

            if (totalNumberOfBytesUsed > buffer.Length)
                throw new ArgumentOutOfRangeException(nameof(totalNumberOfBytesUsed));

            Buffer = buffer;
            TotalNumberOfBytesUsed = totalNumberOfBytesUsed;
            NumberOfBitsUsedInByte = numberOfBitsUsedInByte;
        }
    }
}

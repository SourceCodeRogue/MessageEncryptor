using System;
using MessageEncryptor.Domain.Readers.Abstractions;

namespace MessageEncryptor.Domain.Readers
{
    public class BitFromByteReader : IReadBitFromByte
    {
        public bool Read(byte sourceByte, ushort bitPosition)
        {
            if (bitPosition > 8)
                throw new ArgumentOutOfRangeException(nameof(bitPosition));

            var bitValue = ((sourceByte & (1 << bitPosition)) != 0);

            return bitValue;
        }
    }
}

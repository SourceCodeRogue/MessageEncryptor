using MessageEncryptor.Domain.Writers.Abstractions;
using System;

namespace MessageEncryptor.Domain.Writers
{
    public class BitToByteWriter : IWriteBitToByte
    {
        public byte WriteBit(byte targetByte, bool bitValue, ushort bitPosition)
        {
            if (bitPosition > 7)
                throw new ArgumentException(nameof(bitPosition));

            var resultByte = targetByte;

            if (bitValue)
            {
                //left-shift 1, then bitwise OR
                resultByte = (byte)(targetByte | (1 << bitPosition));
            }
            else
            {
                //left-shift 1, then take complement, then bitwise AND
                resultByte = (byte)(targetByte & ~(1 << bitPosition));
            }

            return resultByte;
        }
    }
}

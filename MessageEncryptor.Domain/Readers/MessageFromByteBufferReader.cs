using System;
using MessageEncryptor.Domain.Readers.Abstractions;
using System.Collections.Generic;

namespace MessageEncryptor.Domain.Readers
{
    public class MessageFromByteBufferReader : IReadMessageFromByteBuffer
    {
        private readonly IReadBitFromByte _bitReader;

        public MessageFromByteBufferReader(IReadBitFromByte bitReader)
        {
            if (bitReader == null)
                throw new ArgumentNullException(nameof(bitReader));

            _bitReader = bitReader;
        }

        public string Read(byte[] sourceBuffer, ushort numberOfBitsUsedInByte, uint totalNumberOfBytesUsed)
        {
            if (sourceBuffer == null)
                throw new ArgumentNullException(nameof(sourceBuffer));
            if (sourceBuffer.Length == 0)
                throw new ArgumentException("Buffer cannot be empty", nameof(sourceBuffer));
            if (numberOfBitsUsedInByte > 8)
                throw new ArgumentOutOfRangeException(nameof(numberOfBitsUsedInByte));
            if (numberOfBitsUsedInByte == 0)
                throw new ArgumentException("Value must be greater then zero", nameof(numberOfBitsUsedInByte));
            if (totalNumberOfBytesUsed > sourceBuffer.Length)
                throw new ArgumentOutOfRangeException(nameof(totalNumberOfBytesUsed));
            if (totalNumberOfBytesUsed == 0)
                throw new ArgumentException("Value must be greater then zero", nameof(totalNumberOfBytesUsed));

            string resultMessage = null;
            ushort byteThresholdBitCounter = 0;

            byte byteContainer = new byte();
            var messageRetrievedFromResultBuffer = new List<byte>();

            for (int byteIndex = 0; byteIndex < totalNumberOfBytesUsed; byteIndex++)
            {
                var currentByte = sourceBuffer[byteIndex];

                for (ushort bitInBytePositionCounter = 0; bitInBytePositionCounter < numberOfBitsUsedInByte; bitInBytePositionCounter++)
                {
                    var bit = _bitReader.Read(currentByte, bitInBytePositionCounter);
                    byteContainer = (byte)(byteContainer | (1 << byteThresholdBitCounter));
                    byteThresholdBitCounter++;

                    if (byteThresholdBitCounter == 8)
                    {                        
                        messageRetrievedFromResultBuffer.Add(byteContainer);
                        byteContainer = new byte();
                        byteThresholdBitCounter = 0;
                    }
                }
            }

            resultMessage = System.Text.Encoding.UTF8.GetString(messageRetrievedFromResultBuffer.ToArray(), 0, messageRetrievedFromResultBuffer.Count);

            return resultMessage;
        }
    }
}

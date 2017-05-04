using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using MessageEncryptor.Domain.Writers.Abstractions;
using MessageEncryptor.Domain.Writers.Dtos;

namespace MessageEncryptor.Domain.Writers
{
    public class MessageToByteBufferWriter : IWriteMessageToByteBuffer
    {
        private readonly IWriteBitToByte _bitToByteWriter;

        public MessageToByteBufferWriter(IWriteBitToByte bitToByteWriter)
        {
            if (bitToByteWriter == null)
                throw new ArgumentNullException(nameof(bitToByteWriter));

            _bitToByteWriter = bitToByteWriter;
        }

        private IEnumerable<bool> GetBitsFromByte(byte sourceByte)
        {
            var bits = new BitArray(new byte[] { sourceByte });
            
            return bits.Cast<bool>();
        }        

        private bool WillMessageFit(string message, byte[] targetArray, ushort numberOfBitsToUseInByte)
        {
            var messageBytes = System.Text.Encoding.UTF8.GetBytes(message);
            var numberOfBitsRequiredtoStoreMessage = messageBytes.Length * 8;
            var numberOfBitsAvailable = targetArray.Length * numberOfBitsToUseInByte;

            return numberOfBitsAvailable >= numberOfBitsRequiredtoStoreMessage;
        }

        public MessageWriteResult WriteMessage(string message, byte[] targetArray, ushort numberOfBitsToUse)
        {
            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException(nameof(message));

            if (targetArray == null)
                throw new ArgumentNullException(nameof(targetArray));

            if (numberOfBitsToUse > 8)
                throw new ArgumentOutOfRangeException(nameof(numberOfBitsToUse));

            var willMessageFit = WillMessageFit(message, targetArray, numberOfBitsToUse);

            if (!willMessageFit)
                throw new ArgumentException("Provided message is too big to be stored in the provided byte array");

            var resultByteBuffer = new List<byte>();
            var messageBytes = System.Text.Encoding.UTF8.GetBytes(message);
            var bitsToWrite = messageBytes.SelectMany(x => GetBitsFromByte(x)).ToArray();

            var numberOfBitsInMessage = messageBytes.Length * 8;
            uint totalNumberOfBytesUsed = (uint)Math.Ceiling((decimal)numberOfBitsInMessage / numberOfBitsToUse);

            ushort bitPositionCounter = 0;
            int targetByteCounter = 0;
            byte targetByte = byte.MinValue;

            foreach (var bit in bitsToWrite)
            {
                if (bitPositionCounter == 0)
                    targetByte = targetArray[targetByteCounter];

                targetByte = _bitToByteWriter.WriteBit(targetByte, bit, bitPositionCounter);
                bitPositionCounter++;

                if(bitPositionCounter >= numberOfBitsToUse)
                {
                    bitPositionCounter = 0;
                    targetByteCounter++;
                    resultByteBuffer.Add(targetByte);
                }
            }

            for (int i = resultByteBuffer.Count; i < targetArray.Length; i++)
                resultByteBuffer.Add(targetArray[i]);

            return new MessageWriteResult(resultByteBuffer.ToArray(), totalNumberOfBytesUsed, numberOfBitsToUse);
        }
    }
}

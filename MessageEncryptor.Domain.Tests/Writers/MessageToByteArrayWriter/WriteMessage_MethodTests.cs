using System;
using Xunit;
using FluentAssertions;

using MessageEncryptor.Domain.Tests.Attributes;
using System.Collections;
using System.Collections.Generic;
using MessageEncryptor.Domain.Writers.Dtos;

using SutMessageToByteBufferWriter = MessageEncryptor.Domain.Writers.MessageToByteBufferWriter;

namespace MessageEncryptor.Domain.Tests.Writers.MessageToByteBufferWriter
{
    
    public class Given_MessageToByteBufferWriter_When_WriteMessage_IsCalled
    {
        private string ReadFromWriteResult(MessageWriteResult writeResult)
        {
            string resultMessage = null;            
            ushort byteThresholdBitCounter = 0;

            var bitArray = new BitArray(8);
            var messageRetrievedFromResultBuffer = new List<byte>();

            for (int byteIndex = 0; byteIndex < writeResult.TotalNumberOfBytesUsed; byteIndex++)
            {
                var currentByte = writeResult.Buffer[byteIndex];

                for (ushort bitInBytePositionCounter = 0; bitInBytePositionCounter < writeResult.NumberOfBitsUsedInByte; bitInBytePositionCounter++)
                {
                    var bit = ((currentByte & (1 << bitInBytePositionCounter)) != 0);
                    bitArray.Set(byteThresholdBitCounter, bit);
                    byteThresholdBitCounter++;

                    if (byteThresholdBitCounter == 8)
                    {
                        var byteStorage = new byte[1];
                        bitArray.CopyTo(byteStorage, 0);
                        messageRetrievedFromResultBuffer.Add(byteStorage[0]);
                        bitArray.SetAll(false);
                        byteThresholdBitCounter = 0;
                    }
                }
            }

            resultMessage = System.Text.Encoding.UTF8.GetString(messageRetrievedFromResultBuffer.ToArray());

            return resultMessage;
        }

        [Theory, AutoNSubstituteData]
        public void Then_NoException_IsThrown(
            SutMessageToByteBufferWriter sut)
        {
            //a
            var messageStub = "Abc";
            var byteBufferStub = new byte[24];
            ushort numberOfBitstoUseDummy = 1;

            //aa
            Action writeMessageCall = () => sut.WriteMessage(messageStub, byteBufferStub, numberOfBitstoUseDummy);

            //aaa
            writeMessageCall.ShouldNotThrow();
        }

        [Theory, AutoNSubstituteData]
        public void WithNull_Message_Then_ArgumentNullException_IsThrown(
            byte[] byteArrayDumy,
            SutMessageToByteBufferWriter sut)
        {
            //a
            string nullMessageStub = null;
            ushort numberOfBitstoUseDummy = 1;

            //aa
            Action writeMessageCall = () => sut.WriteMessage(nullMessageStub, byteArrayDumy, numberOfBitstoUseDummy);

            //aaa
            writeMessageCall.ShouldThrowExactly<ArgumentNullException>();
        }

        [Theory, AutoNSubstituteData]
        public void WithNull_TargetArray_Then_ArgumentNullException_IsThrown(
            string messageDummy,
            SutMessageToByteBufferWriter sut)
        {
            //a
            byte[] nullByteBuffer = null;
            ushort numberOfBitstoUseDummy = 1;

            //aa
            Action writeMessageCall = () => sut.WriteMessage(messageDummy, nullByteBuffer, numberOfBitstoUseDummy);

            //aaa
            writeMessageCall.ShouldThrowExactly<ArgumentNullException>();
        }

        [Theory, AutoNSubstituteData]
        public void WithMessage_GreaterThan_TotalNumberOfBits_ChoosenToBeUsed_Then_ArgumentException_IsThrown(
            SutMessageToByteBufferWriter sut)
        {
            //a
            var messageStub = "Test message";
            var targetByteBuffer = new byte[4];
            ushort numberOfBitstoUseDummy = 1;

            //aa
            Action writeMessageCall = () => sut.WriteMessage(messageStub, targetByteBuffer, numberOfBitstoUseDummy);

            //aaa
            writeMessageCall.ShouldThrowExactly<ArgumentException>();
        }

        [Theory, AutoNSubstituteData]
        public void Then_ProvidedMessage_IsStored_InReturned_MessageWriteResult_Buffer(
            Domain.Writers.BitToByteWriter bitToByteWriterStub)
        {
            //a
            var messageStub = "Abc";
            var targetByteBuffer = new byte[24];
            ushort numberOfBitstoUseStub = 2;
            var sut = new SutMessageToByteBufferWriter(bitToByteWriterStub);

            //aa
            var writeResult = sut.WriteMessage(messageStub, targetByteBuffer, numberOfBitstoUseStub);

            //aaa
            var messageRetrieved = ReadFromWriteResult(writeResult);
            messageRetrieved.Should().Be(messageStub);
        }

        [Theory, AutoNSubstituteData]
        public void Then_MessageWriteResult_HasCorrect_TotalNumberOfBytesUsed(
            SutMessageToByteBufferWriter sut)
        {
            //a
            var messageStub = "Abc";
            var targetByteBuffer = new byte[24];
            ushort numberOfBitsToUseStub = 5;
      
            var numberOfBitsInMessage = messageStub.Length * 8;
            uint expectedTotalNumberOfBytesUsed = (uint)Math.Ceiling((decimal)numberOfBitsInMessage / numberOfBitsToUseStub);

            //aa
            var writeResult = sut.WriteMessage(messageStub, targetByteBuffer, numberOfBitsToUseStub);

            //aaa
            writeResult.TotalNumberOfBytesUsed.Should().Be(expectedTotalNumberOfBytesUsed);
        }
    }
}

using MessageEncryptor.Domain.Tests.Attributes;
using Xunit;
using FluentAssertions;
using System;
using MessageEncryptor.Domain.Writers;

using SutMessageFromByteBufferReader = MessageEncryptor.Domain.Readers.MessageFromByteBufferReader;

namespace MessageEncryptor.Domain.Tests.Readers.MessageFromByteBufferReader
{
    public class Given_MessageFromByteBufferReader_When_ReadIsCalled
    {
        [Theory, AutoNSubstituteData]        
        public void With_NumberOfBitsUsedInByte_EqualZero_Then_ArgumentException_IsThrown(
            SutMessageFromByteBufferReader sut)
        {
            //a
            var sourceBufferDummy = new byte[10];
            ushort numberOfbitsUsedInByteStub = 0;
            uint totalNuberOfBytesUsedDummy = 10;

            //aa
            Action readMethodCall = () => sut.Read(sourceBufferDummy, numberOfbitsUsedInByteStub, totalNuberOfBytesUsedDummy);

            //aaa
            readMethodCall.ShouldThrowExactly<ArgumentException>();
        }

        [Theory, AutoNSubstituteData]
        public void With_TotalNumberOfBytesUsed_EqualZero_AvailableNuberOfBits_Then_ArgumentException_IsThrown(
            SutMessageFromByteBufferReader sut)
        {
            //a
            var sourceBufferDummy = new byte[10];
            ushort numberOfbitsUsedInByteDummy = 4;
            uint totalNuberOfBytesUsedStub = 0;

            //aa
            Action readMethodCall = () => sut.Read(sourceBufferDummy, numberOfbitsUsedInByteDummy, totalNuberOfBytesUsedStub);

            //aaa
            readMethodCall.ShouldThrowExactly<ArgumentException>();
        }

        [Theory, AutoNSubstituteData]
        public void With_NumberOfBitsUsedInByte_OutsideOf_ByteRange_Then_ArgumentOutOfRangeException_IsThrown(
            SutMessageFromByteBufferReader sut)
        {
            //a
            var sourceBufferDummy = new byte[10];
            ushort numberOfbitsUsedInByteStub = 9;
            uint totalNuberOfBytesUsedDummy = 10;

            //aa
            Action readMethodCall = () => sut.Read(sourceBufferDummy, numberOfbitsUsedInByteStub, totalNuberOfBytesUsedDummy);

            //aaa
            readMethodCall.ShouldThrowExactly<ArgumentOutOfRangeException>();
        }

        [Theory, AutoNSubstituteData]
        public void With_TotalNumberOfBytesUsed_GreaterThan_BufferLength_Then_ArgumentOutOfRangeException_IsThrown(
            SutMessageFromByteBufferReader sut)
        {
            //a
            var sourceBufferDummy = new byte[10];
            ushort numberOfbitsUsedInByteDummy = 4;
            uint totalNuberOfBytesUsedStub = 11;

            //aa
            Action readMethodCall = () => sut.Read(sourceBufferDummy, numberOfbitsUsedInByteDummy, totalNuberOfBytesUsedStub);

            //aaa
            readMethodCall.ShouldThrowExactly<ArgumentOutOfRangeException>();
        }

        [Theory, AutoNSubstituteData]
        public void With_Null_SourceBuffer_Then_ArgumentNullException_IsThrown(
            SutMessageFromByteBufferReader sut)
        {
            //a
            byte[] sourceBufferStub = null;
            ushort numberOfbitsUsedInByteDummy = 4;
            uint totalNuberOfBytesUsedDummy = 10;

            //aa
            Action readMethodCall = () => sut.Read(sourceBufferStub, numberOfbitsUsedInByteDummy, totalNuberOfBytesUsedDummy);

            //aaa
            readMethodCall.ShouldThrowExactly<ArgumentNullException>();
        }

        [Theory, AutoNSubstituteData]
        public void With_Empty_SourceBuffer_Then_ArgumentException_IsThrown(
            SutMessageFromByteBufferReader sut)
        {
            //a
            var sourceBufferStub = new byte[0];
            ushort numberOfbitsUsedInByteDummy = 4;
            uint totalNuberOfBytesUsedDummy = 10;

            //aa
            Action readMethodCall = () => sut.Read(sourceBufferStub, numberOfbitsUsedInByteDummy, totalNuberOfBytesUsedDummy);

            //aaa
            readMethodCall.ShouldThrowExactly<ArgumentException>();
        }

        [Theory, AutoNSubstituteData]
        public void Then_CorrectMessage_IsRetrieved_FromBuffer(
            BitToByteWriter bitToByteWriterStub,
            Domain.Readers.BitFromByteReader bitReaderStub)
        {
            //a
            var messageStub = "Abc";
            var sourceBufferStub = new byte[15];
            ushort numberOfbitsUsedInByteDummy = 4;

            var messageToBufferWriterStub = new MessageToByteBufferWriter(bitToByteWriterStub);
            var bufferWithMessage = messageToBufferWriterStub.WriteMessage(messageStub, sourceBufferStub, numberOfbitsUsedInByteDummy);
            var sut = new SutMessageFromByteBufferReader(bitReaderStub);

            //aa
            var messageRetrieved = ReadFromWriteResult(bufferWithMessage);

            //aaa
            messageRetrieved.Should().Be(messageStub);
        }

        private string ReadFromWriteResult(Domain.Writers.Dtos.MessageWriteResult writeResult)
        {
            string resultMessage = null;
            ushort byteThresholdBitCounter = 0;

            var bitArray = new System.Collections.BitArray(8);
            var messageRetrievedFromResultBuffer = new System.Collections.Generic.List<byte>();

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
    }
}

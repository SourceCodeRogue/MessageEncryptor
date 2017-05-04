using System;
using Xunit;
using FluentAssertions;

using MessageEncryptor.Domain.Tests.Attributes;

using SutBitFromByteReader = MessageEncryptor.Domain.Readers.BitFromByteReader;

namespace MessageEncryptor.Domain.Tests.Readers.BitFromByteReader
{
    public class Given_BitfromBytereader_When_ReadIsCalled
    {
        [Theory, AutoNSubstituteData]
        public void WithValid_BitPosition_Then_NoExceptionIsThrown(
            SutBitFromByteReader sut)
        {
            //a
            var byteDummy = new byte();
            ushort bitPositionStub = 4;

            //aa
            Action readMethodCall = () => sut.Read(byteDummy, bitPositionStub);

            //aaa
            readMethodCall.ShouldNotThrow();
        }

        [Theory, AutoNSubstituteData]
        public void With_BitPosition_OutsideOfByteRange_Then_ArgumentOutOfRangeException_IsThrown(
            SutBitFromByteReader sut)
        {
            //a
            var byteDummy = new byte();
            ushort bitPositionStub = 9;

            //aa
            Action readMethodCall = () => sut.Read(byteDummy, bitPositionStub);

            //aaa
            readMethodCall.ShouldThrowExactly<ArgumentOutOfRangeException>();
        }


        [Theory, AutoNSubstituteData]
        public void With_ValidBitPosition_Then_BitValue_FromThatPosition_IsReturned(
            SutBitFromByteReader sut)
        {
            //a
            var byteDummy = new byte();            
            ushort bitPositionStub = 4;
            byteDummy = (byte)(byteDummy | (1 << bitPositionStub));

            //aa
            var bitvalue = sut.Read(byteDummy, bitPositionStub);

            //aaa
            bitvalue.Should().BeTrue();
        }
    }
}

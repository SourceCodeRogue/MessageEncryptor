using System;
using Xunit;
using FluentAssertions;

using MessageEncryptor.Domain.Tests.Attributes;

using SutMessageWriteResult = MessageEncryptor.Domain.Writers.Dtos.MessageWriteResult;

namespace MessageEncryptor.Domain.Tests.Writers.Dtos.MessageWriteResult
{
    public class Given_MessageWriteResult_When_ConstructorIsCalled
    {
        [Theory, AutoNSubstituteData]
        public void With_NullBuffer_Then_ArgumentNullException_IsThrown()
        {
            //a
            byte[] bufferStub = null;
            uint totalNumberOfBytesUsedDummy = 2;
            ushort numberOfBitsUsedInByteDummy = 4;

            //aa
            Action constructorCall = () => new SutMessageWriteResult(bufferStub, totalNumberOfBytesUsedDummy, numberOfBitsUsedInByteDummy);

            //aaa
            constructorCall.ShouldThrowExactly<ArgumentNullException>();
        }

        [Theory, AutoNSubstituteData]
        public void With_NumberOfBits_OutsideOf_ByteRange_Then_ArgumentOutOfrangeException_IsThrown()
        {
            //a
            var bufferStub = new byte[10];
            ushort numberOfBitsUsedInByteStub = 9;
            uint totalNuberOfBytesUsedDummy = 12;

            //aa
            Action constructorCall = () => new SutMessageWriteResult(bufferStub, totalNuberOfBytesUsedDummy, numberOfBitsUsedInByteStub);

            //aaa
            constructorCall.ShouldThrowExactly<ArgumentOutOfRangeException>();
        }

        [Theory, AutoNSubstituteData]
        public void With_TotalNumberOfBytesUsed_GreaterThan_BufferLength_Then_ArgumentOutOfRangeException_IsThrown()
        {
            //a
            var bufferDummy = new byte[10];
            ushort numberOfBitsUsedInByteDummy = 4;
            uint totalNuberOfBytesUsedStub = 12;

            //aa
            Action constructorCall = () => new SutMessageWriteResult(bufferDummy, totalNuberOfBytesUsedStub, numberOfBitsUsedInByteDummy);

            //aaa
            constructorCall.ShouldThrowExactly<ArgumentOutOfRangeException>();
        }

        [Theory, AutoNSubstituteData]
        public void With_ValidParameters_Then_NoExceptionIsThrown()
        {
            //a
            var bufferDummy = new byte[10];
            ushort numberOfBitsUsedInByteStub = 8;
            uint totalNuberOfBytesUsedDummy = 5;

            //aa
            Action constructorCall = () => new SutMessageWriteResult(bufferDummy, totalNuberOfBytesUsedDummy, numberOfBitsUsedInByteStub);

            //aaa
            constructorCall.ShouldNotThrow();
        }
    }
}

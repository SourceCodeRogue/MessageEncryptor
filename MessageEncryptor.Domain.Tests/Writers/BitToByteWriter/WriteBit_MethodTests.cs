using System;
using FluentAssertions;
using Xunit;

using MessageEncryptor.Domain.Tests.Attributes;

namespace MessageEncryptor.Domain.Tests.Writers.BitToByteWriter
{
    public class Given_BitToByteWriter_When_WriteBit_IsCalled
    {
        [Theory, AutoNSubstituteData]
        public void Then_No_Exception_IsThrown(
            byte targetByteDummy,
            bool bitValueDummy,
            Domain.Writers.BitToByteWriter sut)
        {
            //a
            ushort bitPositionDummy = 1;
            
            //aa
            Action writeBitMethodCall = () => sut.WriteBit(targetByteDummy, bitValueDummy, bitPositionDummy);

            //aaa
            writeBitMethodCall.ShouldNotThrow();
        }

        [Theory, AutoNSubstituteData]
        public void With_BitPosition_Parameter_GreaterThan_7_Then_ArgumentException_IsThrown(
            byte targetByteDummy,
            bool bitValueDummy,
            Domain.Writers.BitToByteWriter sut)
        {
            //a
            ushort bitPositionStub = 8;

            //aa
            Action writeBitMethodCall = () => sut.WriteBit(targetByteDummy, bitValueDummy, bitPositionStub);

            //aaa
            writeBitMethodCall.ShouldThrowExactly<ArgumentException>();
        }

        [Theory, AutoNSubstituteData]
        public void Then_Bit_AtPassedPositon_IsChangedTo_PassedBitValue_InResultByte(
            byte targetByteStub,
            bool bitValueStub,
            Domain.Writers.BitToByteWriter sut)
        {
            //a
            ushort bitPositionStub = 2;

            //aa
            var resultByte = sut.WriteBit(targetByteStub, bitValueStub, bitPositionStub);

            //aaa
            var resultBitValue = ((resultByte & (1 << bitPositionStub)) != 0);
            resultBitValue.Should().Be(bitValueStub);
        }

        [Theory, AutoNSubstituteData]
        public void Then_Bits_AtThe_RemainingPositions_AreNotChanged_InResultByte(
            byte targetByteStub,
            bool bitValueStub,
            Domain.Writers.BitToByteWriter sut)
        {
            //a
            ushort bitPositionStub = 2;

            //aa
            var resultByte = sut.WriteBit(targetByteStub, bitValueStub, bitPositionStub);

            //aaa
            var resultByteWithActivatedBit = (byte)(resultByte | (1 << bitPositionStub));
            var originalByteWithActivatedBit = (byte)(targetByteStub | (1 << bitPositionStub));
            resultByteWithActivatedBit.Should().Be(resultByteWithActivatedBit);
        }
    }
}

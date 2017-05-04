using FluentAssertions;
using MessageEncryptor.Domain.Tests.Attributes;
using System.IO;
using Xunit;

using SutBitmapFileStructureProvider = MessageEncryptor.Domain.Providers.BitmapFIleStructureProvider;

namespace MessageEncryptor.Domain.Tests.Providers.BitmapFileStructureProvider
{
    public class Given_BitmapFIleStructureProvider_When_GetStructureInfo_IsCalled
    {
        [Theory, AutoNSubstituteWithTestBitmap]
        public void With_StreamTo_EncryptorTestFile_Then_CorrectFileStructure_IsReturned(
            Stream streamToTestFileStub,
            SutBitmapFileStructureProvider sut
            )
        {
            //Given            
            var expectedBytesPerPixel = 3;
            var expectedImageBytesCount = 5325600;

            //When
            var fileStructureInfoReturned = sut.GetStructureInfo(streamToTestFileStub);

            //Then
            fileStructureInfoReturned.ImageBytesCount.Should().Be(expectedImageBytesCount);
            fileStructureInfoReturned.BytesPerPixel.Should().Be(expectedBytesPerPixel);
        }

        [Theory, AutoNSubstituteWithTestBitmap]
        public void With_StreamTo_EncryptorTestFile_Then_StreamPosition_IsNotAltered(
            Stream streamToTestFileStub,
            SutBitmapFileStructureProvider sut
            )
        {
            //Given            
            var expectedOriginalStreamPosition = streamToTestFileStub.Position;

            //When
            var fileStructureInfoReturned = sut.GetStructureInfo(streamToTestFileStub);

            //Then
            streamToTestFileStub.Position.Should().Be(expectedOriginalStreamPosition);
        }
    }
}

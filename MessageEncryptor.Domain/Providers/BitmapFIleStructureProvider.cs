using MessageEncryptor.Domain.Providers.Abstractions;
using MessageEncryptor.Domain.Providers.Dtos;
using System.IO;

namespace MessageEncryptor.Domain.Providers
{
    public class BitmapFIleStructureProvider : IProvideFileStructureInfo
    {
        private int GetLittleEndianIntegerFromByteArray(byte[] data)
        {
            return GetLittleEndianIntegerFromByteArray(data, 0);
        }

        private int GetLittleEndianIntegerFromByteArray(byte[] data, int startIndex)
        {            
            int result = data[startIndex];

            var indexIncrement = 1;
            var currentIndex = startIndex + indexIncrement;

            while (currentIndex < data.Length)
            {
                result = result | (data[currentIndex] << (indexIncrement * 8));
                indexIncrement++;
                currentIndex = startIndex + indexIncrement;
            }

            return result;
        }        

        public FileStructureInfo GetStructureInfo(Stream fileStream)
        {
            var originalStreamPosition = fileStream.Position;

            var initialPixelsIndexBuffer = new byte[4];
            var bitsPerPixel = new byte[2];
            var imageSize = new byte[4];

            fileStream.Position = 10;
            fileStream.Read(initialPixelsIndexBuffer, 0, 4);                        

            fileStream.Position = 28;
            fileStream.Read(bitsPerPixel, 0, 2);

            fileStream.Position = 34;
            fileStream.Read(imageSize, 0, 4);

            var initialPixlesIndex = GetLittleEndianIntegerFromByteArray(initialPixelsIndexBuffer);
            var imageBytesCount = GetLittleEndianIntegerFromByteArray(imageSize);
            var numberOfBitsPerPixel = GetLittleEndianIntegerFromByteArray(bitsPerPixel);

            fileStream.Position = originalStreamPosition;

            return new FileStructureInfo(initialPixlesIndex, imageBytesCount, numberOfBitsPerPixel / 8);
        }
    }
}

using System.IO;

using MessageEncryptor.Domain.Providers.Abstractions;

namespace MessageEncryptor.Domain.Providers
{
    public class BitmapPixelsProvider : IProvideImageDataFromFile
    {
        private readonly IProvideFileStructureInfo _fileStructureProvider;

        private int GetLittleEndianIntegerFromByteArray(byte[] data, int startIndex)
        {
            return (data[startIndex + 3] << 24)
                 | (data[startIndex + 2] << 16)
                 | (data[startIndex + 1] << 8)
                 | data[startIndex];
        }

        public byte[] GetPixelsArray(Stream fileStream)
        {
            

            return null;
        }
    }
}

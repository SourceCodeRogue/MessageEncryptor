using System.IO;

namespace MessageEncryptor.Domain.Providers.Abstractions
{
    public interface IProvideImageDataFromFile
    {
        byte[] GetPixelsArray(Stream fileStream);
    }
}

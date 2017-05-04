using MessageEncryptor.Domain.Providers.Dtos;
using System.IO;

namespace MessageEncryptor.Domain.Providers.Abstractions
{
    public interface IProvideFileStructureInfo
    {
        FileStructureInfo GetStructureInfo(Stream fileStream);
    }
}

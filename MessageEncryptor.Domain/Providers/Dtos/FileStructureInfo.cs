

namespace MessageEncryptor.Domain.Providers.Dtos
{
    public class FileStructureInfo
    {
        public int PixelsStartIndex { get; }

        public int ImageBytesCount { get; }

        public int BytesPerPixel { get; }

        public FileStructureInfo(int pixelstartIndex, int imageBytesCount, int bytesPerPixel)
        {
            PixelsStartIndex = pixelstartIndex;
            ImageBytesCount = imageBytesCount;
            BytesPerPixel = bytesPerPixel;
        }
    }
}

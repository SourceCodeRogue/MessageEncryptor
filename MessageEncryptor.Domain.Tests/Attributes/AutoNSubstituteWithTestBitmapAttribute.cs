using Ploeh.AutoFixture;
using System.IO;

namespace MessageEncryptor.Domain.Tests.Attributes
{
    internal class AutoNSubstituteWithTestBitmapAttribute : AutoNSubstituteDataAttribute
    {
        public AutoNSubstituteWithTestBitmapAttribute()
        {
            Fixture.Register<Stream>(() => File.OpenRead("TestData\\EncryptorTestFile.bmp"));
        }
    }
}

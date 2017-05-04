using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace MessageEncryptor.Domain.Tests.Attributes
{
    [TraitDiscoverer("MessageEncryptor.Domain.Tests.Attributes.BDDDiscoverer", "MessageEncryptor.Domain.Tests")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class BDDTestNameAttribute : Attribute, ITraitAttribute
    {
        public BDDTestNameAttribute(string category) : base() { }
    }

    public class BDDDiscoverer : ITraitDiscoverer
    {
        public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
        {
            var ctorArgs = traitAttribute.GetConstructorArguments().ToList();
            yield return new KeyValuePair<string, string>("Category", ctorArgs[0].ToString());
        }
    }
}

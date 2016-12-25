 
using System.Collections.Generic;

namespace SF.Core.Abstraction.Steup
{
    public interface IVersionProviderFactory
    {
        IEnumerable<IVersionProvider> VersionProviders { get; }
        IVersionProvider Get(string name);

    }
}

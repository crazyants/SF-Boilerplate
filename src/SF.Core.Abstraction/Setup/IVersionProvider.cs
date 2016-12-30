 
using System;

namespace SF.Core.Abstraction.Steup
{
    public interface IVersionProvider
    {
        string Name { get; }
        Guid ApplicationId { get; }
        Version CurrentVersion { get; }
    }
}

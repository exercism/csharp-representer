using System.Collections.Generic;
using System.Linq;

namespace Exercism.Representers.CSharp
{
    internal record Mapping(Dictionary<string, string> PlaceholdersToIdentifier);
}
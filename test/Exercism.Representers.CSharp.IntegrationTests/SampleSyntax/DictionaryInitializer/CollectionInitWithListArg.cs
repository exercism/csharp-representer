using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Exercism.Representers.CSharp.IntegrationTests.SampleSyntax.DictionaryInitializer
{
    public class CollectionInitWithListArg
    {
        Dictionary<List<int>, List<string>> dict = new Dictionary<List<int>, List<string>>
            {{new List<int> {1}, new List<string> {"one"}}};
    }
}
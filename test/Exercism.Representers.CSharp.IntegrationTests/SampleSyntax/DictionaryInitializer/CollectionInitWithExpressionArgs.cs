using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Exercism.Representers.CSharp.IntegrationTests.SampleSyntax.DictionaryInitializer
{
    public class CollectionInitWithExpressionArgs
    {
        Dictionary<DateTime, Random> dict = new Dictionary<DateTime, Random> {{DateTime.Now, new Random()}};

        SyntaxNode node = CompilationUnit()
            .WithMembers(
                SingletonList<MemberDeclarationSyntax>(
                    FieldDeclaration(
                        VariableDeclaration(
                                GenericName(
                                        Identifier("Dictionary"))
                                    .WithTypeArgumentList(
                                        TypeArgumentList(
                                            SeparatedList<TypeSyntax>(
                                                new SyntaxNodeOrToken[]
                                                {
                                                    IdentifierName("DateTime"),
                                                    Token(SyntaxKind.CommaToken),
                                                    IdentifierName("Random")
                                                }))))
                            .WithVariables(
                                SingletonSeparatedList<VariableDeclaratorSyntax>(
                                    VariableDeclarator(
                                            Identifier("dict"))
                                        .WithInitializer(
                                            EqualsValueClause(
                                                ObjectCreationExpression(
                                                        GenericName(
                                                                Identifier("Dictionary"))
                                                            .WithTypeArgumentList(
                                                                TypeArgumentList(
                                                                    SeparatedList<TypeSyntax>(
                                                                        new SyntaxNodeOrToken[]
                                                                        {
                                                                            IdentifierName("DateTime"),
                                                                            Token(SyntaxKind.CommaToken),
                                                                            IdentifierName("Random")
                                                                        }))))
                                                    .WithInitializer(
                                                        InitializerExpression(
                                                            SyntaxKind.ObjectInitializerExpression,
                                                            SingletonSeparatedList<ExpressionSyntax>(
                                                                AssignmentExpression(
                                                                    SyntaxKind.SimpleAssignmentExpression,
                                                                    ImplicitElementAccess()
                                                                        .WithArgumentList(
                                                                            BracketedArgumentList(
                                                                                SingletonSeparatedList<ArgumentSyntax>(
                                                                                    Argument(
                                                                                        MemberAccessExpression(
                                                                                            SyntaxKind
                                                                                                .SimpleMemberAccessExpression,
                                                                                            IdentifierName("DateTime"),
                                                                                            IdentifierName("Now")))))),
                                                                    ObjectCreationExpression(
                                                                            IdentifierName("Random"))
                                                                        .WithArgumentList(
                                                                            ArgumentList()))))))))))))
            .NormalizeWhitespace();
    }
}

/*
    IInvocationOperation (InitializerExpressionSyntax)
        IInstanceReferenceOperation (GenericNameSyntax)
        IArgumentOperation (MemberAccessExpressionSyntax)
            IPropertyReferenceOperation (MemberAccessExpressionSyntax)
        IArgumentOperation (ObjectCreationExpressionSyntax)
            IObjectCreationOperation (ObjectCreationExpressionSyntax)

    type name is available from IInstanceReferenceOperation.Type but only because it refers implicitly to the object
    being initialized
    
    initializerOperation.Parent.Type.Name == "Dictionary"
*/



/*
[0] = {Microsoft.CodeAnalysis.Operations.CSharpLazyInvocationOperation}
 Arguments = {ImmutableArray<IArgumentOperation>} Length = 2
 Children = {Microsoft.CodeAnalysis.Operations.BaseInvocationOperation.<get_Children>d__8}
 ConstantValue = {Optional<object>} "unspecified"
 Instance = {Microsoft.CodeAnalysis.Operations.InstanceReferenceOperation}
 IsImplicit = {bool} true
 IsVirtual = {bool} false
 Kind = {OperationKind} Invocation
 Language = {string} "C#"
 Parent = {Microsoft.CodeAnalysis.Operations.CSharpLazyObjectOrCollectionInitializerOperation}
 Syntax = {InitializerExpressionSyntax} "{DateTime.Now, new Random()}"
 TargetMethod = {SubstitutedMethodSymbol} "System.Collections.Generic.Dictionary<System.DateTime, System.Random>.Add(System.DateTime, System.Random)"
 Type = {PENamedTypeSymbolNonGeneric} "void"
 Static members = {} 
 Non-public members = {} 
[1] = {Microsoft.CodeAnalysis.Operations.InstanceReferenceOperation}
 Children = {IOperation[]} Count = 0
 ConstantValue = {Optional<object>} "unspecified"
 IsImplicit = {bool} true
 Kind = {OperationKind} InstanceReference
 Language = {string} "C#"
 Parent = {Microsoft.CodeAnalysis.Operations.CSharpLazyInvocationOperation}
 ReferenceKind = {InstanceReferenceKind} ImplicitReceiver
 Syntax = {GenericNameSyntax} "Dictionary<DateTime, Random>"
 Type = {ConstructedNamedTypeSymbol} "System.Collections.Generic.Dictionary<System.DateTime, System.Random>"
 Static members = {} 
 Non-public members = {} 
[2] = {Microsoft.CodeAnalysis.Operations.ArgumentOperation}
 ArgumentKind = {ArgumentKind} Explicit
 Children = {Microsoft.CodeAnalysis.Operations.BaseArgumentOperation.<get_Children>d__19}
 ConstantValue = {Optional<object>} "unspecified"
 InConversion = {Microsoft.CodeAnalysis.Operations.CommonConversion}
 IsImplicit = {bool} true
 Kind = {OperationKind} Argument
 Language = {string} "C#"
 OutConversion = {Microsoft.CodeAnalysis.Operations.CommonConversion}
 Parameter = {SubstitutedParameterSymbol} "System.DateTime"
 Parent = {Microsoft.CodeAnalysis.Operations.CSharpLazyInvocationOperation}
 Syntax = {MemberAccessExpressionSyntax} "DateTime.Now"
 Type = {ITypeSymbol} null
 Value = {Microsoft.CodeAnalysis.Operations.CSharpLazyPropertyReferenceOperation}
 Static members = {} 
 Non-public members = {} 
[3] = {Microsoft.CodeAnalysis.Operations.CSharpLazyPropertyReferenceOperation}
 Arguments = {ImmutableArray<IArgumentOperation>} Length = 0
 Children = {Microsoft.CodeAnalysis.Operations.BasePropertyReferenceOperation.<get_Children>d__4}
 ConstantValue = {Optional<object>} "unspecified"
 Instance = {IOperation} null
 IsImplicit = {bool} false
 Kind = {OperationKind} PropertyReference
 Language = {string} "C#"
 Member = {PEPropertySymbol} "System.DateTime.Now"
 Parent = {Microsoft.CodeAnalysis.Operations.ArgumentOperation}
 Property = {PEPropertySymbol} "System.DateTime.Now"
 Syntax = {MemberAccessExpressionSyntax} "DateTime.Now"
 Type = {PENamedTypeSymbolNonGeneric} "System.DateTime"
 Static members = {} 
 Non-public members = {} 
[4] = {Microsoft.CodeAnalysis.Operations.ArgumentOperation}
 ArgumentKind = {ArgumentKind} Explicit
 Children = {Microsoft.CodeAnalysis.Operations.BaseArgumentOperation.<get_Children>d__19}
 ConstantValue = {Optional<object>} "unspecified"
 InConversion = {Microsoft.CodeAnalysis.Operations.CommonConversion}
 IsImplicit = {bool} true
 Kind = {OperationKind} Argument
 Language = {string} "C#"
 OutConversion = {Microsoft.CodeAnalysis.Operations.CommonConversion}
 Parameter = {SubstitutedParameterSymbol} "System.Random"
 Parent = {Microsoft.CodeAnalysis.Operations.CSharpLazyInvocationOperation}
 Syntax = {ObjectCreationExpressionSyntax} "new Random()"
 Type = {ITypeSymbol} null
 Value = {Microsoft.CodeAnalysis.Operations.CSharpLazyObjectCreationOperation}
 Static members = {} 
 Non-public members = {} 
[5] = {Microsoft.CodeAnalysis.Operations.CSharpLazyObjectCreationOperation}
 Arguments = {ImmutableArray<IArgumentOperation>} Length = 0
 Children = {Microsoft.CodeAnalysis.Operations.BaseObjectCreationOperation.<get_Children>d__5}
 ConstantValue = {Optional<object>} "unspecified"
 Constructor = {PEMethodSymbol} "System.Random.Random()"
 Initializer = {IObjectOrCollectionInitializerOperation} null
 IsImplicit = {bool} false
 Kind = {OperationKind} ObjectCreation
 Language = {string} "C#"
 Parent = {Microsoft.CodeAnalysis.Operations.ArgumentOperation}
 Syntax = {ObjectCreationExpressionSyntax} "new Random()"
 Type = {PENamedTypeSymbolNonGeneric} "System.Random"
 Static members = {} 
 Non-public members = {} 
*/
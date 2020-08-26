using System;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Exercism.Representers.CSharp.IntegrationTests.SampleSyntax.DictionaryInitializer
{
    public class ObjectInitWithExpressionArgs
    {
        Dictionary<DateTime, Random> dict = new Dictionary<DateTime, Random> {[DateTime.Now] = new Random()};

        private SyntaxNode node = CompilationUnit()
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
                                                                                            MemberAccessExpression(
                                                                                                SyntaxKind
                                                                                                    .SimpleMemberAccessExpression,
                                                                                                IdentifierName(
                                                                                                    "System"),
                                                                                                IdentifierName(
                                                                                                    "DateTime")),
                                                                                            IdentifierName("Now")))))),
                                                                    ObjectCreationExpression(
                                                                            IdentifierName("Random"))
                                                                        .WithArgumentList(
                                                                            ArgumentList()))))))))))))
            .NormalizeWhitespace();
    }
}

/*
    ISimpleAssignmentOperation (AssignmentExpressionSyntax)
        IPropertyReferenceOperation (ImplicitElementAccessSyntax)
            IInstanceReferenceOperation (ImplicitElementAccessSyntax)
            IArgumentOperation (ArgumentSyntax) - "DateTime.Now()"
                IPropertyReferenceOperation (MemberAccessExpressionSyntax) - "DateTime.Now()"
        IObjectCreationOperation (ObjectCreationExpressionSyntax) - "new Random()"
        
*/

/*
[0] = {Microsoft.CodeAnalysis.Operations.CSharpLazySimpleAssignmentOperation}
 Children = {Microsoft.CodeAnalysis.Operations.AssignmentOperation.<get_Children>d__2}
 ConstantValue = {Optional<object>} "unspecified"
 IsImplicit = {bool} false
 IsRef = {bool} false
 Kind = {OperationKind} SimpleAssignment
 Language = {string} "C#"
 Parent = {Microsoft.CodeAnalysis.Operations.CSharpLazyObjectOrCollectionInitializerOperation}
 Syntax = {AssignmentExpressionSyntax} "[DateTime.Now] = new Random()"
 Target = {Microsoft.CodeAnalysis.Operations.CSharpLazyPropertyReferenceOperation}
 Type = {PENamedTypeSymbolNonGeneric} "System.Random"
 Value = {Microsoft.CodeAnalysis.Operations.CSharpLazyObjectCreationOperation}
 Static members = {} 
 Non-public members = {} 
[1] = {Microsoft.CodeAnalysis.Operations.CSharpLazyPropertyReferenceOperation}
 Arguments = {ImmutableArray<IArgumentOperation>} Length = 1
 Children = {Microsoft.CodeAnalysis.Operations.BasePropertyReferenceOperation.<get_Children>d__4}
 ConstantValue = {Optional<object>} "unspecified"
 Instance = {Microsoft.CodeAnalysis.Operations.InstanceReferenceOperation}
 IsImplicit = {bool} false
 Kind = {OperationKind} PropertyReference
 Language = {string} "C#"
 Member = {SubstitutedPropertySymbol} "System.Collections.Generic.Dictionary<System.DateTime, System.Random>.this[System.DateTime]"
 Parent = {Microsoft.CodeAnalysis.Operations.CSharpLazySimpleAssignmentOperation}
 Property = {SubstitutedPropertySymbol} "System.Collections.Generic.Dictionary<System.DateTime, System.Random>.this[System.DateTime]"
 Syntax = {ImplicitElementAccessSyntax} "[DateTime.Now]"
 Type = {PENamedTypeSymbolNonGeneric} "System.Random"
 Static members = {} 
 Non-public members = {} 
[2] = {Microsoft.CodeAnalysis.Operations.InstanceReferenceOperation}
 Children = {IOperation[]} Count = 0
 ConstantValue = {Optional<object>} "unspecified"
 IsImplicit = {bool} true
 Kind = {OperationKind} InstanceReference
 Language = {string} "C#"
 Parent = {Microsoft.CodeAnalysis.Operations.CSharpLazyPropertyReferenceOperation}
 ReferenceKind = {InstanceReferenceKind} ImplicitReceiver
 Syntax = {ImplicitElementAccessSyntax} "[DateTime.Now]"
 Type = {ConstructedNamedTypeSymbol} "System.Collections.Generic.Dictionary<System.DateTime, System.Random>"
 Static members = {} 
 Non-public members = {} 
[3] = {Microsoft.CodeAnalysis.Operations.CSharpLazyArgumentOperation}
 ArgumentKind = {ArgumentKind} Explicit
 Children = {Microsoft.CodeAnalysis.Operations.BaseArgumentOperation.<get_Children>d__19}
 ConstantValue = {Optional<object>} "unspecified"
 InConversion = {Microsoft.CodeAnalysis.Operations.CommonConversion}
 IsImplicit = {bool} false
 Kind = {OperationKind} Argument
 Language = {string} "C#"
 OutConversion = {Microsoft.CodeAnalysis.Operations.CommonConversion}
 Parameter = {SubstitutedParameterSymbol} "System.DateTime"
 Parent = {Microsoft.CodeAnalysis.Operations.CSharpLazyPropertyReferenceOperation}
 Syntax = {ArgumentSyntax} "DateTime.Now"
 Type = {ITypeSymbol} null
 Value = {Microsoft.CodeAnalysis.Operations.CSharpLazyPropertyReferenceOperation}
 Static members = {} 
 Non-public members = {} 
[4] = {Microsoft.CodeAnalysis.Operations.CSharpLazyPropertyReferenceOperation}
 Arguments = {ImmutableArray<IArgumentOperation>} Length = 0
 Children = {Microsoft.CodeAnalysis.Operations.BasePropertyReferenceOperation.<get_Children>d__4}
 ConstantValue = {Optional<object>} "unspecified"
 Instance = {IOperation} null
 IsImplicit = {bool} false
 Kind = {OperationKind} PropertyReference
 Language = {string} "C#"
 Member = {PEPropertySymbol} "System.DateTime.Now"
 Parent = {Microsoft.CodeAnalysis.Operations.CSharpLazyArgumentOperation}
 Property = {PEPropertySymbol} "System.DateTime.Now"
 Syntax = {MemberAccessExpressionSyntax} "DateTime.Now"
 Type = {PENamedTypeSymbolNonGeneric} "System.DateTime"
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
 Parent = {Microsoft.CodeAnalysis.Operations.CSharpLazySimpleAssignmentOperation}
 Syntax = {ObjectCreationExpressionSyntax} "new Random()"
 Type = {PENamedTypeSymbolNonGeneric} "System.Random"
 Static members = {} 
 Non-public members = {} 
*/


/**
[0] = {Microsoft.CodeAnalysis.Operations.CSharpLazySimpleAssignmentOperation}
 Children = {Microsoft.CodeAnalysis.Operations.AssignmentOperation.<get_Children>d__2}
 ConstantValue = {Optional<object>} "unspecified"
 IsImplicit = {bool} false
 IsRef = {bool} false
 Kind = {OperationKind} SimpleAssignment
 Language = {string} "C#"
 Parent = {Microsoft.CodeAnalysis.Operations.CSharpLazyObjectOrCollectionInitializerOperation}
 Syntax = {AssignmentExpressionSyntax} "[5 * 6] = $"{DateTime.Now}""
 Target = {Microsoft.CodeAnalysis.Operations.CSharpLazyPropertyReferenceOperation}
 Type = {PENamedTypeSymbolNonGeneric} "string"
 Value = {Microsoft.CodeAnalysis.Operations.CSharpLazyInterpolatedStringOperation}
 Static members = {} 
 Non-public members = {} 
[1] = {Microsoft.CodeAnalysis.Operations.CSharpLazyPropertyReferenceOperation}
 Arguments = {ImmutableArray<IArgumentOperation>} Length = 1
 Children = {Microsoft.CodeAnalysis.Operations.BasePropertyReferenceOperation.<get_Children>d__4}
 ConstantValue = {Optional<object>} "unspecified"
 Instance = {Microsoft.CodeAnalysis.Operations.InstanceReferenceOperation}
 IsImplicit = {bool} false
 Kind = {OperationKind} PropertyReference
 Language = {string} "C#"
 Member = {SubstitutedPropertySymbol} "System.Collections.Generic.Dictionary<int, string>.this[int]"
 Parent = {Microsoft.CodeAnalysis.Operations.CSharpLazySimpleAssignmentOperation}
 Property = {SubstitutedPropertySymbol} "System.Collections.Generic.Dictionary<int, string>.this[int]"
 Syntax = {ImplicitElementAccessSyntax} "[5 * 6]"
 Type = {PENamedTypeSymbolNonGeneric} "string"
 Static members = {} 
 Non-public members = {} 
[2] = {Microsoft.CodeAnalysis.Operations.InstanceReferenceOperation}
 Children = {IOperation[]} Count = 0
 ConstantValue = {Optional<object>} "unspecified"
 IsImplicit = {bool} true
 Kind = {OperationKind} InstanceReference
 Language = {string} "C#"
 Parent = {Microsoft.CodeAnalysis.Operations.CSharpLazyPropertyReferenceOperation}
 ReferenceKind = {InstanceReferenceKind} ImplicitReceiver
 Syntax = {ImplicitElementAccessSyntax} "[5 * 6]"
 Type = {ConstructedNamedTypeSymbol} "System.Collections.Generic.Dictionary<int, string>"
 Static members = {} 
 Non-public members = {} 
[3] = {Microsoft.CodeAnalysis.Operations.CSharpLazyArgumentOperation}
 ArgumentKind = {ArgumentKind} Explicit
 Children = {Microsoft.CodeAnalysis.Operations.BaseArgumentOperation.<get_Children>d__19}
 ConstantValue = {Optional<object>} "unspecified"
 InConversion = {Microsoft.CodeAnalysis.Operations.CommonConversion}
 IsImplicit = {bool} false
 Kind = {OperationKind} Argument
 Language = {string} "C#"
 OutConversion = {Microsoft.CodeAnalysis.Operations.CommonConversion}
 Parameter = {SubstitutedParameterSymbol} "int"
 Parent = {Microsoft.CodeAnalysis.Operations.CSharpLazyPropertyReferenceOperation}
 Syntax = {ArgumentSyntax} "5 * 6"
 Type = {ITypeSymbol} null
 Value = {Microsoft.CodeAnalysis.Operations.CSharpLazyBinaryOperation}
 Static members = {} 
 Non-public members = {} 
[4] = {Microsoft.CodeAnalysis.Operations.CSharpLazyBinaryOperation}
 Children = {Microsoft.CodeAnalysis.Operations.BaseBinaryOperation.<get_Children>d__20}
 ConstantValue = {Optional<object>} "30"
 IsChecked = {bool} false
 IsCompareText = {bool} false
 IsImplicit = {bool} false
 IsLifted = {bool} false
 Kind = {OperationKind} Binary
 Language = {string} "C#"
 LeftOperand = {Microsoft.CodeAnalysis.Operations.LiteralOperation}
 OperatorKind = {BinaryOperatorKind} Multiply
 OperatorMethod = {IMethodSymbol} null
 Parent = {Microsoft.CodeAnalysis.Operations.CSharpLazyArgumentOperation}
 RightOperand = {Microsoft.CodeAnalysis.Operations.LiteralOperation}
 Syntax = {BinaryExpressionSyntax} "5 * 6"
 Type = {PENamedTypeSymbolNonGeneric} "int"
 UnaryOperatorMethod = {IMethodSymbol} null
 Static members = {} 
 Non-public members = {} 
[5] = {Microsoft.CodeAnalysis.Operations.LiteralOperation}
 Children = {IOperation[]} Count = 0
 ConstantValue = {Optional<object>} "5"
 IsImplicit = {bool} false
 Kind = {OperationKind} Literal
 Language = {string} "C#"
 Parent = {Microsoft.CodeAnalysis.Operations.CSharpLazyBinaryOperation}
 Syntax = {LiteralExpressionSyntax} "5"
 Type = {PENamedTypeSymbolNonGeneric} "int"
 Static members = {} 
 Non-public members = {} 
[6] = {Microsoft.CodeAnalysis.Operations.LiteralOperation}
 Children = {IOperation[]} Count = 0
 ConstantValue = {Optional<object>} "6"
 IsImplicit = {bool} false
 Kind = {OperationKind} Literal
 Language = {string} "C#"
 Parent = {Microsoft.CodeAnalysis.Operations.CSharpLazyBinaryOperation}
 Syntax = {LiteralExpressionSyntax} "6"
 Type = {PENamedTypeSymbolNonGeneric} "int"
 Static members = {} 
 Non-public members = {} 
[7] = {Microsoft.CodeAnalysis.Operations.CSharpLazyInterpolatedStringOperation}
 Children = {Microsoft.CodeAnalysis.Operations.BaseInterpolatedStringOperation.<get_Children>d__2}
 ConstantValue = {Optional<object>} "unspecified"
 IsImplicit = {bool} false
 Kind = {OperationKind} InterpolatedString
 Language = {string} "C#"
 Parent = {Microsoft.CodeAnalysis.Operations.CSharpLazySimpleAssignmentOperation}
 Parts = {ImmutableArray<IInterpolatedStringContentOperation>} Length = 1
 Syntax = {InterpolatedStringExpressionSyntax} "$"{DateTime.Now}""
 Type = {PENamedTypeSymbolNonGeneric} "string"
 Static members = {} 
 Non-public members = {} 
[8] = {Microsoft.CodeAnalysis.Operations.CSharpLazyInterpolationOperation}
 Alignment = {IOperation} null
 Children = {Microsoft.CodeAnalysis.Operations.BaseInterpolationOperation.<get_Children>d__2}
 ConstantValue = {Optional<object>} "unspecified"
 Expression = {Microsoft.CodeAnalysis.Operations.CSharpLazyPropertyReferenceOperation}
 FormatString = {IOperation} null
 IsImplicit = {bool} false
 Kind = {OperationKind} Interpolation
 Language = {string} "C#"
 Parent = {Microsoft.CodeAnalysis.Operations.CSharpLazyInterpolatedStringOperation}
 Syntax = {InterpolationSyntax} "{DateTime.Now}"
 Type = {ITypeSymbol} null
 Static members = {} 
 Non-public members = {} 
[9] = {Microsoft.CodeAnalysis.Operations.CSharpLazyPropertyReferenceOperation}
 Arguments = {ImmutableArray<IArgumentOperation>} Length = 0
 Children = {Microsoft.CodeAnalysis.Operations.BasePropertyReferenceOperation.<get_Children>d__4}
 ConstantValue = {Optional<object>} "unspecified"
 Instance = {IOperation} null
 IsImplicit = {bool} false
 Kind = {OperationKind} PropertyReference
 Language = {string} "C#"
 Member = {PEPropertySymbol} "System.DateTime.Now"
 Parent = {Microsoft.CodeAnalysis.Operations.CSharpLazyInterpolationOperation}
 Property = {PEPropertySymbol} "System.DateTime.Now"
 Syntax = {MemberAccessExpressionSyntax} "DateTime.Now"
 Type = {PENamedTypeSymbolNonGeneric} "System.DateTime"
 Static members = {} 
 Non-public members = {} 

**/
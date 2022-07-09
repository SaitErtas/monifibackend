﻿using MonifiBackend.WalletModule.Application.AccountMovements.Queries.GetAccountMovements;
using MonifiBackend.WalletModule.Domain.AccountMovements;
using MonifiBackend.WalletModule.Infrastructure.AccountMovements;
using NetArchTest.Rules;
using NUnit.Framework;
using System.Reflection;

namespace MonifiBackend.WalletModule.ArchTests.Base;

public abstract class TestBase
{
    protected static Assembly ApplicationAssembly => typeof(GetAccountMovementsQuery).Assembly;

    protected static Assembly DomainAssembly => typeof(AccountMovement).Assembly;

    protected static Assembly InfrastructureAssembly => typeof(AccountMovementCommandDataAdapter).Assembly;

    protected static void AssertAreImmutable(IEnumerable<Type> types)
    {
        IList<Type> failingTypes = new List<Type>();
        foreach (var type in types)
        {
            if (type.GetFields().Any(x => !x.IsInitOnly) || type.GetProperties().Any(x => x.CanWrite))
            {
                failingTypes.Add(type);
                break;
            }
        }

        AssertFailingTypes(failingTypes);
    }

    protected static void AssertFailingTypes(IEnumerable<Type> types)
    {
        Assert.That(types, Is.Null.Or.Empty);
    }

    protected static void AssertArchTestResult(TestResult result)
    {
        AssertFailingTypes(result.FailingTypes);
    }
}

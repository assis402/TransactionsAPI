using Xunit.Abstractions;
using Xunit.Sdk;

namespace Transactions.IntegrationTests.Helpers;

internal class AlphabeticalTestOrderer : ITestCaseOrderer
{
    /// <summary>
    /// Orders test cases for execution.
    /// </summary>
    /// <param name="testCases">The test cases to be ordered.</param>
    /// <returns>The test cases in the order to be run.</returns>
    public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases)
        where TTestCase : ITestCase
    {
        return testCases.OrderBy(testCase => testCase.TestMethod.Method.Name);
    }
}
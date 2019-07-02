using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Builders;

namespace NUnit.Framework {

    /// <summary>
    /// Marks a method as a parameterized test suite with stdin
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class TestCaseStdAttribute : TestCaseAttribute
    {
        /// <summary>
        /// Construct a TestCaseAttribute with a list of arguments.
        /// This constructor is not CLS-Compliant
        /// </summary>
        /// <param name="arguments"></param>
        public TestCaseStdAttribute(params object[] arguments) : base(arguments) { }

        /// <summary>
        /// Construct a TestCaseAttribute with a single argument
        /// </summary>
        /// <param name="arg"></param>
        public TestCaseStdAttribute(object arg) : base(arg) { }

        /// <summary>
        /// Construct a TestCaseAttribute with a two arguments
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        public TestCaseStdAttribute(object arg1, object arg2) : base(arg1, arg2) { }

        /// <summary>
        /// Construct a TestCaseAttribute with a three arguments
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <param name="arg3"></param>
        public TestCaseStdAttribute(object arg1, object arg2, object arg3) : base(arg1, arg2, arg3) { }

        /// <summary>
        /// Builds the parameters
        /// </summary>
        protected override TestCaseParameters GetParametersForTestCase(IMethodInfo method)
        {
            TestCaseParameters parms;

            try
            {
                var parameterlist = method.GetParameters().ToList();
                parameterlist.RemoveAt(0);
                IParameterInfo[] parameters = parameterlist.ToArray();
                parms = GetParametersForTestCaseHelper(parameters, method);

            }
            catch (Exception ex)
            {
                parms = new TestCaseParameters(ex);
            }

            return parms;
        }

        #region ITestBuilder Members

        /// <summary>
        /// Builds a single test from the specified method and context.
        /// </summary>
        /// <param name="method">The MethodInfo for which tests are to be constructed.</param>
        /// <param name="suite">The suite to which the tests will be added.</param>
        public override IEnumerable<TestMethod> BuildFrom(IMethodInfo method, Test suite)
        {
            TestMethod test = BuildFromHelper(method, suite);
            test.StdIn = Arguments[0].ToString();
            yield return test;
        }

        #endregion
    }
}

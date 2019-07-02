 // ***********************************************************************
// Copyright (c) 2010-2017 Charlie Poole, Rob Prouse
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

using System.Diagnostics;
using NUnit.Framework.Interfaces;

namespace NUnit.Framework.Internal.Commands
{
    /// <summary>
    /// <see cref="MaxTimeCommand" /> adjusts the result of a successful test
    /// to a failure if the elapsed time has exceeded the specified maximum
    /// time allowed.
    /// </summary>
    public class MaxTimeCommand : AfterTestCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MaxTimeCommand"/> class.
        /// </summary>
        /// <param name="innerCommand">The inner command.</param>
        /// <param name="maxTime">The max time allowed in milliseconds</param>
        public MaxTimeCommand(TestCommand innerCommand, int maxTime)
            : base(innerCommand)
        {
            AfterTest = (context) =>
            {
                // TODO: This command duplicates the calculation of the
                // duration of the test because that calculation is 
                // normally performed at a higher level. Most likely,
                // we should move the maxtime calculation to the
                // higher level eventually.

                long tickCount = Stopwatch.GetTimestamp() - context.StartTicks;
                double seconds = (double)tickCount / Stopwatch.Frequency;
                TestResult result = context.CurrentResult;

                result.Duration = seconds;

                if (result.ResultState == ResultState.Success)
                {
                    double elapsedTime = result.Duration * 1000d;

                    if (elapsedTime > maxTime)
                        result.SetResult(ResultState.Failure,
                            string.Format("Elapsed time of {0}ms exceeds maximum of {1}ms",
                                elapsedTime, maxTime));
                }
            };
        }
    }
}

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondHand.Tests
{
    [TestFixture]
    public class SampleTests
    {
        [Test]
        public void LeRandomTestAppears()
        {
            Assert.AreEqual(1, 1);
        }
    }
}


//OpenCover.Console.exe -target:"C:\Program Files (x86)\NUnit 2.6\bin\nunit-console.exe"
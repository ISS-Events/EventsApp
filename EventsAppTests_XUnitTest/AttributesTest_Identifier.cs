using EventsApp.Logic.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsAppTests_XUnitTest
{
    internal class AttributesTest_Identifier
    {
        public void Identifier_Constructor_Test(Dictionary<string, object> inputValues)
        {
            var identifier = new Identifier(inputValues);

            Assert.Equal(inputValues, identifier.PrimaryKeys);
        }
        public void Identifier_Equals_Test(Dictionary<string, object> inputValues)
        {
            var twinIdentifier1 = new Identifier(inputValues);

            var twinIdentifier2 = new Identifier(inputValues);

            Assert.True(twinIdentifier1.Equals(twinIdentifier2));
        }
    }
}

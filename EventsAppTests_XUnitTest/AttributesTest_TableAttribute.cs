using EventsApp.Logic.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsAppTests_XUnitTest
{
    internal class AttributesTest_TableAttribute
    {
        public void TableAttribute_Constructor(string name)
        {
            var table = new TableAttribute(name);

            Assert.Equal(name, table.TableName);
        }
    }
}

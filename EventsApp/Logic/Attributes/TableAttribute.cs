namespace EventsApp.Logic.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TableAttribute(string tableName)
        : Attribute
    {
        private string tableName = tableName;

        public string TableName
        {
            get
            {
                return this.tableName;
            }
        }
    }
}

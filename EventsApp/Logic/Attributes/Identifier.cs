namespace EventsApp.Logic.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public struct Identifier(Dictionary<string, object> primaryKeys)
    {
        private Dictionary<string, object> primaryKeys = primaryKeys;

        public Dictionary<string, object> PrimaryKeys
        {
            get
            {
                return this.primaryKeys;
            }
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj is Identifier identifier)
            {
                return this.primaryKeys.SequenceEqual(identifier.primaryKeys);
            }

            return false;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}

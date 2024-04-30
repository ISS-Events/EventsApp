namespace EventsApp.Logic.Adapters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using EventsApp.Logic.Attributes;
    using EventsApp.Logic.Entities;
    using EventsApp.Logic.Extensions;

    public abstract class DataAdapter<T>(string key)
        where T : struct
    {
        private string key = key;

        public abstract void Connect();

        public abstract void Clear();

        public abstract void Add(T item);

        public abstract T Get(Identifier id);

        public abstract List<T> GetAll();

        public abstract void Update(Identifier id, T item);

        public abstract void Delete(Identifier id);

        public abstract bool Contains(Identifier id);
    }
}

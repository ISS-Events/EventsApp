﻿using EventsApp.Logic.Attributes;
using EventsApp.Logic.Entities;
using EventsApp.Logic.Extensions;
using EventsApp.Logic.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsApp.Logic.Adapters
{
    /// <summary>
    /// Parses csv files
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CSVAdapter<T> : DataAdapter<T> where T : struct
    {
        string _filePath; // CSV file path

        public CSVAdapter(string filePath) : base(filePath)
        {
            _filePath = AppDataInfo.ValidatePath(filePath);
        }

        public override void Connect()
        {
            // No connection needed
        }

        public override void Clear()
        {
            try
            {
                System.IO.File.WriteAllText(_filePath, string.Empty);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error clearing CSV: {ex.Message}");
            }
        }

        public override void Add(T item)
        {
            List<T> items = GetAll();
            items.Add(item);
            WriteToCSV(items);
        }

        public override bool Contains(Identifier id)
        {
            List<T> items = GetAll();
            foreach (T item in items)
            {
                if (item.GetIdentifier().Equals(id))
                {
                    return true;
                }
            }
            return false;
        }

        public override void Delete(Identifier id)
        {
            List<T> items = GetAll();
            T item = Get(id);
            
            // Iterate and check their identifiers
            int index = -1;

            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].GetIdentifier().Equals(id))
                {
                    index = i;
                    break;
                }
            }

            if(index != -1) items.RemoveAt(index);

            WriteToCSV(items);
        }

        public override T Get(Identifier id)
        {
            List<T> items = GetAll();

            foreach (T item in items)
            {
                if (item.GetIdentifier().Equals(id))
                {
                    return item;
                }
            }

            return default(T);
        }

        public override List<T> GetAll()
        {
            var data = new List<T>();

            try
            {
                using (var reader = new StreamReader(_filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();

                        // We're working with structs, so reflection is tricky
                        // We'll match the fields and set their values to an instance
                        // of the generic type

                        var values = line.Split(',');

                        if (typeof(T).IsValueType && !typeof(T).IsPrimitive && !typeof(T).IsEnum)
                        {
                            // For structs, we'll use a workaround to create an instance
                            var instance = Activator.CreateInstance<T>();
                            var fields = typeof(T).GetFields();
                            
                            // We create a reference to the instance because structs are value types
                            // We cant use SetValue directly on the instance
                            // 7th page of google btw
                            TypedReference reference = __makeref(instance);

                            for (int i = 0; i < fields.Length && i < values.Length; i++)
                            {
                                if (fields[i].FieldType == typeof(Guid))
                                {
                                    fields[i].SetValueDirect(reference, Guid.Parse(values[i]));
                                }
                                else
                                {
                                    fields[i].SetValueDirect(reference, Convert.ChangeType(values[i], fields[i].FieldType));
                                }
                            }

                            data.Add(instance);
                        }
                        else
                        {
                            // Handle error: T is not a struct
                            Console.WriteLine("Error: T must be a struct.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing CSV: {ex.Message}");
            }

            return data;
        }

        public override void Update(Identifier id, T item)
        {
            List<T> items = GetAll();
            T oldItem = Get(id);
            items.Remove(oldItem);
            items.Add(item);
            WriteToCSV(items);
        }

        private void WriteToCSV(List<T> items)
        {
            try
            {
                using (var writer = new StreamWriter(_filePath))
                {
                    foreach (T item in items)
                    {
                        var fields = item.GetType().GetFields();

                        foreach (var field in fields)
                        {
                            object value = field.GetValue(item);
                            writer.Write(value.ToString());
                            writer.Write(",");
                        }

                        writer.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to CSV: {ex.Message}");
            }
        }

    }
}

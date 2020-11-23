﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;

namespace FileManager
{
    public class PseudoObject
    {
        public string name;
        public PseudoObject() { }
    }
    public class PseudoFile : PseudoObject
    {
        public string path;
        public List<string> tags;
        public PseudoFile() : base()
        {
            tags = new List<string>();
        }
        public PseudoFile(string n, string p) : this()
        {
            tags = new List<string>();
            name = n;
            path = p;
        }
        public override string ToString()
        {
            return $"{name} | {tags.Count} tags";
        }
    }
    public class PseudoFolder : PseudoObject
    {
        public List<string> tags;
        public List<PseudoFile> psFiles;
        public PseudoFolder() : base()
        {
            name = "new psFolder";
            psFiles = new List<PseudoFile>();
            tags = new List<string>();
        }
        public string Name { get => name; set { name = value; } }
        public int TagCount { get => tags.Count; }
        public int FileCount { get => psFiles.Count; }
        public override string ToString()
        {
            return $"{name} | {psFiles.Count} files | {tags.Count} tags";
        }
    }
    public class FML
    {
        public static FML curr;
        public string path;
        public List<PseudoFolder> psFolders;
        SortedDictionary<string, List<PseudoObject>> tagDict;
        //public SortedDictionary<string, List<PseudoObject>> TagDictionary { get => tagDict; }
        public FML()
        {
            psFolders = new List<PseudoFolder>();
            tagDict = new SortedDictionary<string, List<PseudoObject>>();
        }
        public void WriteToFile(bool append)
        {
            Xml.WriteToXmlFile(this.path, this, append);
        }
    }
    static public class Xml
    {
        /// <summary>
        /// Writes the given object instance to an XML file.
        /// <para>Only Public properties and variables will be written to the file. These can be any type though, even other classes.</para>
        /// <para>If there are public properties/variables that you do not want written to the file, decorate them with the [XmlIgnore] attribute.</para>
        /// <para>Object type must have a parameterless constructor.</para>
        /// </summary>
        /// <typeparam name="T">The type of object being written to the file.</typeparam>
        /// <param name="filePath">The file path to write the object instance to.</param>
        /// <param name="objectToWrite">The object instance to write to the file.</param>
        /// <param name="append">If false the file will be overwritten if it already exists. If true the contents will be appended to the file.</param>
        public static void WriteToXmlFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            TextWriter writer = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                writer = new StreamWriter(filePath, append);
                serializer.Serialize(writer, objectToWrite);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        /// <summary>
        /// Reads an object instance from an XML file.
        /// <para>Object type must have a parameterless constructor.</para>
        /// </summary>
        /// <typeparam name="T">The type of object to read from the file.</typeparam>
        /// <param name="filePath">The file path to read the object instance from.</param>
        /// <returns>Returns a new instance of the object read from the XML file.</returns>
        public static T ReadFromXmlFile<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                reader = new StreamReader(filePath);
                return (T)serializer.Deserialize(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        public static void Serialize(TextWriter writer, IDictionary dictionary)
        {
            List<Entry> entries = new List<Entry>(dictionary.Count);
            foreach (object key in dictionary.Keys)
            {
                entries.Add(new Entry(key, dictionary[key]));
            }
            XmlSerializer serializer = new XmlSerializer(typeof(List<Entry>));
            serializer.Serialize(writer, entries);
        }
        public static void Deserialize(TextReader reader, IDictionary dictionary)
        {
            dictionary.Clear();
            XmlSerializer serializer = new XmlSerializer(typeof(List<Entry>));
            List<Entry> list = (List<Entry>)serializer.Deserialize(reader);
            foreach (Entry entry in list)
            {
                dictionary[entry.Key] = entry.Value;
            }
        }
        public class Entry
        {
            public object Key;
            public object Value;
            public Entry()
            {
            }

            public Entry(object key, object value)
            {
                Key = key;
                Value = value;
            }
        }
    }

}

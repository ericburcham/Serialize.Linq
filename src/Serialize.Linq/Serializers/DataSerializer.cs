﻿using System;
using System.IO;
using Serialize.Linq.Nodes;
#if !WINDOWS_PHONE && !NETSTANDARD && !WINDOWS_UWP
using System.Runtime.Serialization;
#endif
using Serialize.Linq.Interfaces;

namespace Serialize.Linq.Serializers
{
    public abstract class DataSerializer : SerializerBase, ISerializer
    {
#if !WINDOWS_PHONE && !NETSTANDARD && !WINDOWS_UWP
        public virtual void Serialize<T>(Stream stream, T obj) where T : Node
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            var serializer = CreateXmlObjectSerializer(typeof(T));
            serializer.WriteObject(stream, obj);
        }

        public virtual T Deserialize<T>(Stream stream) where T : Node
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            var serializer = CreateXmlObjectSerializer(typeof(T));
            return (T)serializer.ReadObject(stream);
        }

        protected abstract XmlObjectSerializer CreateXmlObjectSerializer(Type type);

#else

        public abstract void Serialize<T>(Stream stream, T obj) where T : Node;

        public abstract T Deserialize<T>(Stream stream) where T : Node;

#endif
    }
}

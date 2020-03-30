using System;
using System.Collections.Generic;
using System.Text;

namespace System.Reflection
{
    internal static class TypeExtenstions
    {
        public static bool IsKindOfGeneric(this Type type, Type definition)
        {
            if (ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (ReferenceEquals(definition, null))
            {
                throw new ArgumentNullException(nameof(definition));
            }
            return (FindGenericType(type, definition) != null);
        }

        public static Type FindGenericType(this Type type, Type definition)
        {
            if (ReferenceEquals(type, null))
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (ReferenceEquals(definition, null))
            {
                throw new ArgumentNullException(nameof(definition));
            }
            while ((type != null) && (type != typeof(object)))
            {
                if (type.IsGenericType && (type.GetGenericTypeDefinition() == definition))
                {
                    return type;
                }
                if (definition.IsInterface)
                {
                    foreach (Type type2 in type.GetInterfaces())
                    {
                        Type type3 = FindGenericType(type2, definition);
                        if (type3 != null)
                        {
                            return type3;
                        }
                    }
                }
                type = type.BaseType;
            }
            return null;
        }
    }
}

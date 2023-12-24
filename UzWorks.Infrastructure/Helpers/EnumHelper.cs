﻿using System.ComponentModel;
using System.Reflection;

namespace UzWorks.Infrastructure.Helpers;

public static class EnumHelper
{
    public static string GetDescription(Enum value)
    {
        FieldInfo field = value.GetType().GetField(value.ToString());

        DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

        return attribute == null ? value.ToString() : attribute.Description;
    }
}

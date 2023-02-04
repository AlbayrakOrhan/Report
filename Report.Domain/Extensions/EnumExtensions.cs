using System.ComponentModel;

namespace Report.Domain.Extensions;

public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        if (value == null)
        {
            throw new ArgumentNullException("value");
        }

        var description = value.ToString();
        var fieldInfo = value.GetType().GetField(description);
        var attributes =
            (DescriptionAttribute[])
            fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

        if (attributes.Length > 0)
        {
            description = attributes[0].Description;
        }

        return description;
    }
}
namespace NoteShare.Core.Extensions
{
    public static class EnumExtension
    {
        public static void ValidateEnumValue<T>(this T value) where T : struct, IComparable, IFormattable, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("NOT_ENUM_TYPE");
            }

            if (!Enum.IsDefined(typeof(T), value) || Convert.ToInt32(value) >= Enum.GetValues(typeof(T)).Length)
            {
                throw new ArgumentException("INVALID_ENUM_VALUE");
            }
        }
    }
}

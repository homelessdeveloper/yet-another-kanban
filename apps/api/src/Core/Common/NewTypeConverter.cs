using LanguageExt;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Yak.Core.Common;

/// <summary>
/// Converts between a custom new type and its underlying type for storage in a database using Entity Framework Core.
/// </summary>
/// <typeparam name="TSource">The source type.</typeparam>
/// <typeparam name="TType">The target type.</typeparam>
/// <since>0.0.1</since>
public class NewTypeConverter<TSource, TType> : ValueConverter<TSource, TType> where TSource : NewType<TSource, TType>
{
  /// <summary>
  /// Initializes a new instance of the <see cref="NewTypeConverter{S, T}"/> class.
  /// </summary>
  /// <since>0.0.1</since>
  public NewTypeConverter()
    : base(
      // Converts the source value to its underlying value.
      v => v.Value,
      // Converts the target value to a new instance of the new type.
      v => NewType<TSource, TType>.New(v)
    )
  {
  }
}

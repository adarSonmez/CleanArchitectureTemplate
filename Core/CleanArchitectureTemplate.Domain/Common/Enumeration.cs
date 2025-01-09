using System.Reflection;

namespace CleanArchitectureTemplate.Domain.Common;

/// <summary>
/// Base class for creating enumeration types.
/// Provides functionality to define enumerations with additional metadata.
/// </summary>
public abstract class Enumeration : IComparable
{
    #region Public Properties

    /// <summary>
    /// Gets the unique identifier of the enumeration.
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Gets the name of the enumeration.
    /// </summary>
    public string Name { get; private set; }

    #endregion Public Properties

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="Enumeration"/> class.
    /// </summary>
    /// <param name="id">The unique identifier of the enumeration.</param>
    /// <param name="name">The name of the enumeration.</param>
    protected Enumeration(int id, string name)
    {
        Id = id;
        Name = name;
    }

    #endregion Constructor

    #region Instance Methods

    /// <summary>
    /// Compares the current instance with another object of the same type.
    /// </summary>
    /// <param name="obj">An object to compare with this instance.</param>
    /// <returns>An integer that indicates the relative order of the objects being compared.</returns>
    public int CompareTo(object? obj)
    {
        return Id.CompareTo((obj as Enumeration)?.Id);
    }

    /// <summary>
    /// Converts the enumeration to a string.
    /// </summary>
    /// <returns>The name of the enumeration.</returns>
    public override string ToString()
    {
        return Name;
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current object.
    /// </summary>
    /// <param name="obj">The object to compare with the current object.</param>
    /// <returns><c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is not Enumeration otherEnum)
            return false;

        return Id == otherEnum.Id && Name == otherEnum.Name;
    }

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current object.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name);
    }

    #endregion Instance Methods

    #region Static Methods

    /// <summary>
    /// Gets the enumeration instance with the specified ID.
    /// </summary>
    /// <typeparam name="T">The type of the enumeration.</typeparam>
    /// <param name="id">The unique identifier of the enumeration.</param>
    /// <returns>The matching enumeration instance.</returns>
    public static T FromId<T>(int id) where T : Enumeration
    {
        var matchingItem = GetAll<T>().FirstOrDefault(e => e.Id == id);

        return matchingItem ?? throw new ArgumentException($"No {typeof(T).Name} with Id {id} found.", nameof(id));
    }

    /// <summary>
    /// Gets the enumeration instance with the specified name.
    /// </summary>
    /// <typeparam name="T">The type of the enumeration.</typeparam>
    /// <param name="name">The name of the enumeration.</param>
    /// <returns>The matching enumeration instance.</returns>
    public static T FromName<T>(string name) where T : Enumeration
    {
        var matchingItem = GetAll<T>().FirstOrDefault(e => string.Equals(e.Name, name, StringComparison.OrdinalIgnoreCase));

        return matchingItem ?? throw new ArgumentException($"No {typeof(T).Name} with Name {name} found.", nameof(name));
    }

    /// <summary>
    /// Returns all instances of the enumeration type.
    /// </summary>
    /// <typeparam name="T">The type of the enumeration.</typeparam>
    /// <returns>A list of all enumeration instances.</returns>
    public static IEnumerable<T> GetAll<T>() where T : Enumeration
    {
        return typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                        .Select(f => f.GetValue(null))
                        .Cast<T>();
    }

    #endregion Static Methods

    #region Operator Overloads

    /// <summary>
    /// Determines if two enumerations are equal.
    /// </summary>
    /// <param name="left">The left enumeration.</param>
    /// <param name="right">The right enumeration.</param>
    /// <returns><c>true</c> if the enumerations are equal; otherwise, <c>false</c>.</returns>
    public static bool operator ==(Enumeration left, Enumeration right)
    {
        if (left is null)
            return right is null;

        return left.Equals(right);
    }

    /// <summary>
    /// Determines if two enumerations are not equal.
    /// </summary>
    /// <param name="left">The left enumeration.</param>
    /// <param name="right">The right enumeration.</param>
    /// <returns><c>true</c> if the enumerations are not equal; otherwise, <c>false</c>.</returns>
    public static bool operator !=(Enumeration left, Enumeration right)
    {
        return !(left == right);
    }

    #endregion Operator Overloads
}
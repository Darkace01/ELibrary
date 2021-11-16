namespace ELibrary.Core;

/// <summary>
/// General attributes of all entities in the database
/// </summary>
public class Entity
{
    /// <summary>
    /// Primary key Id of the object
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Determines if the object is deleted or not
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// The date and time the object was created in LOCAL time
    /// </summary>
    public DateTime DateCreated { get; set; }

    /// <summary>
    /// The date and time object was last modified in LOCAL time
    /// </summary>
    public DateTime LastModified { get; set; }
}

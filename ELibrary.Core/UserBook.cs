namespace ELibrary.Core;

public class UserBook : Entity
{
    /// <summary>
    /// User account connected to the book
    /// </summary>
    public ApplicationUser? User { get; set; }
    /// <summary>
    /// User Id
    /// </summary>
    public string UserId { get; set; }
    /// <summary>
    /// Book owned by the user
    /// </summary>
    public Book? Book { get; set; }
    /// <summary>
    /// Book Id
    /// </summary>
    public int BookId { get; set; }
}

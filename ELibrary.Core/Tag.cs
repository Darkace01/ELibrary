namespace ELibrary.Core
{
    public class Tag : Entity
    {
        /// <summary>
        /// Name of tag
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Determines if the tag is displayed in search options
        /// </summary>
        public bool IsFeatured { get; set; }
    }
}

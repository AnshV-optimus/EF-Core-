namespace EntityFrameworkCoreApp.Data
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        //[System.Text.Json.Serialization.JsonIgnore]
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}

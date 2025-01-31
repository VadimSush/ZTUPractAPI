namespace PractAPI
{
    public class Book
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public Book(int id, string name, string author)
        {
            Id = id;
            Name = name;
            Author = author;
        }
    }
}

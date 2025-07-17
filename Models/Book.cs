namespace BookLendingSystem.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public string Description { get; set; }
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }
        public bool IsAvailable { get; set; }
        public string ISBN { get; set; }
    }
}

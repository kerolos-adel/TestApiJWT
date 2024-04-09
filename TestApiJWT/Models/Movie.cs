using System.ComponentModel.DataAnnotations.Schema;

namespace TestApiJWT.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        public string StoreLine { get; set; }
        public byte[] Poster { get; set; }
        public int GategoryId { get; set; }
        [ForeignKey("GategoryId")]
        public Category Category { get; set; }
    }
}

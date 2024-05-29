using Newtonsoft.Json;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        [JsonProperty("uId")]
        public string UId { get; set; }

        [JsonProperty("title")]

        public string Title { get; set; }

        [JsonProperty("author")]

        public string Author { get; set; }

        [JsonProperty("publishedDate")]

        public DateTime PublishedDate { get; set; }

        [JsonProperty("isbn")]

        public string ISBN { get; set; }

        [JsonProperty("isIssued")]

        public bool IsIssued { get; set; }
    }
}

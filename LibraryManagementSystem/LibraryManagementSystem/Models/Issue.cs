using Newtonsoft.Json;

namespace LibraryManagementSystem.Models
{
    public class Issue
    {
        [JsonProperty("uId")]

        public string UId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("dateofBirth")]

        public DateTime DateOfBirth { get; set; }

        [JsonProperty("email")]

        public string Email { get; set; }
    }
}        


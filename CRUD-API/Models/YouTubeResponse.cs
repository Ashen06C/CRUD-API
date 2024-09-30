namespace CRUD_API.Models
{
    public class YouTubeResponse
    {
        public List<VideoDetails> Videos { get; set; } = new List<VideoDetails>();
        public string? NextPageToken { get; set; }
        public string? PrevPageToken { get; set;}
    }
}

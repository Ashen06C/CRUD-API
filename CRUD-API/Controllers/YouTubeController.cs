using CRUD_API.Models;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YouTubeController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetChannelVideo(string? pageToken = null, int maxResults = 50)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyCWeIPicjtBaTcrbzNtWacUKWXNPl1dRjc",
                ApplicationName = "My YouTube Project"
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.ChannelId = "UCJA-NQ4MtcRIog66wziD8fA";
            searchListRequest.Order = SearchResource.ListRequest.OrderEnum.Date;
            searchListRequest.MaxResults = maxResults;
            searchListRequest.PageToken = pageToken;

            var searchListResponse = await searchListRequest.ExecuteAsync();

            var videoList = searchListResponse.Items.Select(item => new VideoDetails
            {
                Title = item.Snippet.Title,
                Link = $"https://www.youtube.com/watch?v={item.Id.VideoId}",
                Thumbnail = item.Snippet.Thumbnails.Medium.Url,
                PublishedAt = item.Snippet.PublishedAt ?? DateTimeOffset.MinValue
            })
                .OrderByDescending(item => item.PublishedAt)
                .ToList();

            var response = new YouTubeResponse
            {
                NextPageToken = searchListResponse.NextPageToken,
                Videos = videoList,
                PrevPageToken = searchListResponse.PrevPageToken
            };

            return Ok(response);
        }
    }
}

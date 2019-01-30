using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Victoria.Entities;

namespace Victoria.Utilities
{
    public sealed class ThumbnailResolver
    {
        /// <summary>
        /// Finds the proper thumbnail of the specified track. Currently supports: youtube, twitch, soundcloud, vimeo.
        /// </summary>
        /// <param name="track"><see cref="LavaTrack"/></param>
        /// <returns>Url of the thumbnail.</returns>
        public static async Task<string> FetchAsync(LavaTrack track)
        {
            JToken thumbObj;
            
            using (var http = new HttpClient())
            {
                JObject result;
                switch ($"{track.Uri}".ToLower())
                {
                    case var yt when yt.Contains("youtube"):
                        return $"https://img.youtube.com/vi/{track.Id}/maxresdefault.jpg";

                    case var twich when twich.Contains("twitch"):
                        result = JObject.Parse(
                            await http.GetStringAsync($"https://api.twitch.tv/v4/oembed?url={track.Uri}"));
                        result.TryGetValue("thumbnail_url", out thumbObj);
                        break;

                    case var sc when sc.Contains("soundcloud"):
                        result = JObject.Parse(
                            await http.GetStringAsync($"https://soundcloud.com/oembed?url={track.Uri}&format=json"));
                        result.TryGetValue("thumbnail_url", out thumbObj);
                        break;

                    case var vim when vim.Contains("vimeo"):
                        result = JObject.Parse(
                            await http.GetStringAsync($"https://vimeo.com/api/oembed.json?url={track.Uri}"));
                        result.TryGetValue("thumbnail_url", out thumbObj);
                        break;

                    default:
                        return "https://i.imgur.com/YPCEUDK.gif";
                }
            }

            var thumbnail = $"{thumbObj}";
            return string.IsNullOrWhiteSpace(thumbnail) ? "https://i.imgur.com/YPCEUDK.gif" : thumbnail;
        }
    }
}
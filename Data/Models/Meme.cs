namespace Data.Models
{
    using System;
    using Newtonsoft.Json;

    public partial class Meme
    {
        [JsonProperty("postLink")]
        public Uri PostLink { get; set; }

        [JsonProperty("subreddit")]
        public string Subreddit { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("nsfw")]
        public bool Nsfw { get; set; }

        [JsonProperty("spoiler")]
        public bool Spoiler { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("ups")]
        public long Ups { get; set; }

        [JsonProperty("preview")]
        public Uri[] Preview { get; set; }
    }

    public partial class Meme
    {
        public static Meme FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Meme>(json, Converter.Settings);
        }
    }
}

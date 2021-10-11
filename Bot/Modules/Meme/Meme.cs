using System.Net.Http;
using System.Threading.Tasks;
using Bot.Common;
using Discord.Commands;

namespace Bot.Modules.Meme
{
    public class Meme : ModuleBase<SocketCommandContext>
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public Meme(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Command("meme")]
        public async Task GetMeme()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetStringAsync("https://meme-api.herokuapp.com/gimme");
            var meme = Data.Models.Meme.FromJson(response);

            if (meme == null)
            {
                await ReplyAsync(
                    "Bots fucked and couldn't find your meme. Let Cillian know that he's an idiot and needs to fix it.");
                return;
            }

            var memeEmbed = new SP1XEmbedBuilder()
                .WithTitle($"{meme.Title}")
                .WithImage($"{meme.Url}")
                .WithFooter($"⬆️: {meme.Ups} Source: {meme.Author} @ /r/{meme.Subreddit}")
                .WithStyle(EmbedStyle.Image)
                .Build();

            await Context.Channel.SendMessageAsync(embed: memeEmbed);
            return;
        }
    }
}

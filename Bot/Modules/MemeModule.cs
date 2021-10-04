using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Bot.Common;
using Data.Models;
using Discord.Commands;

namespace Bot.Modules
{
    public class MemeModule : ModuleBase<SocketCommandContext>
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MemeModule(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Command("meme")]
        public async Task GetMeme()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetStringAsync("https://meme-api.herokuapp.com/gimme");
            var meme = Meme.FromJson(response);

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

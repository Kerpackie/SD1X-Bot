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
    public class PepeModule : ModuleBase<SocketCommandContext>
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PepeModule(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Command("pepe")]
        public async Task GetPepe()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetStringAsync("https://meme-api.herokuapp.com/gimme/pepethefrog");
            var pepe = Meme.FromJson(response);

            if (pepe == null)
            {
                await ReplyAsync(
                    "Pepe is too hungover to return any pictures of last night.. Fuck off and try again later. While your at it let Cillian know that he's an idiot and needs to fix Pepe.");
                return;
            }

            var pepeEmbed = new SP1XEmbedBuilder()
                .WithTitle($"{pepe.Title}")
                .WithImage($"{pepe.Url}")
                .WithStyle(EmbedStyle.Image)
                .Build();

            await Context.Channel.SendMessageAsync(embed: pepeEmbed);
            return;
        }
    }
}
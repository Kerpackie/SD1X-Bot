using System.Net.Http;
using System.Threading.Tasks;
using Bot.Common;
using Discord.Commands;

namespace Bot.Modules.Meme
{
    public class Pepe : ModuleBase<SocketCommandContext>
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public Pepe(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Command("pepe")]
        public async Task GetPepe()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetStringAsync("https://meme-api.herokuapp.com/gimme/pepethefrog");
            var pepe = Data.Models.Meme.FromJson(response);

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
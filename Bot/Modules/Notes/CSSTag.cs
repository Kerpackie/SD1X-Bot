using System;
using System.Linq;
using System.Threading.Tasks;
using Bot.Common;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace Bot.Modules.Notes
{
    public class CSSTagModule : SP1XModuleBase
    {
        public CSSTagModule(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("csstags", RunMode = RunMode.Async)]
        public async Task CSSTags()
        {
            var cssTags = await _DataAccessLayer.GetCSSTags();

            if (!cssTags.Any())
            {
                var noCSSTags = new SP1XEmbedBuilder()
                    .WithTitle("No CSS Tags Found")
                    .WithDescription("This server does not have any CSS Tags yet.")
                    .WithStyle(EmbedStyle.Error)
                    .Build();

                await Context.Channel.SendMessageAsync(embed: noCSSTags);
                return;
            }

            string description = string.Join(", ", cssTags.Select(x => x.Tag));
            var prefix = _DataAccessLayer.GetGuildPrefix(Context.Guild.Id);

            var list = new SP1XEmbedBuilder()
                .WithTitle($"CSS Tags ({cssTags.Count()}")
                .WithDescription(description)
                .WithFooter($"Use \"{prefix}csstag name\" to view a CSS Tag.")
                .WithStyle(EmbedStyle.Information)
                .Build();
            await this.Context.Channel.SendMessageAsync(embed: list);
        }

        [Command("csstag", RunMode = RunMode.Async)]
        public async Task CSSTag([Remainder] string argument)
        {
            var arguments = argument.Split(" ");

            if (arguments.Count() == 1 && arguments[0] != "create" && arguments[0] != "edit" &&
                arguments[0] != "transfer" && arguments[0] != "delete")
            {
                var cssTag = await _DataAccessLayer.GetCSSTag(arguments[0]);
                if (cssTag is null)
                {
                    var embed = new SP1XEmbedBuilder()
                        .WithTitle("Not Found")
                        .WithDescription("The CSS Tag you requested could not be found.")
                        .WithStyle(EmbedStyle.Error)
                        .Build();

                    await Context.Channel.SendMessageAsync(embed: embed);
                    return;
                }

                var cssTagEmbed = new SP1XEmbedBuilder()
                    .WithTitle(cssTag.Tag)
                    .WithDescription(cssTag.Content)
                    .WithStyle(EmbedStyle.Information)
                    .Build();

                await Context.Channel.SendMessageAsync(embed: cssTagEmbed);
                return;
            }

            var socketGuildUser = Context.User as SocketGuildUser;

            switch (arguments[0])
            {
                case "create":
                    var cssTag = await _DataAccessLayer.GetCSSTag(arguments[1]);
                    if (cssTag != null)
                    {
                        var embed = new SP1XEmbedBuilder()
                            .WithTitle("Already Exists")
                            .WithDescription("There already exists a CSS Tag with that name.")
                            .WithStyle(EmbedStyle.Error)
                            .Build();

                        await Context.Channel.SendMessageAsync(embed: embed);
                        return;
                    }

                    if (!Context.User.IsPromoted())
                    {
                        var embed = new SP1XEmbedBuilder()
                            .WithTitle("Access Denied!")
                            .WithDescription("You need to be a student or administrator to create a CSS Tag.")
                            .WithStyle(EmbedStyle.Error)
                            .Build();

                        await Context.Channel.SendMessageAsync(embed: embed);
                        return;
                    }

                    await _DataAccessLayer.CreateCSSTag(arguments[1], Context.User.Id,
                        string.Join(" ", arguments.Skip(2)));

                    var prefix = await _DataAccessLayer.GetGuildPrefix(Context.Guild.Id);

                    var created = new SP1XEmbedBuilder()
                        .WithTitle("CSS Tag Created!")
                        .WithDescription(
                            $"The CSS Tag has been successfully created. You can view it by using `{prefix}csstag {arguments[1]}`")
                        .WithStyle(EmbedStyle.Success)
                        .Build();
                    await Context.Channel.SendMessageAsync(embed: created);
                    break;
                case "edit":
                    var foundCSSTag = await _DataAccessLayer.GetCSSTag(arguments[1]);
                    if (foundCSSTag == null)
                    {
                        var embed = new SP1XEmbedBuilder()
                            .WithTitle("Not Found")
                            .WithDescription("The CSS Tag you requested could not be found.")
                            .WithStyle(EmbedStyle.Error)
                            .Build();

                        await Context.Channel.SendMessageAsync(embed: embed);
                        return;
                    }

                    if (foundCSSTag.OwnerId != Context.User.Id && !socketGuildUser.GuildPermissions.Administrator)
                    {
                        var embed = new SP1XEmbedBuilder()
                            .WithTitle("Access Denied!")
                            .WithDescription("You need to be a student or administrator to create a CSS Tag.")
                            .WithStyle(EmbedStyle.Error)
                            .Build();

                        await Context.Channel.SendMessageAsync(embed: embed);
                        return;
                    }

                    await _DataAccessLayer.EditCSSTagContent(arguments[1], string.Join(" ", arguments.Skip(2)));

                    var edited = new SP1XEmbedBuilder()
                        .WithTitle("Access Denied!")
                        .WithDescription("You need to be a student or administrator to create a CSS Tag.")
                        .WithStyle(EmbedStyle.Error)
                        .Build();
                    await Context.Channel.SendMessageAsync(embed: edited);
                    break;
                case "transfer":
                    var cssTagToTransfer = await _DataAccessLayer.GetCSSTag(arguments[1]);
                    if (cssTagToTransfer == null)
                    {
                        var embed = new SP1XEmbedBuilder()
                            .WithTitle("Not Found!")
                            .WithDescription("That CSS Tag could not be found.")
                            .WithStyle(EmbedStyle.Error)
                            .Build();

                        await this.Context.Channel.SendMessageAsync(embed: embed);
                        return;
                    }

                    if (!MentionUtils.TryParseUser(arguments[2], out ulong userId) ||
                        Context.Guild.GetUser(userId) == null)
                    {
                        var embed = new SP1XEmbedBuilder()
                            .WithTitle("Invalid User")
                            .WithDescription("Please provide a valid user.")
                            .WithStyle(EmbedStyle.Error)
                            .Build();

                        await Context.Channel.SendMessageAsync(embed: embed);
                        return;
                    }

                    if (cssTagToTransfer.OwnerId != this.Context.User.Id && !socketGuildUser.GuildPermissions.Administrator)
                    {
                        var embed = new SP1XEmbedBuilder()
                            .WithTitle("Access Denied!")
                            .WithDescription("You need to be the owner of this tag or an administrator to edit the content of this CSS Tag.")
                            .WithStyle(EmbedStyle.Error)
                            .Build();

                        await this.Context.Channel.SendMessageAsync(embed: embed);
                        return;
                    }

                    await _DataAccessLayer.EditCSSTagOwner(arguments[1], userId);

                    var success = new SP1XEmbedBuilder()
                        .WithTitle("CSS Tag Ownership Transferred")
                        .WithDescription($"The ownership of the tag has been transferred to <@{userId}>.")
                        .WithStyle(EmbedStyle.Success)
                        .Build();

                    await this.Context.Channel.SendMessageAsync(embed: success);
                    break;
                case "delete":

                    var cssTagtoDelete = await _DataAccessLayer.GetCSSTag(arguments[1]);

                    if (cssTagtoDelete == null)
                    {
                        var embed = new SP1XEmbedBuilder()
                            .WithTitle("Not Found!")
                            .WithDescription("That CSS Tag could not be found.")
                            .WithStyle(EmbedStyle.Error)
                            .Build();

                        await this.Context.Channel.SendMessageAsync(embed: embed);
                        return;
                    }

                    if (cssTagtoDelete.OwnerId != this.Context.User.Id && !socketGuildUser.GuildPermissions.Administrator)
                    {
                        var embed = new SP1XEmbedBuilder()
                            .WithTitle("Access Denied!")
                            .WithDescription("You need to be the owner of this tag or an administrator to edit the content of this CSS Tag.")
                            .WithStyle(EmbedStyle.Error)
                            .Build();

                        await this.Context.Channel.SendMessageAsync(embed: embed);
                        return;
                    }

                    await _DataAccessLayer.DeleteCSSTag(arguments[1]);

                    var deleted = new SP1XEmbedBuilder()
                        .WithTitle("CSS Tag Deleted!")
                        .WithDescription("The CSS Tag was successfully deleted.")
                        .WithStyle(EmbedStyle.Success)
                        .Build();

                    await this.Context.Channel.SendMessageAsync(embed: deleted);
                    break;
            }
        }
    }
}

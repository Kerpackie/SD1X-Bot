using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bot.Common;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;

namespace Bot.Modules
{
    public class HTMLTagModule : SP1XModuleBase
    {
        public HTMLTagModule(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("htmltags", RunMode = RunMode.Async)]
        public async Task HTMLTags()
        {
            var htmlTags = await _DataAccessLayer.GetHTMLTags();

            if (!htmlTags.Any())
            {
                var noHtmlTags = new SP1XEmbedBuilder()
                    .WithTitle("No HTML Tags Found")
                    .WithDescription("This server does not have any HTML Tags yet.")
                    .WithStyle(EmbedStyle.Error)
                    .Build();

                await Context.Channel.SendMessageAsync(embed: noHtmlTags);
                return;
            }

            string description = string.Join(", ", htmlTags.Select(x => x.Tag));
            var prefix = _DataAccessLayer.GetGuildPrefix(Context.Guild.Id);

            var list = new SP1XEmbedBuilder()
                .WithTitle($"HTML Tags ({htmlTags.Count()}")
                .WithDescription(description)
                .WithFooter($"Use \"{prefix}htmltag name\" to view a HTML Tag.")
                .WithStyle(EmbedStyle.Information)
                .Build();
            await this.Context.Channel.SendMessageAsync(embed: list);
        }

        [Command("htmltag", RunMode = RunMode.Async)]
        public async Task HtmlTag([Remainder] string argument)
        {
            var arguments = argument.Split(" ");

            if (arguments.Count() == 1 && arguments[0] != "create" && arguments[0] != "edit" &&
                arguments[0] != "transfer" && arguments[0] != "delete")
            {
                var htmlTag = await _DataAccessLayer.GetHTMLTag(arguments[0]);
                if (htmlTag is null)
                {
                    var embed = new SP1XEmbedBuilder()
                        .WithTitle("Not Found")
                        .WithDescription("The HTML Tag you requested could not be found.")
                        .WithStyle(EmbedStyle.Error)
                        .Build();

                    await Context.Channel.SendMessageAsync(embed: embed);
                    return;
                }

                var htmlTagEmbed = new SP1XEmbedBuilder()
                    .WithTitle(htmlTag.Tag)
                    .WithDescription(htmlTag.Content)
                    .WithStyle(EmbedStyle.Information)
                    .Build();

                await Context.Channel.SendMessageAsync(embed: htmlTagEmbed);
                return;
            }

            var socketGuildUser = Context.User as SocketGuildUser;

            switch (arguments[0])
            {
                case "create":
                    var htmlTag = await _DataAccessLayer.GetHTMLTag(arguments[1]);
                    if (htmlTag != null)
                    {
                        var embed = new SP1XEmbedBuilder()
                            .WithTitle("Already Exists")
                            .WithDescription("There already exists a HTML Tag with that name.")
                            .WithStyle(EmbedStyle.Error)
                            .Build();

                        await Context.Channel.SendMessageAsync(embed: embed);
                        return;
                    }

                    if (!Context.User.IsPromoted())
                    {
                        var embed = new SP1XEmbedBuilder()
                            .WithTitle("Access Denied!")
                            .WithDescription("You need to be a student or administrator to create a HTML Tag.")
                            .WithStyle(EmbedStyle.Error)
                            .Build();

                        await Context.Channel.SendMessageAsync(embed: embed);
                        return;
                    }

                    await _DataAccessLayer.CreateHTMLTag(arguments[1], Context.User.Id,
                        string.Join(" ", arguments.Skip(2)));

                    var prefix = await _DataAccessLayer.GetGuildPrefix(Context.Guild.Id);

                    var created = new SP1XEmbedBuilder()
                        .WithTitle("HTML Tag Created!")
                        .WithDescription(
                            $"The HTML Tag has been successfully created. You can view it by using `{prefix}htmltag {arguments[1]}`")
                        .WithStyle(EmbedStyle.Success)
                        .Build();
                    await Context.Channel.SendMessageAsync(embed: created);
                    break;
                case "edit":
                    var foundHtmlTag = await _DataAccessLayer.GetHTMLTag(arguments[1]);
                    if (foundHtmlTag == null)
                    {
                        var embed = new SP1XEmbedBuilder()
                            .WithTitle("Not Found")
                            .WithDescription("The HTML Tag you requested could not be found.")
                            .WithStyle(EmbedStyle.Error)
                            .Build();

                        await Context.Channel.SendMessageAsync(embed: embed);
                        return;
                    }

                    if (foundHtmlTag.OwnerId != Context.User.Id && !socketGuildUser.GuildPermissions.Administrator)
                    {
                        var embed = new SP1XEmbedBuilder()
                            .WithTitle("Access Denied!")
                            .WithDescription("You need to be a student or administrator to create a HTML Tag.")
                            .WithStyle(EmbedStyle.Error)
                            .Build();

                        await Context.Channel.SendMessageAsync(embed: embed);
                        return;
                    }

                    await _DataAccessLayer.EditHTMLTagContent(arguments[1], string.Join(" ", arguments.Skip(2)));

                    var edited = new SP1XEmbedBuilder()
                        .WithTitle("Access Denied!")
                        .WithDescription("You need to be a student or administrator to create a HTML Tag.")
                        .WithStyle(EmbedStyle.Error)
                        .Build();
                    await Context.Channel.SendMessageAsync(embed: edited);
                    break;
                case "transfer":
                    var htmlTagToTransfer = await _DataAccessLayer.GetHTMLTag(arguments[1]);
                    if (htmlTagToTransfer == null)
                    {
                        var embed = new SP1XEmbedBuilder()
                            .WithTitle("Not Found!")
                            .WithDescription("That HTML Tag could not be found.")
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

                    if (htmlTagToTransfer.OwnerId != this.Context.User.Id && !socketGuildUser.GuildPermissions.Administrator)
                    {
                        var embed = new SP1XEmbedBuilder()
                            .WithTitle("Access Denied!")
                            .WithDescription("You need to be the owner of this tag or an administrator to edit the content of this HTML Tag.")
                            .WithStyle(EmbedStyle.Error)
                            .Build();

                        await this.Context.Channel.SendMessageAsync(embed: embed);
                        return;
                    }

                    await _DataAccessLayer.EditHTMLTagOwner(arguments[1], userId);

                    var success = new SP1XEmbedBuilder()
                        .WithTitle("HTML Tag Ownership Transferred")
                        .WithDescription($"The ownership of the tag has been transferred to <@{userId}>.")
                        .WithStyle(EmbedStyle.Success)
                        .Build();

                    await this.Context.Channel.SendMessageAsync(embed: success);
                    break;
                case "delete":

                    var htmlTagtoDelete = await _DataAccessLayer.GetHTMLTag(arguments[1]);

                    if (htmlTagtoDelete == null)
                    {
                        var embed = new SP1XEmbedBuilder()
                            .WithTitle("Not Found!")
                            .WithDescription("That HTML Tag could not be found.")
                            .WithStyle(EmbedStyle.Error)
                            .Build();

                        await this.Context.Channel.SendMessageAsync(embed: embed);
                        return;
                    }

                    if (htmlTagtoDelete.OwnerId != this.Context.User.Id && !socketGuildUser.GuildPermissions.Administrator)
                    {
                        var embed = new SP1XEmbedBuilder()
                            .WithTitle("Access Denied!")
                            .WithDescription("You need to be the owner of this tag or an administrator to edit the content of this HTML Tag.")
                            .WithStyle(EmbedStyle.Error)
                            .Build();

                        await this.Context.Channel.SendMessageAsync(embed: embed);
                        return;
                    }

                    await _DataAccessLayer.DeleteHTMLTag(arguments[1]);

                    var deleted = new SP1XEmbedBuilder()
                        .WithTitle("HTML Tag Deleted!")
                        .WithDescription("The HTML Tag was successfully deleted.")
                        .WithStyle(EmbedStyle.Success)
                        .Build();

                    await this.Context.Channel.SendMessageAsync(embed: deleted);
                    break;
            }
        }
    }
}

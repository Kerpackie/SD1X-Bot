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
    public class CSharp : SP1XModuleBase
    {
        public CSharp(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider, configuration)
        {
        }

        [Command("csharptags", RunMode = RunMode.Async)]
        [Alias("c#tags")]
        public async Task CSharpTags()
        {
            var cSharpTags = await _DataAccessLayer.GetCSharpTags();

            if (!cSharpTags.Any())
            {
                var noCSharpTags = new SP1XEmbedBuilder()
                    .WithTitle("No C# Tags Found")
                    .WithDescription("This server does not have any C# Tags yet.")
                    .WithStyle(EmbedStyle.Error)
                    .Build();

                await Context.Channel.SendMessageAsync(embed: noCSharpTags);
                return;
            }

            string description = string.Join(", ", cSharpTags.Select(x => x.Tag));
            var prefix = _DataAccessLayer.GetGuildPrefix(Context.Guild.Id);

            var list = new SP1XEmbedBuilder()
                .WithTitle($"C# Tags ({cSharpTags.Count()}")
                .WithDescription(description)
                .WithFooter($"Use \"{prefix}csharptag name\" to view a C# Tag.")
                .WithStyle(EmbedStyle.Information)
                .Build();
            await Context.Channel.SendMessageAsync(embed: list);
        }

        [Command("csharptag", RunMode = RunMode.Async)]
        [Alias("c#")]
        public async Task CSharpTag([Remainder] string argument)
        {
            var arguments = argument.Split(" ");

            if (arguments.Count() == 1 && arguments[0] != "create" && arguments[0] != "edit" &&
                arguments[0] != "transfer" && arguments[0] != "delete")
            {
                var cSharpTag = await _DataAccessLayer.GetCSharpTag(arguments[0]);
                if (cSharpTag is null)
                {
                    var embed = new SP1XEmbedBuilder()
                        .WithTitle("Not Found")
                        .WithDescription("The C# Tag you requested could not be found.")
                        .WithStyle(EmbedStyle.Error)
                        .Build();

                    await Context.Channel.SendMessageAsync(embed: embed);
                    return;
                }

                var cSharpTagEmbed = new SP1XEmbedBuilder()
                    .WithTitle(cSharpTag.Tag)
                    .WithDescription(cSharpTag.Content)
                    .WithStyle(EmbedStyle.Information)
                    .Build();

                await Context.Channel.SendMessageAsync(embed: cSharpTagEmbed);
                return;
            }

            var socketGuildUser = Context.User as SocketGuildUser;

            switch (arguments[0])
            {
                case "create":
                    var cSharpTag = await _DataAccessLayer.GetCSharpTag(arguments[1]);
                    if (cSharpTag != null)
                    {
                        var embed = new SP1XEmbedBuilder()
                            .WithTitle("Already Exists")
                            .WithDescription("There already exists a C# Tag with that name.")
                            .WithStyle(EmbedStyle.Error)
                            .Build();

                        await Context.Channel.SendMessageAsync(embed: embed);
                        return;
                    }

                    if (!Context.User.IsPromoted())
                    {
                        var embed = new SP1XEmbedBuilder()
                            .WithTitle("Access Denied!")
                            .WithDescription("You need to be a student or administrator to create a C# Tag.")
                            .WithStyle(EmbedStyle.Error)
                            .Build();

                        await Context.Channel.SendMessageAsync(embed: embed);
                        return;
                    }

                    await _DataAccessLayer.CreateCSharpTag(arguments[1], Context.User.Id,
                        string.Join(" ", arguments.Skip(2)));

                    var prefix = await _DataAccessLayer.GetGuildPrefix(Context.Guild.Id);

                    var created = new SP1XEmbedBuilder()
                        .WithTitle("C# Tag Created!")
                        .WithDescription(
                            $"The C# Tag has been successfully created. You can view it by using `{prefix}csharptag {arguments[1]}`")
                        .WithStyle(EmbedStyle.Success)
                        .Build();
                    await Context.Channel.SendMessageAsync(embed: created);
                    break;
                case "edit":
                    var foundCSharpTag = await _DataAccessLayer.GetCSharpTag(arguments[1]);
                    if (foundCSharpTag == null)
                    {
                        var embed = new SP1XEmbedBuilder()
                            .WithTitle("Not Found")
                            .WithDescription("The C# Tag you requested could not be found.")
                            .WithStyle(EmbedStyle.Error)
                            .Build();

                        await Context.Channel.SendMessageAsync(embed: embed);
                        return;
                    }

                    if (foundCSharpTag.OwnerId != Context.User.Id && !socketGuildUser.GuildPermissions.Administrator)
                    {
                        var embed = new SP1XEmbedBuilder()
                            .WithTitle("Access Denied!")
                            .WithDescription("You need to be a student or administrator to create a C# Tag.")
                            .WithStyle(EmbedStyle.Error)
                            .Build();

                        await Context.Channel.SendMessageAsync(embed: embed);
                        return;
                    }

                    await _DataAccessLayer.EditCSharpTagContent(arguments[1], string.Join(" ", arguments.Skip(2)));

                    var edited = new SP1XEmbedBuilder()
                        .WithTitle("Access Denied!")
                        .WithDescription("You need to be a student or administrator to create a C# Tag.")
                        .WithStyle(EmbedStyle.Error)
                        .Build();
                    await Context.Channel.SendMessageAsync(embed: edited);
                    break;
                case "transfer":
                    var cSharpTagToTransfer = await _DataAccessLayer.GetCSharpTag(arguments[1]);
                    if (cSharpTagToTransfer == null)
                    {
                        var embed = new SP1XEmbedBuilder()
                            .WithTitle("Not Found!")
                            .WithDescription("That C# could not be found.")
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

                    if (cSharpTagToTransfer.OwnerId != this.Context.User.Id &&
                        !socketGuildUser.GuildPermissions.Administrator)
                    {
                        var embed = new SP1XEmbedBuilder()
                            .WithTitle("Access Denied!")
                            .WithDescription(
                                "You need to be the owner of this tag or an administrator to edit the content of this C#.")
                            .WithStyle(EmbedStyle.Error)
                            .Build();

                        await this.Context.Channel.SendMessageAsync(embed: embed);
                        return;
                    }

                    await _DataAccessLayer.EditCSharpTagOwner(arguments[1], userId);

                    var success = new SP1XEmbedBuilder()
                        .WithTitle("C# Ownership Transferred")
                        .WithDescription($"The ownership of the tag has been transferred to <@{userId}>.")
                        .WithStyle(EmbedStyle.Success)
                        .Build();

                    await this.Context.Channel.SendMessageAsync(embed: success);
                    break;
                case "delete":

                    var cSharpTagtoDelete = await _DataAccessLayer.GetCSharpTag(arguments[1]);

                    if (cSharpTagtoDelete == null)
                    {
                        var embed = new SP1XEmbedBuilder()
                            .WithTitle("Not Found!")
                            .WithDescription("that C# Tag could not be found.")
                            .WithStyle(EmbedStyle.Error)
                            .Build();

                        await this.Context.Channel.SendMessageAsync(embed: embed);
                        return;
                    }

                    if (cSharpTagtoDelete.OwnerId != this.Context.User.Id &&
                        !socketGuildUser.GuildPermissions.Administrator)
                    {
                        var embed = new SP1XEmbedBuilder()
                            .WithTitle("Access Denied!")
                            .WithDescription(
                                "You need to be the owner of this tag or an administrator to edit the content of this C# Tag.")
                            .WithStyle(EmbedStyle.Error)
                            .Build();

                        await this.Context.Channel.SendMessageAsync(embed: embed);
                        return;
                    }

                    await _DataAccessLayer.DeleteCSharpTag(arguments[1]);

                    var deleted = new SP1XEmbedBuilder()
                        .WithTitle("C# Tag Deleted!")
                        .WithDescription("The C# Tag was successfully deleted.")
                        .WithStyle(EmbedStyle.Success)
                        .Build();

                    await this.Context.Channel.SendMessageAsync(embed: deleted);
                    break;
            }
        }
    }
}

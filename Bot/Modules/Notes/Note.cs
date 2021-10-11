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

namespace Bot.Modules.Notes
{
    public class Note : SP1XModuleBase
    {
        public Note(IServiceProvider serviceProvider, IConfiguration configuration) : base(serviceProvider,
            configuration)
        {
        }

        [Command("notes", RunMode = RunMode.Async)]
        public async Task GetNotes()
        {
            var notes = await _DataAccessLayer.GetNotes();

            if (!notes.Any())
            {
                var noNotes = new SP1XEmbedBuilder()
                    .WithTitle("No Notes Found")
                    .WithDescription("This server does not have any Notes yet.")
                    .WithStyle(EmbedStyle.Error)
                    .Build();

                await Context.Channel.SendMessageAsync(embed: noNotes);
                return;
            }

            string description = string.Join(", ", notes.Select(x => x.Name));
            var prefix = "!";

            var list = new SP1XEmbedBuilder()
                .WithTitle($"Note ({notes.Count()}")
                .WithDescription(description)
                .WithFooter($"Use \"{prefix} name\" to view a Note.")
                .WithStyle(EmbedStyle.Information)
                .Build();
            await this.Context.Channel.SendMessageAsync(embed: list);
        }

        [Command("note", RunMode = RunMode.Async)]
        public async Task NoteCmd([Remainder] string argument)
        {
            var arguments = argument.Split(" ");

            if (arguments.Count() == 1 && arguments[0] != "create" && arguments[0] != "edit" &&
                arguments[0] != "transfer" && arguments[0] != "delete")
            {
                var note = await _DataAccessLayer.GetNote(arguments[0]);
                if (note is null)
                {
                    var embed = new SP1XEmbedBuilder()
                        .WithTitle("Not Found")
                        .WithDescription("The Note Tag you requested could not be found.")
                        .WithStyle(EmbedStyle.Error)
                        .Build();

                    await Context.Channel.SendMessageAsync(embed: embed);
                    return;
                }

                var noteEmbed = new SP1XEmbedBuilder()
                    .WithTitle(note.Name)
                    .WithDescription(note.Content)
                    .WithStyle(EmbedStyle.Information)
                    .Build();

                await Context.Channel.SendMessageAsync(embed: noteEmbed);
                return;
            }

            var socketGuildUser = Context.User as SocketGuildUser;

            switch (arguments[0])
            {
                case "create":
                    var note = await _DataAccessLayer.GetNote(arguments[1]);
                    if (note != null)
                    {
                        var embed = new SP1XEmbedBuilder()
                            .WithTitle("Already Exists")
                            .WithDescription("There already exists a Note with that name.")
                            .WithStyle(EmbedStyle.Error)
                            .Build();

                        await Context.Channel.SendMessageAsync(embed: embed);
                        return;
                    }

                    if (!Context.User.IsPromoted())
                    {
                        var embed = new SP1XEmbedBuilder()
                            .WithTitle("Access Denied!")
                            .WithDescription("You need to be a student or administrator to create a Note.")
                            .WithStyle(EmbedStyle.Error)
                            .Build();

                        await Context.Channel.SendMessageAsync(embed: embed);
                        return;
                    }

                    await _DataAccessLayer.CreateNote(arguments[1], Context.User.Id,
                        string.Join(" ", arguments.Skip(2)));

                    var prefix = await _DataAccessLayer.GetGuildPrefix(Context.Guild.Id);

                    var created = new SP1XEmbedBuilder()
                        .WithTitle("Note Created!")
                        .WithDescription(
                            $"The Note has been successfully created. You can view it by using `{prefix}note {arguments[1]}`")
                        .WithStyle(EmbedStyle.Success)
                        .Build();
                    await Context.Channel.SendMessageAsync(embed: created);
                    break;
                case "edit":
                    var foundNote = await _DataAccessLayer.GetNote(arguments[1]);
                    if (foundNote == null)
                    {
                        var embed = new SP1XEmbedBuilder()
                            .WithTitle("Not Found")
                            .WithDescription("The Note you requested could not be found.")
                            .WithStyle(EmbedStyle.Error)
                            .Build();

                        await Context.Channel.SendMessageAsync(embed: embed);
                        return;
                    }

                    if (foundNote.OwnerId != Context.User.Id && !socketGuildUser.GuildPermissions.Administrator)
                    {
                        var embed = new SP1XEmbedBuilder()
                            .WithTitle("Access Denied!")
                            .WithDescription("You need to be a student or administrator to create a Note.")
                            .WithStyle(EmbedStyle.Error)
                            .Build();

                        await Context.Channel.SendMessageAsync(embed: embed);
                        return;
                    }

                    await _DataAccessLayer.EditNoteContent(arguments[1], string.Join(" ", arguments.Skip(2)));

                    var edited = new SP1XEmbedBuilder()
                        .WithTitle("Access Denied!")
                        .WithDescription("You need to be a student or administrator to create a Note.")
                        .WithStyle(EmbedStyle.Error)
                        .Build();
                    await Context.Channel.SendMessageAsync(embed: edited);
                    break;
                case "transfer":
                    var noteToTransfer = await _DataAccessLayer.GetNote(arguments[1]);
                    if (noteToTransfer == null)
                    {
                        var embed = new SP1XEmbedBuilder()
                            .WithTitle("Not Found!")
                            .WithDescription("That Note could not be found.")
                            .WithStyle(EmbedStyle.Error)
                            .Build();

                        await Context.Channel.SendMessageAsync(embed: embed);
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

                    if (noteToTransfer.OwnerId != Context.User.Id &&
                        !socketGuildUser.GuildPermissions.Administrator)
                    {
                        var embed = new SP1XEmbedBuilder()
                            .WithTitle("Access Denied!")
                            .WithDescription(
                                "You need to be the owner of this tag or an administrator to edit the content of this Note.")
                            .WithStyle(EmbedStyle.Error)
                            .Build();

                        await Context.Channel.SendMessageAsync(embed: embed);
                        return;
                    }

                    await _DataAccessLayer.EditNoteOwner(arguments[1], userId);

                    var success = new SP1XEmbedBuilder()
                        .WithTitle("Note Ownership Transferred")
                        .WithDescription($"The ownership of the tag has been transferred to <@{userId}>.")
                        .WithStyle(EmbedStyle.Success)
                        .Build();

                    await Context.Channel.SendMessageAsync(embed: success);
                    break;
                case "delete":

                    var notetoDelete = await _DataAccessLayer.GetNote(arguments[1]);

                    if (notetoDelete == null)
                    {
                        var embed = new SP1XEmbedBuilder()
                            .WithTitle("Not Found!")
                            .WithDescription("That Note could not be found.")
                            .WithStyle(EmbedStyle.Error)
                            .Build();

                        await Context.Channel.SendMessageAsync(embed: embed);
                        return;
                    }

                    if (notetoDelete.OwnerId != this.Context.User.Id &&
                        !socketGuildUser.GuildPermissions.Administrator)
                    {
                        var embed = new SP1XEmbedBuilder()
                            .WithTitle("Access Denied!")
                            .WithDescription(
                                "You need to be the owner of this tag or an administrator to delete the content of this Note.")
                            .WithStyle(EmbedStyle.Error)
                            .Build();

                        await this.Context.Channel.SendMessageAsync(embed: embed);
                        return;
                    }

                    await _DataAccessLayer.DeleteNote(arguments[1]);

                    var deleted = new SP1XEmbedBuilder()
                        .WithTitle("Note Deleted!")
                        .WithDescription("The Note was successfully deleted.")
                        .WithStyle(EmbedStyle.Success)
                        .Build();

                    await this.Context.Channel.SendMessageAsync(embed: deleted);
                    break;
            }
        }
    }
}

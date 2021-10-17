using Bot.Common;
using Data;
using Discord;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Utilities
{
    public class TimeTableManager
    {
        private readonly DataAccessLayer _dataAccessLayer;

        public TimeTableManager(DataAccessLayer dataAccessLayer)
        {
            _dataAccessLayer = dataAccessLayer;
        }

        public async Task PostWeeklyTimeTableEmbed(IGuild guild, ulong channelId)
        {
            var channelToPost = await guild.GetTextChannelAsync(channelId);
            if (channelToPost == null)
            {
                return;
            }
            
            EmbedBuilder timetableEmbed = new EmbedBuilder()
                .WithColor(Colors.Success)
                .WithTitle("SD1X Weekly Timetable");

            var days = new List<string> { DaysOfWeek.Monday, DaysOfWeek.Tuesday, DaysOfWeek.Wednesday, DaysOfWeek.Thursday, DaysOfWeek.Friday };
            foreach (var day in days)
            {
                var timetableEmbedField = new EmbedFieldBuilder()
                    .WithName($"{day}");

                var timetables = await _dataAccessLayer.GetTimeTableDay(guild.Id, day);

                StringBuilder sb = new StringBuilder();
                foreach (var timetable in timetables)
                {
                    sb.Append($"```css \n[{timetable.Subject}] \n [{timetable.Location} \n [{timetable.Time}]``` \n");
                }
                timetableEmbedField.WithValue(sb.ToString());
                timetableEmbedField.WithIsInline(true);
                timetableEmbed.AddField(timetableEmbedField);
            }
            
            await channelToPost.SendMessageAsync(null, false, timetableEmbed.Build());
        }

        public async Task PostDailyTimeTableEmbed(IGuild guild, ulong channelId, string day)
        {   
            var channelToPost = await guild.GetTextChannelAsync(channelId);
            if (channelToPost == null)
            {
                return;
            }

            EmbedBuilder embed = new EmbedBuilder()
                .WithColor(Colors.Success)
                .WithTitle($"SD1X Daily Timetable For: {day}");

            StringBuilder stringBuilder = new StringBuilder();

            var timetables = await _dataAccessLayer.GetTimeTableDay(guild.Id, day);
            foreach (var timetable in timetables)
            {
                stringBuilder.Append($"```css \n[{ timetable.Subject}] \n[{ timetable.Location} \n[{ timetable.Time}]``` \n");
            }
            embed.WithDescription($"{stringBuilder}");

            await channelToPost.SendMessageAsync(null, false, embed.Build());
        }

        public async Task CreateNewTimeTableEntry(IGuild guild, ulong channelId, string day, string subject, string location, string time)
        {
            var channelToPost = await guild.GetTextChannelAsync(channelId);
            if (channelToPost == null)
            {
                return;
            }
            var checkTimetable = await _dataAccessLayer.GetTimeTableDayTime(guild.Id, day, time);
            if (checkTimetable != null)
            {
                var errorEmbed = new SP1XEmbedBuilder()
                .WithTitle($"Error! Timetable alredy exists.")
                .WithDescription($"```css \n[ERROR!] \n[Timetable already exists for Timetable: {day}, {time}]```")
                .WithStyle(EmbedStyle.Error)
                .Build();

                await channelToPost.SendMessageAsync(embed: errorEmbed);
            }

            await _dataAccessLayer.CreateTimeTable(guild.Id, day, subject, time, location);

            var embed = new SP1XEmbedBuilder()
                .WithTitle($"New timetable added for: {day}")
                .WithDescription($"```css \n[{subject}] \n[{location} \n[{time}]```")
                .WithStyle(EmbedStyle.Success)
                .Build();

            await channelToPost.SendMessageAsync(embed: embed);
        }

        public async Task DeleteTimeTableEntry(IGuild guild, ulong channelId, string day, string time)
        {
            var channelToPost = await guild.GetTextChannelAsync(channelId);
            if (channelToPost == null)
            {
                return;
            }
            var checkTimetable = await _dataAccessLayer.GetTimeTableDayTime(guild.Id, day, time);
            if (checkTimetable == null)
            {
                var errorEmbed = new SP1XEmbedBuilder()
                .WithTitle($"Error! Timetable does not exist.")
                .WithDescription($"```css \n[ERROR!] \n[No Timetable exists for Timetable: {day}, {time}]```")
                .WithStyle(EmbedStyle.Error)
                .Build();

                await channelToPost.SendMessageAsync(embed: errorEmbed);
            }

            await _dataAccessLayer.DeleteTimeTable(guild.Id, day, time);

            var embed = new SP1XEmbedBuilder()
                .WithTitle($"Success! Timetable deleted!")
                .WithDescription($"The timetable was successfully deleted for Timetable: `{day}, {time}`")
                .WithStyle(EmbedStyle.Success)
                .Build();

            await channelToPost.SendMessageAsync(embed: embed);
        }
    }
}

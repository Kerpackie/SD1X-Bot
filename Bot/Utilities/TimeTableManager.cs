using Bot.Common;
using Data;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task GetWeeklyTimeTableEmbed(IGuild guild)
        {
            EmbedBuilder timetableEmbed = new EmbedBuilder()
                .WithColor(Colors.Success)
                .WithTitle("SD1X Weekly Timetable");

            var days = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
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
                timetableEmbedField.Build();
            }
            timetableEmbed.Build();
        }

    }
}

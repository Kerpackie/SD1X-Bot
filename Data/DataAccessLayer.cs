using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Data.Context;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class DataAccessLayer
    {
        private readonly SP1XDbContext _dbContext;

        public DataAccessLayer(SP1XDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Assignment> GetAssignment(string subject, string name)
        {
            return await _dbContext.Assignments
                .FirstOrDefaultAsync(x => x.Name == name && x.Subject == subject);
        }

        public async Task<Assignment> GetAssignmentId(int id)
        {
            return await _dbContext.Assignments
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Assignment> GetAssignmentSubject(string subject, string name)
        {
            return await _dbContext.Assignments
                .FirstOrDefaultAsync(x => x.Name == name && x.Subject == subject);
        }

        public async Task<IEnumerable<Assignment>> GetAssignments()
        {
            return await _dbContext.Assignments
                .ToListAsync();
        }

        public async Task<IEnumerable<Assignment>> GetAssignmentsSubject(string subject)
        {
            return await _dbContext.Assignments
                .Where(s => s.Subject == subject)
                .ToListAsync();
        }

        public async Task CreateAssignment(string subject, ulong ownerId, string name, string content)
        {
            //var assignment = await _dbContext.Assignments
            //    .FirstOrDefaultAsync(x => x.MessageId == messageId);

            //if (messageId != 0)
            //{
            //    return;
            //}

            _dbContext.Add(new Assignment
            {
                Subject = subject,
                Name = name,
                OwnerId = ownerId,
                Content = content,
            });

            await _dbContext.SaveChangesAsync();
        }

        public async Task EditAssignmentContent(int id, string content)
        {
            var assignment = await _dbContext.Assignments
                .FirstOrDefaultAsync(x => x.Id == id);

            if (assignment is null)
            {
                return;
            }

            assignment.Content = content;
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditAssignmentContentSubject(string subject, string name, string newName, string content)
        {
            var assignment = await _dbContext.Assignments
                .Where(x => x.Subject == subject && x.Name == name)
                .FirstOrDefaultAsync();
            if (assignment is null)
            {
                return;
            }

            assignment.Name = newName;
            assignment.Content = content;
            await _dbContext.SaveChangesAsync();
        }

        //public async Task EditAssignmentContentSubject(string subject, string name, string content)
        //{
        //    var assignment = await _dbContext.Assignments
        //        .Where(x => x.Subject == subject && x.Name == name)
        //        .FirstOrDefaultAsync();
          
        //    if (assignment is null)
        //    {
        //        return;
        //    }

        //    assignment.Content = content;
        //    await _dbContext.SaveChangesAsync();
        //}

        public async Task EditAssignmentOwner(int id, ulong ownerId)
        {
            var assignment = await _dbContext.Assignments
                .FirstOrDefaultAsync(x => x.Id == id);

            if (assignment is null)
            {
                return;
            }

            assignment.OwnerId = ownerId;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAssignment(int id)
        {
            var assignment = await _dbContext.Assignments
                .FirstOrDefaultAsync(x => x.Id == id);

            if (assignment is null)
            {
                return;
            }

            _dbContext.Remove(assignment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAssignmentWithSubjectName(string subject, string name)
        {
            var assignment = await _dbContext.Assignments
                .FirstOrDefaultAsync(x => x.Subject == subject && x.Name == name);

            if (assignment is null)
            {
                return;
            }

            _dbContext.Remove(assignment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<AssignmentChannel> GetAssignmentChannel(ulong channelId)
        {
            return await _dbContext.AssignmentChannels
                .FirstOrDefaultAsync(x => x.Channel == channelId);
        }

        public async Task<IEnumerable<AssignmentChannel>> GetAssignmentChannels(ulong guildId)
        {
            return await _dbContext.AssignmentChannels
                .Where(x => x.GuildId == guildId)
                .ToListAsync();
        }

        public async Task CreateAssignmentChannel(ulong channelId, ulong guildId)
        {
            var channel = await _dbContext.AssignmentChannels
                .FirstOrDefaultAsync(x => x.Channel == channelId);

            if (channel != null)
            {
                return;
            }

            _dbContext.Add(new AssignmentChannel
            {
                Channel = channelId,
                GuildId = guildId
            });

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAssignmentChannelUlong(ulong channelId)
        {
            var assignment = await _dbContext.AssignmentChannels
                .FirstOrDefaultAsync(x => x.Channel == channelId);

            if (assignment is null)
            {
                return;
            }

            _dbContext.Remove(assignment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAssignmentChannelId(int id)
        {
            var assignment = await _dbContext.AssignmentChannels
                .FirstOrDefaultAsync(x => x.Id == id);

            if (assignment is null)
            {
                return;
            }

            _dbContext.Remove(assignment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<CSharpTag> GetCSharpTag(string tag)
        {
            return await _dbContext.CSharpTags
                .FirstOrDefaultAsync(x => x.Tag == tag);
        }

        public async Task<IEnumerable<CSharpTag>> GetCSharpTags()
        {
            return await _dbContext.CSharpTags
                .ToListAsync();
        }

        public async Task CreateCSharpTag(string tagName, ulong ownerId, string content)
        {
            var tag = await _dbContext.CSharpTags.FirstOrDefaultAsync(x => x.Tag == tagName);

            if (tag != null)
            {
                return;
            }

            _dbContext.Add(new CSharpTag
            {
                Tag = tagName,
                OwnerId = ownerId,
                Content = content,
            });

            await _dbContext.SaveChangesAsync();
        }

        public async Task EditCSharpTagContent(string tagName, string content)
        {
            var tag = await _dbContext.CSharpTags
                .FirstOrDefaultAsync(x => x.Tag == tagName);

            if (tag is null)
            {
                return;
            }

            tag.Content = content;
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditCSharpTagOwner(string tagName, ulong ownerId)
        {
            var tag = await _dbContext.CSharpTags
                .FirstOrDefaultAsync(x => x.Tag == tagName);

            if (tag is null)
            {
                return;
            }

            tag.OwnerId = ownerId;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCSharpTag(string tagName)
        {
            var tag = await _dbContext.CSharpTags
                .FirstOrDefaultAsync(x => x.Tag == tagName);

            if (tag is null)
            {
                return;
            }

            _dbContext.Remove(tag);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<CSSTag> GetCSSTag(string tag)
        {
            return await _dbContext.CSSTags
                .FirstOrDefaultAsync(x => x.Tag == tag);
        }

        public async Task<IEnumerable<CSSTag>> GetCSSTags()
        {
            return await _dbContext.CSSTags
                .ToListAsync();
        }

        public async Task CreateCSSTag(string tagName, ulong ownerId, string content)
        {
            var tag = await _dbContext.CSSTags
                .FirstOrDefaultAsync(x => x.Tag == tagName);

            if (tag != null)
            {
                return;
            }

            _dbContext.Add(new CSSTag
            {
                Tag = tagName,
                OwnerId = ownerId,
                Content = content,
            });

            await _dbContext.SaveChangesAsync();
        }

        public async Task EditCSSTagContent(string tagName, string content)
        {
            var tag = await _dbContext.CSSTags
                .FirstOrDefaultAsync(x => x.Tag == tagName);

            if (tag is null)
            {
                return;
            }

            tag.Content = content;
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditCSSTagOwner(string tagName, ulong ownerId)
        {
            var tag = await _dbContext.CSSTags
                .FirstOrDefaultAsync(x => x.Tag == tagName);

            if (tag is null)
            {
                return;
            }

            tag.OwnerId = ownerId;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCSSTag(string tagName)
        {
            var tag = await _dbContext.CSSTags
                .FirstOrDefaultAsync(x => x.Tag == tagName);

            if (tag is null)
            {
                return;
            }

            _dbContext.Remove(tag);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<HTMLTag> GetHTMLTag(string tag)
        {
            return await _dbContext.HTMLTags
                .FirstOrDefaultAsync(x => x.Tag == tag);
        }

        public async Task<IEnumerable<HTMLTag>> GetHTMLTags()
        {
            return await _dbContext.HTMLTags
                .ToListAsync();
        }

        public async Task CreateHTMLTag(string tagName, ulong ownerId, string content)
        {
            var tag = await _dbContext.HTMLTags
                .FirstOrDefaultAsync(x => x.Tag == tagName);

            if (tag != null)
            {
                return;
            }

            _dbContext.Add(new HTMLTag
            {
                Tag = tagName,
                OwnerId = ownerId,
                Content = content,
            });

            await _dbContext.SaveChangesAsync();
        }

        public async Task EditHTMLTagContent(string tagName, string content)
        {
            var tag = await _dbContext.HTMLTags
                .FirstOrDefaultAsync(x => x.Tag == tagName);

            if (tag is null)
            {
                return;
            }

            tag.Content = content;
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditHTMLTagOwner(string tagName, ulong ownerId)
        {
            var tag = await _dbContext.HTMLTags
                .FirstOrDefaultAsync(x => x.Tag == tagName);

            if (tag is null)
            {
                return;
            }

            tag.OwnerId = ownerId;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteHTMLTag(string tagName)
        {
            var tag = await _dbContext.HTMLTags
                .FirstOrDefaultAsync(x => x.Tag == tagName);

            if (tag is null)
            {
                return;
            }

            _dbContext.Remove(tag);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Notes> GetNote(string name)
        {
            return await _dbContext.Notes
                .FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<IEnumerable<Notes>> GetNotes()
        {
            return await _dbContext.Notes
                .ToListAsync();
        }

        public async Task CreateNote(string name, ulong ownerId, string content)
        {
            var note = await _dbContext.Notes
                .FirstOrDefaultAsync(x => x.Name == name);

            if (note != null)
            {
                return;
            }

            _dbContext.Add(new Notes
            {
                Name = name,
                OwnerId = ownerId,
                Content = content,
            });

            await _dbContext.SaveChangesAsync();
        }

        public async Task EditNoteContent(string name, string content)
        {
            var note = await _dbContext.Notes
                .FirstOrDefaultAsync(x => x.Name == name);

            if (note is null)
            {
                return;
            }

            note.Content = content;
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditNoteOwner(string name, ulong ownerId)
        {
            var note = await _dbContext.Notes
                .FirstOrDefaultAsync(x => x.Name == name);

            if (note is null)
            {
                return;
            }

            note.OwnerId = ownerId;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteNote(string name)
        {
            var note = await _dbContext.Notes
                .FirstOrDefaultAsync(x => x.Name == name);

            if (note is null)
            {
                return;
            }

            _dbContext.Remove(note);
            await _dbContext.SaveChangesAsync();
        }

        public async Task ModifyGuildPrefix(ulong id, string prefix)
        {
            var server = await _dbContext.Servers
                .FindAsync(id);

            if (server == null)
            {
                _dbContext.Add(new Server {Id = id, Prefix = prefix});
            }
            else
            {
                server.Prefix = prefix;
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task<string> GetGuildPrefix(ulong id)
        {
            var prefix = await _dbContext.Servers
                .Where(x => x.Id == id)
                .Select(x => x.Prefix)
                .FirstOrDefaultAsync();

            return await Task.FromResult(prefix);
        }
    }
}
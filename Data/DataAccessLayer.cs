using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Data.Context;
using Data.Models;

namespace Data
{
    public class DataAccessLayer
    {
        private readonly SP1XDbContext _dbContext;

        public DataAccessLayer(SP1XDbContext dbContext)
        {
            _dbContext = dbContext;
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
            var tag = await _dbContext.CSharpTags
                .FirstOrDefaultAsync(x => x.Tag == tagName);

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

            if (tagName is null)
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

            if (tagName is null)
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

            if (tagName is null)
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

        public async Task<Notes> GetNote(string tag)
        {
            return await _dbContext.Notes
                .FirstOrDefaultAsync(x => x.Tag == tag);
        }

        public async Task<IEnumerable<Notes>> GetNotes()
        {
            return await _dbContext.Notes
                .ToListAsync();
        }

        public async Task CreateNote(string tagName, ulong ownerId, string content)
        {
            var tag = await _dbContext.Notes
                .FirstOrDefaultAsync(x => x.Tag == tagName);

            if (tag != null)
            {
                return;
            }

            _dbContext.Add(new Notes
            {
                Tag = tagName,
                OwnerId = ownerId,
                Content = content,
            });

            await _dbContext.SaveChangesAsync();
        }

        public async Task EditNoteContent(string tagName, string content)
        {
            var tag = await _dbContext.Notes
                .FirstOrDefaultAsync(x => x.Tag == tagName);

            if (tagName is null)
            {
                return;
            }

            tag.Content = content;
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditNoteOwner(string tagName, ulong ownerId)
        {
            var tag = await _dbContext.Notes
                .FirstOrDefaultAsync(x => x.Tag == tagName);

            if (tag is null)
            {
                return;
            }

            tag.OwnerId = ownerId;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteNote(string tagName)
        {
            var tag = await _dbContext.Notes
                .FirstOrDefaultAsync(x => x.Tag == tagName);

            if (tag is null)
            {
                return;
            }

            _dbContext.Remove(tag);
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
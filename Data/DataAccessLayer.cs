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

        public async Task<CSharpTagModel> GetCSharpTag(string tag)
        {
            return await _dbContext.CSharpTags
                .FirstOrDefaultAsync(x => x.Tag == tag);
        }
        public async Task<IEnumerable<CSharpTagModel>> GetCSharpTags()
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

            _dbContext.Add(new CSharpTagModel
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

        public async Task EditCSharpTaskOwner(string tagName, ulong ownerId)
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

        public async Task DeleteCSharpTask(string tagName)
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
    }
}
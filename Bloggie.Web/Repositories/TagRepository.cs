using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BloggieDbContext _bloggieDbContext;

        public TagRepository(BloggieDbContext bloggieDbContext)
        {
            this._bloggieDbContext = bloggieDbContext;
        }
        public async Task<Tag> AddAsync(Tag tag)
        {
            await _bloggieDbContext.Tags.AddAsync(tag);
            await _bloggieDbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await _bloggieDbContext.Tags.ToListAsync();
        }

        public Task<Tag?> GetAsync(Guid Id)
        {
            return _bloggieDbContext.Tags.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await _bloggieDbContext.Tags.FindAsync(tag.Id);

            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                await _bloggieDbContext.SaveChangesAsync();

                return existingTag;
            }
            return null;
        }

        public async Task<Tag?> DeleteAsync(Guid Id)
        {
            var ExistingTag = await _bloggieDbContext.Tags.FindAsync(Id);
            if (ExistingTag != null)
            {
                _bloggieDbContext.Tags.Remove(ExistingTag);
                await _bloggieDbContext.SaveChangesAsync();

                return ExistingTag;
            }
            return null;
        }
    }
}

using Bloggie.Web.Data;
using Bloggie.Web.Models.Domain;
using Bloggie.Web.Models.ViewModels;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bloggie.Web.Controllers
{
    public class AdminTagsController : Controller
    {
        private readonly ITagRepository _tagRepository;

        public AdminTagsController(ITagRepository tagRepository)
        {
            this._tagRepository = tagRepository;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitTag(AddTagRequest _addTagRequest)
        {
            var tag = new Tag
            {
                Name = _addTagRequest.Name,
                DisplayName = _addTagRequest.DisplayName,
            };

            await _tagRepository.AddAsync(tag);

            //await _bloggieDbContext.Tags.AddAsync(tag);
            //await _bloggieDbContext.SaveChangesAsync();
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            // var tag = await _bloggieDbContext.Tags.ToListAsync();
            var tags = await _tagRepository.GetAllAsync();

            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> ViewTag(Guid Id)
        {
            //var tag = await _bloggieDbContext.Tags.FirstOrDefaultAsync(x => x.Id == Id);
            var tag = await _tagRepository.GetAsync(Id);
            if (tag != null)
            {
                var _editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName,
                };
                return View(_editTagRequest);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> EditTag(EditTagRequest _editTagRequest)
        {
            var tag = new Tag
            {
                Id = _editTagRequest.Id,
                Name = _editTagRequest.Name,
                DisplayName = _editTagRequest.DisplayName,
            };
          

            var updatedTag = await _tagRepository.UpdateAsync(tag);

            if (updatedTag != null)
            {
                //show success notification
            }
            else
            {
                //show error notification
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTag(EditTagRequest _editTagRequest)
        {
            //var tag = await _bloggieDbContext.Tags.FindAsync(_editTagRequest.Id);
            //if (tag != null)
            //{
            //    _bloggieDbContext.Tags.Remove(tag);
            //    await _bloggieDbContext.SaveChangesAsync();

            //    return RedirectToAction("List");
            //}

            var deletedTag = await _tagRepository.DeleteAsync(_editTagRequest.Id);
            if (deletedTag != null)
            {
                return RedirectToAction("List");
            }

            return RedirectToAction("EditTag", new { Id = _editTagRequest.Id });
        }
    }
}

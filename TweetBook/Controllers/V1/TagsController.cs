using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TweetBook.Contracts.V1.Request;
using TweetBook.Contracts.V1.Response;
using TweetBook.Domain;
using TweetBook.Services;
using static TweetBook.Contracts.V1.ApiRoutes;

namespace TweetBook.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TagsController : Controller
    {
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;

        public TagsController(ITagService tagService, IMapper mapper)
        {
            _tagService = tagService;
            _mapper = mapper;
        }

        [HttpGet(Tags.GetTags)]
        public async Task<IActionResult> GetTags()
        {
            var tags = await _tagService.GetTagsAsync();
            var getTagsResponse = _mapper.Map<IEnumerable<GetTagResponse>>(tags);
            return Ok(getTagsResponse);
        }

        [HttpPost(Tags.CreateTag)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateTagRequest createTag)
        {
            var tag = _mapper.Map<Tag>(createTag);
            if (await _tagService.CreateTagAsync(tag))
            {
                return Ok("Tag created successfully");
            }
            return StatusCode(500, new { Error = "Unable to create tag at this moment, please try again later" });
        }

        [HttpDelete(Tags.DeleteTag)]
        [Authorize(Policy = Constants.TweetBookConstants.MustOnlyWorkForTestDomainPolicy)]
        public async Task<IActionResult> Delete([FromRoute] Guid tagId)
        {
            if (await _tagService.DeleteTagAsync(tagId))
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}

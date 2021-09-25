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
    [Produces("application/json")]
    public class TagsController : Controller
    {
        private readonly ITagService _tagService;
        private readonly IMapper _mapper;

        public TagsController(ITagService tagService, IMapper mapper)
        {
            _tagService = tagService;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns all the tags in the system
        /// </summary>
        /// <returns>All tags</returns>
        /// <response code = "200">Gets all the tags in the system</response>
        [HttpGet(Tags.GetTags)]
        [ProducesResponseType(typeof(IEnumerable<TagResponse>), 200)]
        public async Task<IActionResult> GetTags()
        {
            var tags = await _tagService.GetTagsAsync();
            var getTagsResponse = _mapper.Map<IEnumerable<TagResponse>>(tags);
            return Ok(getTagsResponse);
        }

        /// <summary>
        /// Creates the tag in the system
        /// </summary>
        /// <param name="createTag">Create tag request</param>
        /// <returns>Recently created tag</returns>
        /// <response code = "200">Creates the tag</response>
        /// <response code = "400">Error while creating tag</response>
        [HttpPost(Tags.CreateTag)]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(CreateTagResponse), 200)]
        [ProducesResponseType(typeof(ErrorResponse),400)]
        public async Task<IActionResult> Create([FromBody] CreateTagRequest createTag)
        {
            var tag = _mapper.Map<Tag>(createTag);
            if (await _tagService.CreateTagAsync(tag))
            {
                return Ok("Tag created successfully");
            }
            var error = new ErrorResponse
            {
                Errors = new List<ErrorModel> 
                {
                    new ErrorModel 
                    {
                        Message = "Unable to create tag at this moment, please try again later"
                    }
                }
            };
            return BadRequest(error);
        }

        [HttpDelete(Tags.DeleteTag)]
        [Authorize(Policy = Constants.TweetBookConstants.MustOnlyWorkForTestDomainPolicy)]
        [ProducesResponseType(204)]
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

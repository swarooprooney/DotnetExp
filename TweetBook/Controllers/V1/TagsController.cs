﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TweetBook.Contracts.V1.Request;
using TweetBook.Domain;
using TweetBook.Services;
using static TweetBook.Contracts.V1.ApiRoutes;

namespace TweetBook.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TagsController : Controller
    {
        private readonly ITagService _tagService;
        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet(Tags.GetTags)]
        public async Task<IActionResult> GetTags()
        {
           return Ok(await _tagService.GetTagsAsync());
        }

        [HttpPost(Tags.CreateTag)]
        [Authorize(Policy = "TagCreator")]
        public async Task<IActionResult> Create([FromBody] CreateTagRequest createTag)
        {
            var tag = new Tag { TagName = createTag.Name };
            if (await _tagService.CreateTagAsync(tag))
            {
                return Ok("Tag created successfully");
            }
            return StatusCode(500, new { Error = "Unable to create tag at this moment, please try again later" });
        }
    }
}

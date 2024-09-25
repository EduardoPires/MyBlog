﻿using Microsoft.AspNetCore.Mvc;
using MyBlog.Domain.Services;

namespace MyBlog.Web.Mvc.ViewComponents
{
    public class MostViewedPostsWidgetViewComponent(IPostService postService) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var posts = await postService.GetMostViewedPostsAsync();
            return View(posts);
        }
    }
}
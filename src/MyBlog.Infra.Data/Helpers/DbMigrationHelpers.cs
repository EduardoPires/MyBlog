﻿using Bogus;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyBlog.Domain.Entities;
using MyBlog.Infra.Data.Context;
using MyBlog.Infra.Identity;

namespace MyBlog.Infra.Data.Helpers
{
    internal static class DbMigrationHelpers
    {
        private static List<User>? _users;
        private static List<Author>? _authors;
        private static List<Post>? _posts;
        private const string _locale = "pt_BR";

        public static async Task EnsureSeedDataAsync(WebApplication serviceScope)
        {
            var services = serviceScope.Services.CreateScope().ServiceProvider;
            await EnsureSeedDataAsync(services);
        }

        private static async Task EnsureSeedDataAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>()
                                             .CreateScope();
            var env = scope.ServiceProvider.GetRequiredService<IHostEnvironment>();

            if (env.IsDevelopment())
            {
                var context = scope.ServiceProvider.GetRequiredService<MyBlogDbContext>();

                await context.Database.MigrateAsync();
                await SeedAspNetIdentity(context, scope.ServiceProvider);
                await SeedDataAsync(context);
            }
        }

        private static async Task SeedDataAsync(MyBlogDbContext context)
        {
            await SeedUsersAsync(context);
            await SeedAuthorsAsync(context);
            await SeedPostsAsync(context);
            await SeedCommentsAsync(context);
        }

        private static async Task SeedPostsAsync(MyBlogDbContext context)
        {
            if (await context.Posts.AnyAsync())
            {
                return;
            }

            var seedCount = 40;

            var postFaker = new Faker<Post>(_locale)
              .RuleFor(c => c.Title, f => f.Hacker.Phrase())
              .RuleFor(c => c.Summary, f => f.Lorem.Sentence(20))
              .RuleFor(c => c.Content, f => f.Lorem.Sentence(f.Random.Number(100, 300)))
              .RuleFor(c => c.ViewCount, f => f.Random.Number(0, 1_000))
              .RuleFor(c => c.IsActive, f => true)
              .RuleFor(c => c.PublishDate, f => f.Date.Past())
              .RuleFor(c => c.AuthorId, f => f.PickRandom(_authors).Id);

            _posts = postFaker.Generate(seedCount);
            await context.Posts.AddRangeAsync(_posts);
            await context.SaveChangesAsync();
        }

        private static async Task SeedCommentsAsync(MyBlogDbContext context)
        {
            if (await context.Comments.AnyAsync())
            {
                return;
            }

            var seedCount = 100;

            var commentFaker = new Faker<Comment>(_locale)
              .RuleFor(c => c.Content, f => f.Rant.Review())
              .RuleFor(c => c.IsActive, f => true)
              .RuleFor(c => c.PostId, f => f.PickRandom(_posts).Id)
              .RuleFor(c => c.UserId, f => f.PickRandom(_users).Id);

            var comments = commentFaker.Generate(seedCount);
            await context.Comments.AddRangeAsync(comments);
            await context.SaveChangesAsync();
        }

        private static async Task SeedUsersAsync(MyBlogDbContext context)
        {
            if (await context.Users.CountAsync() > 1)
            {
                return;
            }

            var seedCount = 15;

            var userFaker = new Faker<ApplicationUser>(_locale)
              .RuleFor(c => c.FullName, f => f.Name.FullName())
              .RuleFor(c => c.Email, f => f.Internet.Email())
              .RuleFor(c => c.PasswordHash, f => f.Random.AlphaNumeric(50));

            var appUsers = userFaker.Generate(seedCount);

            appUsers.ForEach(u => u.UserName = u.Email);

            await context.Users.AddRangeAsync(appUsers);

            _users = appUsers.Select(u => new User
            {
                FullName = u.FullName,
                Id = Guid.Parse(u.Id)
            }).ToList();

            await context.BlogUsers.AddRangeAsync(_users);
            await context.SaveChangesAsync();
        }

        private static async Task SeedAuthorsAsync(MyBlogDbContext context)
        {
            if (await context.Authors.AnyAsync())
            {
                return;
            }

            var seedCount = _users!.Count;

            var authorFaker = new Faker<Author>(_locale)
              //.RuleFor(c => c.Bio, f => f.Name.JobTitle())
              //.RuleFor(c => c.Slug, f => f.Lorem.Slug())
              ;

            _authors = authorFaker.Generate(seedCount);

            for (int i = 0; i < seedCount; i++)
            {
                _authors[i].UserId = _users[i]!.Id;
            }

            await context.Authors.AddRangeAsync(_authors);
            await context.SaveChangesAsync();
        }

        private static async Task SeedAspNetIdentity(MyBlogDbContext context, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (await userManager.Users.AnyAsync())
            {
                return;
            }

            var adminUser = new ApplicationUser
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                FullName = "Default Admin User"
            };

            var password = $"Admin123!";
            var result = await userManager.CreateAsync(adminUser, password);

            if (result.Succeeded)
            {
                await userManager.ConfirmEmailAsync(adminUser, await userManager.GenerateEmailConfirmationTokenAsync(adminUser));
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }

            await context.BlogUsers.AddAsync(new User
            {
                FullName = adminUser.FullName,
                Id = Guid.Parse(adminUser.Id)
            });

            await context.SaveChangesAsync();            
        }
    }
}
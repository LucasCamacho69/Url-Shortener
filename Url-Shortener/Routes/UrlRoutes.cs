using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using redirect.Data.Models;
using redirect.Models;


namespace redirect.Routes
{
    public static class UrlRoute
    {
        public static void UrlRoutes(this WebApplication app)
        {
            var route = app.MapGroup("url");

            route.MapPost("", async (UrlModel req, DataContext context) =>
            {
                try
                {
                    var url = new UrlModel(
                        req.Url,
                        req.ShortCode
                    );

                    await context.AddAsync(url);
                    await context.SaveChangesAsync();
                    return Results.Ok($"Url shortened! http://localhost:5224/url/shorten/{url.ShortCode}");
                }
                catch (Exception ex)
                {
                    return Results.InternalServerError(ex.Message);
                }
            });

            route.MapGet("", async (DataContext context) =>
            {
                try
                {
                    var allUrl = await context.Url.ToListAsync();
                    if (allUrl == null)
                    {
                        return Results.InternalServerError();
                    }
                    return Results.Ok(allUrl);
                }
                catch (Exception ex)
                {
                    return Results.InternalServerError(ex.Message);
                }
            });

            route.MapGet("shorten/{shortcode:}", async (string shortcode, DataContext context) =>
            {
                try
                {
                    var url = await context.Url.FirstOrDefaultAsync(x => x.ShortCode == shortcode);
                    if (url == null)
                    {
                        return Results.NotFound($"No url with shortcode: {shortcode}");
                    }
                    url.Counter();
                    await context.SaveChangesAsync();
                    return Results.Redirect(url.Url);
                }
                catch (Exception ex)
                {
                    return Results.InternalServerError(ex.Message);
                }
            });
            route.MapGet("{shortcode:}/stats", async (string shortcode, DataContext context) =>
            {
                try
                {
                    var url = await context.Url.FirstOrDefaultAsync(x => x.ShortCode == shortcode);
                    if (url == null)
                    {
                        return Results.NotFound($"No url with shortcode: {shortcode}");
                    }
                    return Results.Ok(url);
                }
                catch (Exception ex)
                {
                    return Results.InternalServerError(ex.Message);
                }
            });

            route.MapPut("shorten/{shortcode:}", async (string shortcode, UrlRequest req, DataContext context) =>
            {
                try
                {
                    var url = await context.Url.FirstOrDefaultAsync(x => x.ShortCode == shortcode);

                    if (url == null)
                    {
                        return Results.NotFound($"No url with shortcode: {shortcode}");

                    }
                    url.UpdatedAt = DateTime.UtcNow;
                    url.ChangeUrl(req.url);
                    await context.SaveChangesAsync();

                    return Results.Ok(url);

                }
                catch (Exception ex)
                {
                    return Results.InternalServerError(ex.Message);
                }
            });

            route.MapDelete("shorten/{shortcode:}", async (string shortcode, DataContext context) =>
            {
                try
                {
                    var url = await context.Url.FirstOrDefaultAsync(x => x.ShortCode == shortcode);


                    if (url == null)
                    {
                        return Results.NotFound($"No url with shortcode: {shortcode}");

                    }

                    context.Remove(url);
                    await context.SaveChangesAsync();
                    return Results.Accepted();

                }
                catch (Exception ex)
                {
                    return Results.InternalServerError(ex.Message);
                }
            });
            

        }
    }
}
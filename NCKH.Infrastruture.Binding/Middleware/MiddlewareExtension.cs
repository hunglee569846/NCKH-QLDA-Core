
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace NCKH.Infrastruture.Binding.Middleware
{
    public static class MiddlewareExtension
    {
        public static void GHMWelcome(this IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(@"<head><title>GHM NailSpa</title> <link href=""/swagger-ui/logo.ico"" type=""image/x-icon"" rel="" shortcut icon ""></head>
                                                    <body style='background-color:powderblue;'>
                                                    <h1 style = 'color:red;'>Hello World!</h1>
                                                    <h2 style = 'color:green;'> Welcome to GHMSOFT.</h2>");
            });
        }

        public static void GHMError(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(@"<body style='background-color:powderblue;'>
                                                    <h1 style = 'color:red;'>Error!</h1>
                                                    <h2 style = 'color:green;'> Welcome to GHMSOFT.</h2>");
            });
        }

        public static void GHMSignalrRequestToken(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                var x = context.Request.Path.Value;
                if (x.StartsWith("/notifications"))
                {
                    if (context.Request.Query.TryGetValue("access_token", out StringValues token))
                    {
                        context.Request.Headers.Add("Authorization", new[] { "bearer " + token });
                    }

                    context.Response.Headers.Add("Access-Control-Allow-Credentials", "true");

                    if (context.Request.Method == "OPTIONS")
                    {
                        context.Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
                        context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept, User, x-requested-with");
                    }

                    //if (context.Request.Method != "OPTIONS" && !context.Request.HttpContext.User.Identity.IsAuthenticated)
                    //{
                    //    context.Response.StatusCode = 401;
                    //    return; //Stop pipeline and return immediately.
                    //}
                }
                await next();
            });
        }

        public static IApplicationBuilder GHMRequestLocalization(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLocalizationMiddleware>();
        }

        public static IApplicationBuilder GHMException(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }


    }
}

using EducationalQuizApp.Pages;
using EducationalQuizApp.Services;
using EducationalQuizApp.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
/*
 Website name to make billions and retire to be a professional 40mu Midair player:
                    
                        QUIZCRAFTAI 
 */
// Add services to the container.
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Configure Logging
builder.Logging.ClearProviders();
builder.Logging.AddDebug();

// Register services with DI container.

builder.Services.AddScoped<QuizService>();
builder.Services.AddScoped<QuizStateManager>();
builder.Services.AddScoped<QuizManager>();
builder.Services.AddScoped<QuizCategoryModel>();  
builder.Services.AddScoped<QuizResultsModel>();


// This is required for using session state (which QuizStateManager might use).
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Configure HTTP context accessor, required for accessing HttpContext in services.
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Add session middleware
app.UseSession();

app.MapRazorPages();

app.Run();

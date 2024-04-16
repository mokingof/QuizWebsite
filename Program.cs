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

// Register your custom services with the DI container.
builder.Services.AddTransient<QuizService>();
builder.Services.AddScoped<QuizStateManager>();
builder.Services.AddScoped<QuizEvaluator>();

// This is required for using session state (which QuizStateManager might use).
builder.Services.AddSession();

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

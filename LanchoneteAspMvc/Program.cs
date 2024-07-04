using LanchoneteAspMvc.Extensions;
using LanchoneteAspMvc.IoC;
using LanchoneteAspMvc.Services;
using Microsoft.AspNetCore.Identity;
using ReflectionIT.Mvc.Paging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDependencyInjection(builder.Configuration);

builder.Services.AddPaging(opt => {
    opt.ViewName = "Bootstrap5";
    opt.PageParameterName = "pageindex";
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
SeedUserRoleInitial.CriarPerfis(app);

app.UseSession();

app.UseAuthentication();
app.UseAuthorization();


app.UseMapControllerRoutes();

app.Run();

using Common.Application;
using Common.Application.FileUtil.Interfaces;
using Common.Application.FileUtil.Services;
using Common.AspNetCore;
using Common.AspNetCore.Middlewares;
using Microsoft.AspNetCore.Mvc;
using Shop.Api.Infrastructure;
using Shop.Api.Infrastructure.JwtUtil;
using Shop.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().ConfigureApiBehaviorOptions(option =>
{
    option.InvalidModelStateResponseFactory = (context =>
    {
        var result = new ApiResult()
        {
            IsSuccess = false,
            MetaData = new MetaData()
            {
                AppStatusCode = AppStatusCode.BadRequest,
                Message = ModelStateUtil.GetModelStateErrors(context.ModelState)
            }
        };
        return new BadRequestObjectResult(result);
    });
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.RegisterShopDependency(builder.Configuration.GetConnectionString("DefaultConnection"));
CommonBootstrapper.Init(builder.Services);
builder.Services.AddTransient<IFileService, FileService>();
builder.Services.RegisterApiDependency();
builder.Services.JwtAuthenticationConfig(builder.Configuration);






var app = builder.Build();


// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{


//}
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseApiCustomExceptionHandler();

app.MapControllers();

app.Run();


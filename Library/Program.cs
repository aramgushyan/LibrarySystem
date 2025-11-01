using Applications.Dto;
using Applications.Dto.Book;
using Applications.Dto.Issue;
using Applications.Dto.Reader;
using Applications.Validators;
using Applications.Validators.BookValidators;
using Applications.Validators.IssueValidators;
using Applications.Validators.ReaderValidators;
using Domain.Models;
using FluentValidation;
using Infrastructure;
using Infrastructure.Extensions;
using Library.Middlewares;
using Microsoft.EntityFrameworkCore;
using Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<LibraryDbContext>(o =>
    o.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();


builder.Services.AddScoped<IValidator<AddReaderDto>, ReaderValidator>();
builder.Services.AddScoped<IValidator<UpdateReaderDto>, UpdateReaderValidator>();

builder.Services.AddScoped<IValidator<AddBookDto>, BookValidator>();
builder.Services.AddScoped<IValidator<UpdateBookDto>, UpdateBookValidator>();

builder.Services.AddScoped<IValidator<Issue>, IssueValidator>();
builder.Services.AddScoped<IValidator<UpdateIssueDto>, UpdateIssueValidator>();

builder.Services.AddScoped<IValidator<PeriodDto>, PeriodValidator>();

builder.Services.AddServices();
builder.Services.AddRepositories();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseMiddleware<ExceptionHandler>();

app.UseHttpsRedirection();


app.UseSwagger();
app.UseSwaggerUI();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();



app.Run();

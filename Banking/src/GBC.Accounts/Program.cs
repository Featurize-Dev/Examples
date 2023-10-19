using Featurize;
using GBC.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Features()
    .AddOpenApi()
    .DiscoverFeatures();

var app = builder.BuildWithFeatures();

app.Run();
using Featurize;
using Featurize.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Features()
    .AddRepositories()
    .DiscoverFeatures();

var app = builder.BuildWithFeatures();

app.Run();

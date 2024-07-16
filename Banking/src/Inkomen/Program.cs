using Common;
using Featurize;
using Featurize.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Features()
    .AddCommonFeatures()
    .AddRepositories()
    .DiscoverFeatures();

var app = builder.BuildWithFeatures();

app.Run();


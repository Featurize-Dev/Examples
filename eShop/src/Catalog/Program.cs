using CommonFeatures;
using Featurize;

var builder = WebApplication.CreateBuilder(args);

builder.Features()
    .AddDefaultFeatures()
    .DiscoverFeatures();

var app = builder.BuildWithFeatures();

app.Run();

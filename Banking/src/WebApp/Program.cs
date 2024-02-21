using Common;
using Featurize;

var builder = WebApplication.CreateBuilder(args);

builder.Features()
    .AddCommonFeatures()
    .DiscoverFeatures();
    
var app = builder.BuildWithFeatures();

app.Run();

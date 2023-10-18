using Featurize;
using Featurize.Repositories;
using Featurize.Repositories.MongoDB;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Features()
.AddRepositories(o =>
    {
        o.AddMongo("mongodb://username:password@localhost:27017");
    })
    .DiscoverFeatures();

var app = builder.BuildWithFeatures();

app.Run();

using Catalog.Features.Catalog;
using Microsoft.Extensions.Options;
using System;

namespace Catalog.Features;

public class CatalogServices
{
    public CatalogContext Context { get; set; }
    public IOptions<CatalogOptions> Options { get; set; }
    public ILogger<CatalogServices> Logger { get; set; }
    public IWebHostEnvironment Environment { get; set; }

    public string GetFullPicturePath(string filename)
        => Path.Combine(Environment.ContentRootPath, "Pics", filename);

    public string GetImageMimeTypeFromImageFileExtension(string extension) => extension switch
    {
        ".png" => "image/png",
        ".gif" => "image/gif",
        ".jpg" or ".jpeg" => "image/jpeg",
        ".bmp" => "image/bmp",
        ".tiff" => "image/tiff",
        ".wmf" => "image/wmf",
        ".jp2" => "image/jp2",
        ".svg" => "image/svg+xml",
        _ => "application/octet-stream",
    };
}

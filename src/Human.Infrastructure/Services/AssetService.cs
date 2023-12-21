using System.Globalization;
using CloudinaryDotNet;
using Human.Core.Interfaces;
using Human.Core.Models;
using Human.Domain.Models;
using Human.Infrastructure.Models;
using Microsoft.Extensions.Options;

namespace Human.Infrastructure.Services;

public class AssetService : IAssetService
{
    private readonly Cloudinary cloudinary;
    private readonly CloudinaryOptions cloudinaryOptions;

    public AssetService(IOptions<CloudinaryOptions> cloudinaryOptions)
    {
        this.cloudinaryOptions = cloudinaryOptions.Value;
        cloudinary = new Cloudinary(new Account(this.cloudinaryOptions.CloudName, this.cloudinaryOptions.ApiKey, this.cloudinaryOptions.ApiSecret));
        cloudinary.Api.Secure = true;
    }

    public string SignImageUploadUrl(object? parameters = null)
    {
        var url = cloudinary.Api.ApiUrlImgUpV;
        var signedUrl = SignUrl(url, ToDictionary(parameters));
        return $"{signedUrl.Url.BuildUrl()}?signature={signedUrl.Signature}&api_key={cloudinaryOptions.ApiKey}&{string.Join('&', signedUrl.Parameters.Select(x => $"{x.Key}={x.Value}"))}";
    }

    public string GenerateUrl(AssetInfo info)
    {
        return cloudinary.Api.UrlImgUp
            .Source(info.Key)
            .Format(info.Format)
            .Version(info.Version.ToString(CultureInfo.InvariantCulture))
            .BuildUrl();
    }

    private static SortedDictionary<string, object>? ToDictionary(object? parameters)
    {
        SortedDictionary<string, object>? dictionary = null;
        if (parameters is SortedDictionary<string, object> dict)
        {
            dictionary = dict;
        }
        else if (parameters is not null)
        {
            dictionary = new SortedDictionary<string, object>(StringComparer.Ordinal);
            foreach (var property in parameters.GetType().GetProperties())
            {
                var value = property.GetValue(parameters);
                if (value is not null)
                {
                    dictionary.Add(property.Name, value);
                }
            }
        }
        return dictionary;
    }

    private SignedUrl SignUrl(Url url, SortedDictionary<string, object>? parameters)
    {
        parameters ??= new SortedDictionary<string, object>(StringComparer.Ordinal);
        parameters["timestamp"] = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(CultureInfo.InvariantCulture);
        if (parameters.TryGetValue("folder", out var folder) && folder is string assetFolder)
        {
            parameters["folder"] = string.Join('/', cloudinaryOptions.BasePath, assetFolder);
        }
        else
        {
            parameters["folder"] = cloudinaryOptions.BasePath;
        }
        var signature = cloudinary.Api.SignParameters(parameters);
        return new SignedUrl
        {
            Url = url,
            Parameters = parameters,
            Signature = signature,
        };
    }
}

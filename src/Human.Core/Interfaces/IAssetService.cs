using Human.Domain.Models;

namespace Human.Core.Interfaces;

public interface IAssetService
{
    string SignImageUploadUrl(object? parameters = null);
    string GenerateUrl(AssetInfo info);
}

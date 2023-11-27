using Featurize.ValueObjects.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Ordering.Features.Order.ValueObjects;
using System.Diagnostics.CodeAnalysis;

namespace Ordering.Features.Order.Entities;

public record UserInfo(UserId Id, string Username)
{

    public static UserInfo Empty => new(UserId.Empty, string.Empty);

}

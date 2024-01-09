﻿using Dietly.Application.Lists.Commands.SendEmailWithList;

namespace Dietly.WebApi.Resources.Lists.SendEmailWithList;

public static class SendEmailWithListMapper
{
    public static SendEmailWithListCommand ToCommand(this SendEmailWithListRequest request) => new()
    {
        UserId = request.UserId,
        From = DateTime.Parse(request.From),
        To = DateTime.Parse(request.To)
    };
}
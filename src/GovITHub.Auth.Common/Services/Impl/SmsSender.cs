﻿using System.Threading.Tasks;

namespace GovITHub.Auth.Common.Services.Impl
{
    public class SmsSender : ISmsSender
    {
        public Task SendSmsAsync(string number, string message)
        {
            return Task.FromResult(0);
        }
    }
}

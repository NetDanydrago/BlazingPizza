﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using BlazingPizza.Shared;
using System.Net.Http;

namespace BlazingPizza.Client.Services
{
    public class ServerAuthenticationStateProvider : AuthenticationStateProvider 
    {
        private readonly HttpClient HttpClient;
        public ServerAuthenticationStateProvider(HttpClient httpClient)
        {
            this.HttpClient = httpClient;
        }

        public override async Task<AuthenticationState> 
            GetAuthenticationStateAsync()
        {
            var UserInfo =
                 await HttpClient.GetJsonAsync<UserInfo>("user");
            var Identity = UserInfo.IsAuthenticated ?
                new ClaimsIdentity(
                    new[]
                    {
                        new Claim(ClaimTypes.Name, UserInfo.Name)
                    }, "serverauth")
                    : new ClaimsIdentity();
            return new AuthenticationState(new ClaimsPrincipal(Identity));
        }
    }
}

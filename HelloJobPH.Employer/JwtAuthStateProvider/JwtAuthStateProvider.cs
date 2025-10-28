//using Microsoft.AspNetCore.Components.Authorization;
//using Microsoft.AspNetCore.Http;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Threading.Tasks;

//namespace HelloJobPH.Employer.JwtAuthStateProviders
//{
//    public class JwtAuthStateProvider : AuthenticationStateProvider
//    {
//        private readonly IHttpContextAccessor _httpContextAccessor;

//        public JwtAuthStateProvider(IHttpContextAccessor httpContextAccessor)
//        {
//            _httpContextAccessor = httpContextAccessor;
//        }

//        public override Task<AuthenticationState> GetAuthenticationStateAsync()
//        {
//            var httpContext = _httpContextAccessor.HttpContext;
//            if (httpContext == null)
//                return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));

//            // 🧠 Retrieve JWT token from cookie
//            var token = httpContext.Request.Cookies["jwtToken"];
//            if (string.IsNullOrWhiteSpace(token))
//                return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));

//            try
//            {
//                var handler = new JwtSecurityTokenHandler();
//                var jwt = handler.ReadJwtToken(token);
//                var identity = new ClaimsIdentity(jwt.Claims, "jwt");
//                var user = new ClaimsPrincipal(identity);

//                return Task.FromResult(new AuthenticationState(user));
//            }
//            catch
//            {
//                // Invalid or expired token
//                var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
//                return Task.FromResult(new AuthenticationState(anonymous));
//            }
//        }

//        // ⚡ Not needed if the server sets the cookie, but you can still use it if you want
//        public void NotifyUserAuthentication()
//        {
//            var httpContext = _httpContextAccessor.HttpContext;
//            var token = httpContext?.Request.Cookies["jwtToken"];

//            if (string.IsNullOrWhiteSpace(token))
//            {
//                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))));
//                return;
//            }

//            var handler = new JwtSecurityTokenHandler();
//            var jwt = handler.ReadJwtToken(token);
//            var identity = new ClaimsIdentity(jwt.Claims, "jwt");
//            var user = new ClaimsPrincipal(identity);

//            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
//        }

//        public void NotifyUserLogout()
//        {
//            var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
//            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
//        }
//    }
//}


using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HelloJobPH.Employer.JwtAuthStateProviders
{
    public class JwtAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;

        public JwtAuthStateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");

            if (string.IsNullOrWhiteSpace(token))
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            var identity = new ClaimsIdentity(jwt.Claims, "jwt");
            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }

        public async Task MarkUserAsAuthenticated(string token)
        {
            await _localStorage.SetItemAsync("authToken", token);
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var user = new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims, "jwt"));
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _localStorage.RemoveItemAsync("authToken");
            var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
        }
    }
}
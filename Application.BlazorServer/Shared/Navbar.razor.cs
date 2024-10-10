using Application.BlazorServer.Security;

namespace Application.BlazorServer.Shared;

public partial class Navbar
{
    [Inject] AuthenticationService _authenticationService { get; set; }

}
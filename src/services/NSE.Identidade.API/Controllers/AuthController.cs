using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NSE.Identidade.API.Extensions;
using NSE.Identidade.API.Models;

namespace NSE.Identidade.API.Controllers
{
    [ApiController]
    [Route("api/identidade")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;

    public AuthController(SignInManager<IdentityUser> signManager,
                          UserManager<IdentityUser> userManager,
                          IOptions<AppSettings> appSettings)
    {
        _signManager = signManager;
        _userManager = userManager;
        _appSettings = appSettings.Value;
    }

    [HttpPost("nova-conta")]
    public async Task<ActionResult> Registrar(UsuarioRegistro usuarioRegistro)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);

        var user = new IdentityUser
        {
            UserName = usuarioRegistro.Email,
            Email = usuarioRegistro.Email,
            EmailConfirmed = true
        };
        var result = await _userManager.CreateAsync(user, usuarioRegistro.Senha);

        if(result.Succeeded)
        {
            await _signManager.SignInAsync(user, isPersistent:false);

            return CustomResponse(await GerarJwt(usuarioRegistro.Email));
        }

        foreach (var error in result.Errors)
        {
            AdicionarErroProcessamento(error.Description);
        }

        return CustomResponse();
    }

    [HttpPost("autenticar")]
    public async Task<ActionResult> Login(UsuarioLogin usuarioLogin)
    {
        if(!ModelState.IsValid) return CustomResponse(ModelState);

        var result = await _signManager.PasswordSignInAsync(userName:usuarioLogin.Email, password:usuarioLogin.Senha, isPersistent:false, lockoutOnFailure:true);

        if(result.Succeeded)
        {
            return CustomResponse(await GerarJwt(usuarioLogin.Email));
        }
        
        if(result.IsLockedOut)
        {
            AdicionarErroProcessamento("Usu??rio temporariamente bloqueado por tentativas inv??lidas!");
            return CustomResponse();    
        }

        AdicionarErroProcessamento("Usu??rio ou senha incorretos.");
        
        return CustomResponse();
    }

    private async Task<ClaimsIdentity> ObterClaimsUsuario(ICollection<Claim> claims, IdentityUser user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);

        claims.Add(new Claim(type:JwtRegisteredClaimNames.Sub, value:user.Id));
        claims.Add(new Claim(type:JwtRegisteredClaimNames.Email, value:user.Email));
        claims.Add(new Claim(type:JwtRegisteredClaimNames.Jti, value:Guid.NewGuid().ToString()));
        claims.Add(new Claim(type:JwtRegisteredClaimNames.Nbf, value:ToUnixEpochDate(DateTime.UtcNow).ToString()));
        claims.Add(new Claim(type:JwtRegisteredClaimNames.Iat, value:ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

        foreach(var userRole in userRoles)
        {
            claims.Add(new Claim(type:"role", value:userRole));
        }

        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);

        return identityClaims;
    }

    private string CodificarToken(ClaimsIdentity identityClaims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _appSettings.Emissor,
            Audience = _appSettings.ValidoEm,
            Subject = identityClaims,
            Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });

        return tokenHandler.WriteToken(token);
    }

    private UsuarioRespostaLogin ObterRespostaToken(string encodedToken, IdentityUser user, IEnumerable<Claim> claims)
    {
        return new UsuarioRespostaLogin
        {
            AccessToken = encodedToken,
            ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds,
            UsuarioToken = new UsuarioToken
            {
                Id = user.Id,
                Email = user.Email,
                Claims = claims.Select(c => new UsuarioClaim{ Type = c.Type, Value = c.Value })
            }
        };
    }

    private async Task<UsuarioRespostaLogin> GerarJwt(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var claims = await _userManager.GetClaimsAsync(user);

        var identityClaims = await ObterClaimsUsuario(claims, user);
        var encodedToken = CodificarToken(identityClaims);

        return ObterRespostaToken(encodedToken, user, claims);
    }

    private static long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(year:1970, month:1, day:1, hour:0, minute:0, second:0, offset:TimeSpan.Zero)).TotalSeconds);
    }
}
using BLL.interfaces;
using DAL.UOW;
using domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace BLL.security;

public class SecurityService : IsecurityService
{
    public IConfiguration _configuration { get; }
    public IusersService _usersService;
    public IUOW _db;
    public SecurityService(IConfiguration configuration, IusersService userService, IUOW db)
    {//Dépendances
        _db = db;
        _configuration = configuration;
        _usersService = userService;
    }

    public async Task<string> signIn(string username, string password)
    {

        //appel BDD pour récupérer les données de l'utilisateur se connectant:
        User user = await _db.Users.GetUserFromConnectionAsync(username, password);


        bool passwordValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
        //Si aucune donnée n'est récupérée de l'utilisateur et que le mot de passe est valide on retourne le token JWT sinon retour null:
        if (user is not null && passwordValid)
        {//Génération du token en passant le name,liste de string, le rôle du user grace à son id: il sera admin ou "user":
            string role = await _usersService.GetUserRoleById(user.Id) ?? "user";
            return GenerateJwtToken(user.Name, new List<string>() { role });
        }
        else
        {
            return null;
        }
    }

    private string GenerateJwtToken(string username, List<string> roles)
    {
        //Add User Infos
        var claims = new List<Claim>(){
               new Claim(JwtRegisteredClaimNames.Sub, username), //ID User (Subject)
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //ID Token
               new Claim(ClaimTypes.NameIdentifier, username) //ID User (NameIdentifier)  == (Subject)
           };

        //Add Roles
        roles.ForEach(role =>
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        });

        //Signing Key
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        //Expiration time
        var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JwtExpireDays"]));

        //Create JWT Token Object
        var token = new JwtSecurityToken(
            _configuration["JwtIssuer"],//Issuer
            _configuration["JwtIssuer"],//Audience
            claims,//Charge utile (Payload)
            expires: expires, //Expiration time
            signingCredentials: creds //Signing Key
        );

        //Serializes a JwtSecurityToken into a JWT in Compact Serialization Format.
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}







using Microsoft.IdentityModel.Tokens;
using StudentGroupsManager.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentGroupsManager.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateTokenStudent(Student student)
        {
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Secret"));

            var tokenDescriptior = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("RA", student.RA.ToString()),
                    new Claim("Id", student.Id.ToString()),
                    new Claim(ClaimTypes.Role, "Student")
                }),

                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return GenerateToken(tokenDescriptior);
        }

        public string GenerateTokenTeacherCoordinator(TeacherCoordinator teacherCoordinator)
        {
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Secret"));

            var tokenDescriptior = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("RP", teacherCoordinator.RP.ToString()),
                    new Claim("Id", teacherCoordinator.Id.ToString()),
                    new Claim(ClaimTypes.Role, "TeacherCoordinator")
                }),

                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return GenerateToken(tokenDescriptior);
        }

        public string GenerateToken(SecurityTokenDescriptor tokenDescriptior)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptior);
            return tokenHandler.WriteToken(token);
        }
    }
}

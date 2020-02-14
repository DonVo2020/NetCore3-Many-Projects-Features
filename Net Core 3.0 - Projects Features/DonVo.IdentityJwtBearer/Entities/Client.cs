using System.ComponentModel.DataAnnotations;

namespace DonVo.IdentityJwtBearer.Entities
{
    public class Client
    {
        //[Key]
        public int Id { get; set; }
        public string ClientId { get; set; }
        public int RefreshTokenLifeTime { get; set; }
    }
}
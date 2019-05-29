using CcRep.Models._dc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System.Threading.Tasks;

namespace CcRep.Models
{
    public class CcRepUserManager : UserManager<CcRepUser, string>
    {
        public CcRepUserManager(IUserStore<CcRepUser, string> store)
                : base(store)
        {
        }
        public static CcRepUserManager Create(IdentityFactoryOptions<CcRepUserManager> options, IOwinContext context)
        {
            CcRepContext db = context.Get<CcRepContext>();
            CcRepUserManager manager = new CcRepUserManager(new CcRepUserStore(db));

            return manager;
        }

        public async Task<string> CreateWithClaims(CcRepUser user)
        {
            await this.CreateAsync(user);

            var FlAccessClaim = new CcRepUserClaim { ClaimType = "FlAccess", ClaimValue = "ФЛ" };
            var PdAccessClaim = new CcRepUserClaim { ClaimType = "PdAccess", ClaimValue = "Y" };

            // добавляем claim пользователю
            user.Claims.Add(FlAccessClaim);
            user.Claims.Add(PdAccessClaim);
            await this.UpdateAsync(user);

            return user.Id;
        }
    }
}
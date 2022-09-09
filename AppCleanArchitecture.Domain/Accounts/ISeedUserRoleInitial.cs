using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCleanArchitecture.Domain.Accounts
{
    public interface ISeedUserRoleInitial
    {
        void SeedUser();
        void SeedRoles();
    }
}

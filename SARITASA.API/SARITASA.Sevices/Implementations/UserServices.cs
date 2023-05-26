using SARITASA.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SARITASA.Sevices.Implementations
{
    public class UserServices:IUserServices
    {
        ApplicationDbContext _appContext;
        public UserServices(ApplicationDbContext appContext) 
        {
            _appContext = appContext;
        }
    }
}

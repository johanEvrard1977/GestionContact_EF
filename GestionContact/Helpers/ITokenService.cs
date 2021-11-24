using GestionContactEF.Dal.Models;
using GestionContactEF.Dal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestionContact.Helpers
{
    public interface ITokenService
    {
        string GenerateToken(LoginSuccessDto user);
        LoginSuccessDto ValidateToken(string token);
    }
}

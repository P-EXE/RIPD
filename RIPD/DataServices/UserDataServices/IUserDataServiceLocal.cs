using RIPD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIPD.DataServices
{
  public interface IUserDataServiceLocal
  {
    Task CreateAsync(User user);

    Task<User> GetFirstAsync();
  }
}

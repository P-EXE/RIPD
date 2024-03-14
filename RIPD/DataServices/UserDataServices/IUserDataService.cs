using RIPD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIPD.DataServices
{
  internal interface IUserDataService
  {
    public Task CreateAsync(User user);
    public Task<List<User>> GetMultipleAsync(Dictionary<string,string> queryParams);
    public Task<User> GetAsync(int id);
    public Task<User> GetAsync(string email);
  }
}

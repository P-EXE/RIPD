using RIPD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIPD.DataServices
{
  internal interface IRunDataService
  {
    Task CreateAsync(Run run);
    Task<Run> GetAsync(User user, int id);
    Task<List<Run>> GetAllAsync(User user);
  }
}

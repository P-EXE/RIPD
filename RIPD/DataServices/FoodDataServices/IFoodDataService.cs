using RIPD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIPD.DataServices
{
  public interface IFoodDataService
  {
    /// <summary>
    /// Create a Food via a PUT HTTP Requests to an API or Locally.
    /// </summary>
    /// <param name="food">Represents a Food Entity.</param>
    /// <returns>Nothing</returns>
    Task CreateAsync(Food food);

    /// <summary>
    /// Get a single Food via an ID.
    /// </summary>
    /// <param name="id">Represents a unique ID of a Food or a .../{id} Route to a Food in case of an API.</param>
    /// <returns>A Food</returns>
    Task<Food> GetOneAsync(int id);

    /// <summary>
    /// Get a single Food via a Barcode.
    /// </summary>
    /// <param name="barcode">Represents a unique Barcode of a Food.</param>
    /// <returns>A Food</returns>
    Task<Food> GetOneAsync(string barcode);

    /// <summary>
    /// Get all Foods
    /// </summary>
    /// <remarks>
    /// Soon to be deprecated
    /// </remarks>
    /// <returns>A List of Foods</returns>
    Task<List<Food>> GetMultipleAsync();

    /// <summary>
    /// Get multiple Foods via matchable Property-Value pairs.
    /// </summary>
    /// <example>
    /// { "name", "Food Name" },
    /// { "manufacturer", "Company" }
    /// </example>
    /// <param name="queryParams">Represents a Property-Value pair used for matching Foods.</param>
    /// <returns>A List of Foods</returns>
    Task<List<Food>> GetMultipleAsync(Dictionary<string,string> queryParams);

    /// <summary>
    /// Update a food
    /// </summary>
    /// <param name="food">A Food</param>
    /// <returns>Nothing</returns>
    Task UpdateAsync(Food food);

    /// <summary>
    /// Delete a Food
    /// </summary>
    /// <param name="id">Represents a Food.Id</param>
    /// <returns>Nothing</returns>
    Task DeleteAsync(int id);
  }
}

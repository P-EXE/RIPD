using System.ComponentModel.DataAnnotations;

namespace RIPD.Models;

// Id will be auto generated
public class Food_UpdateDTO
{
  public required int Id { get; set; }
  public string? Barcode { get; set; }
  [Required]
  public required string Name { get; set; }
  public int Manufacturer { get; set; }
  [Required]
  public int Contributer { get; set; }
  [Required]
  public required DateTime CreationDateTime { get; set; }
  public DateTime UpdateDateTime { get; set; }
  public string? Description { get; set; }
  public string? Image { get; set; }

  /* Macros */
  public float? Energy { get; set; } /* Kcal */
  public float? Water { get; set; } /* ml */
  public float? Protein { get; set; } /* g */
  public float? Fat { get; set; } /* g */
  public float? Carbohydrates { get; set; } /* g */
  public float? Fiber { get; set; } /* g */
  public float? Sugar { get; set; } /* g */

  /* Minerals */
  public float? Calcium { get; set; } /* mg */
  public float? Iron { get; set; } /* mg */
  public float? Magnesium { get; set; } /* mg */
  public float? Phosphorus { get; set; } /* mg */
  public float? Potassium { get; set; } /* mg */
  public float? Sodium { get; set; } /* mg */
  public float? Zinc { get; set; } /* mg */
  public float? Copper { get; set; } /* mg */
  public float? Manganese { get; set; } /* mg */
  public float? Selenium { get; set; } /* ug */

  /* Vitamins */
  public float? VitaminC { get; set; }/* mg */
  public float? VitaminB1 { get; set; } /* mg */
  public float? VitaminB2 { get; set; } /* mg */
  public float? VitaminB3 { get; set; } /* mg */
  public float? VitaminB5 { get; set; } /* mg */
  public float? VitaminB6 { get; set; } /* mg */
  public float? VitaminB9 { get; set; } /* ug */
  public float? VitaminB12 { get; set; } /* ug */
  public float? VitaminA1 { get; set; } /* ug */
  public float? Retinol { get; set; } /* ug */
  public float? AlphaCarotin { get; set; } /* ug */
  public float? BetaCarotin { get; set; } /* ug */
  public float? BetaCryptoxanthin { get; set; } /* ug */
  public float? Lycopene { get; set; } /* ug */
  public float? LutZea { get; set; } /* ug */
  public float? VitamninE { get; set; } /* mg */
  public float? VitaminD { get; set; } /* ug */
  public float? VitaminK { get; set; } /* ug */
  public float? Choline { get; set; } /* mg */

  /* Alcohols */
  public float? Alcohol { get; set; } /* ml */

  /* Fats */
  public float? FattyAcidsSaturated { get; set; } /* g */
  public float? FattyAcidsMono { get; set; } /* g */
  public float? FatyAcidsPoly { get; set; } /* g */
  public float? Cholesterol { get; set; } /* mg */
}

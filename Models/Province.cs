using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
    public class Province
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
    public class City
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [ForeignKey(nameof(Province))]
        public int ProvinceId { get; set; }
        public virtual Province? Province { get; set; }
    }
    public class Suburb
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [ForeignKey(nameof(City))]
        public int CityId { get; set; }
        public virtual City? City { get; set; }
    }


}

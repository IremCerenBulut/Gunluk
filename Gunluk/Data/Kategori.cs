using System.ComponentModel.DataAnnotations;

namespace Gunluk.Data
{
    public class Kategori
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Ad { get; set; } = null!;

        //navigation property

        public List<Gonderi> Gonderiler { get; set; }=new List<Gonderi>(); // (HashSet=>tekrarları dikkate almıyor)
    }
}

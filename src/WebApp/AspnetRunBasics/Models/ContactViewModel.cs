using System.ComponentModel.DataAnnotations;

namespace AspnetRunBasics.Models
{
    public class ContactViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Ad Soyad bilginizi giriniz.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "E-Posta bilginizi giriniz.")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                            @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                            ErrorMessage = "E-poasta adresinizi kontrol ediniz.")]
        public string Email { get; set; }
        [Phone(ErrorMessage = "Telefon numaranızın dogru olduğunu kontrol ediniz.")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Mesaj giriniz.")]
        public string Message { get; set; }
    }
}

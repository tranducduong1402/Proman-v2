using System.ComponentModel.DataAnnotations;

namespace ProMan.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
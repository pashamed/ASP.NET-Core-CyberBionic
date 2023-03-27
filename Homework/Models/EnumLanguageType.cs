using System.ComponentModel.DataAnnotations;

namespace Homework.Models
{
    public enum EnumLanguageType
    {
        [Display(Name = "JavaScript")]
        JavaScript,
        [Display(Name = "C#")]
        C,
        Java,
        Python,
        [Display(Name = "Основы")]
        Basic
    }
}

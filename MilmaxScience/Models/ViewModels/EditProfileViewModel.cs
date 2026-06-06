using System.ComponentModel.DataAnnotations;

namespace MilmaxScience.Models.ViewModels
{
    public class EditProfileViewModel
    {
        [Required(ErrorMessage = "Введите ФИО")]
        [StringLength(100, MinimumLength = 3,
            ErrorMessage = "ФИО должно содержать от 3 до 100 символов")]
        [RegularExpression(
            @"^[А-Яа-яЁёA-Za-z\s\-]+$",
            ErrorMessage = "ФИО может содержать только буквы")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Введите Email")]
        [EmailAddress(ErrorMessage = "Некорректный Email")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Некорректный телефон")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Введите дату рождения")]
        public DateTime BirthDate { get; set; }
    }
}

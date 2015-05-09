﻿using System;
using System.ComponentModel.DataAnnotations;

namespace JAM.Brilliance.Models.ViewModels
{
    public class ChangeNewPasswordViewModel
    {
        [Required(ErrorMessage = "{0} måste anges")]
        [StringLength(256, ErrorMessage = "{0} får vara max {1} tecken långt.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Ogiltig {0}")]
        [Display(Name = "E-Post")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} måste anges")]
        [DataType(DataType.Password)]
        [Display(Name = "Nuvarande lösenord")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "{0} måste anges")]
        [StringLength(100, ErrorMessage = "{0} måste vara mellan {2} och {1} tecken långt.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nytt lösenord")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bekräfta nytt lösenord")]
        [Compare("NewPassword", ErrorMessage = "Lösenorden du angivit är inte samma.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "{0} måste anges")]
        public Guid ReminderGuid { get; set; }
    }
}
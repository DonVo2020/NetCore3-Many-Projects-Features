using DonVo.ViewModels.Attributes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace DonVo.ViewModels
{
    //public class BaseLoginViewModel
    //{
    //    [Required(ErrorMessage = "Email cannot be empty")]
    //    [EmailAddress(ErrorMessage = "Invalid Email")]
    //    public string Email { get; set; }

    //    [DataType(DataType.Password)]
    //    [Required(ErrorMessage = "Password cannot be blank")]
    //    public string Password { get; set; }
    //}

    public class LoginViewModel : BaseLoginViewModel
    {
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    //public class TokenViewModel : BaseLoginViewModel
    //{
    //    [Required]
    //    [StringRange(AllowableValues = new[] { "WEB-APPLICATION", "DESKTOP-APPLICATION", "MOBILE-APPLICATION" },
    //        ErrorMessage = "Client Type must be either 'Web-Application', 'Desktop-Application' or 'Mobile-Application'.")]
    //    public string ClientType { get; set; }
    //}

    //public class RefreshTokenViewModel
    //{
    //    [Required]
    //    [StringRange(AllowableValues = new[] { "WEB-APPLICATION", "DESKTOP-APPLICATION", "MOBILE-APPLICATION" },
    //        ErrorMessage = "ClientType must be either 'Web-Application', 'Desktop-Application' or 'Mobile-Application'.")]
    //    public string ClientType { get; set; }
    //    [Required(ErrorMessage = "Refresh Token cannot be blank")]
    //    public string RefreshToken { get; set; }
    //}

    public class CreateMainAccountingViewModel
    {
        public int? ParentAccountKey { get; set; }
        [Required(ErrorMessage = "AccountLabel cannot be blank!")]
        public string AccountLabel { get; set; }
        [Required(ErrorMessage = "AccountName cannot be blank!")]
        public string AccountName { get; set; }
        public string AccountDescription { get; set; }
        [Required(ErrorMessage = "AccountType cannot be blank!")]
        public string AccountType { get; set; }
        [Required(ErrorMessage = "Operator cannot be blank!")]
        public string Operator { get; set; }
        [Required(ErrorMessage = "CustomMembers cannot be blank!")]
        public string CustomMembers { get; set; }
        [Required(ErrorMessage = "ValueType cannot be blank!")]
        public string ValueType { get; set; }
        public string CustomMemberOptions { get; set; }
        public int? EtlloadId { get; set; }
        public DateTime? UpdateDate { get; set; }
    }

    public class UpdatePersonalDataAccountViewModel
    {
        [Required]
        public string HashId { get; set; }
        [Required(ErrorMessage = "Surname cannot be blank")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Name cannot be blank")]
        public string LastName { get; set; }
        public string Patronymic { get; set; }
    }

    public class UpdateEmailAccountViewModel
    {
        [Required]
        public string HashId { get; set; }
        [Required(ErrorMessage = "Email Cannot Be Empty")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        [Remote("CheckEmail", "Account", ErrorMessage = "This email is already in use!")]
        public string Email { get; set; }
    }

    public class UpdatePasswordAccountViewModel
    {
        [Required]
        public string HashId { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Old password cannot be blank")]
        public string OldPassword { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "New password cannot be blank")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }

    public class UpdateRoleAccountViewModel
    {
        [Required]
        public string HashId { get; set; }
        [Required]
        public string Role { get; set; }
    }
}

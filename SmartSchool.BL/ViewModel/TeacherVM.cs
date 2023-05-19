using SmartSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using SmartSchool.DAL.OurEnums;
using System.Text.Json.Serialization;

namespace SmartSchool.BL.ViewModel
{
    public class TeacherVM
    {
        public string? Id { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression("^[a-zA-Z]{2,15}\\s([a-zA-Z]{2,15}\\s)[a-zA-Z]{2,15}$", ErrorMessage = "Please Enter at least 3 Names")]
        public string FullName { set; get; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please Enter valid Email Address")]
        public string Email { get; set; }

        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$", ErrorMessage = "Password must contain at least (8 charachters, 1 Special Charachter, 1 UpperCase, 1 LowerCase, 1 Digit")]
        public string Password { set; get; }


        [Required]
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }


        [Required]
        [Column(TypeName = "decimal(8,2)")]
        public decimal Salary { get; set; }


        [Required]
        [StringLength(11)]
        [RegularExpression("^01[0125][0-9]{8}$", ErrorMessage = "Please Enter valid Phone number")]
        public string Phone { get; set; }


        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        public string? PhotoUrl { get; set; }

        public string Photo { set; get; }
  
        [Required]
        [DataType(DataType.Date)]
        public DateTime HireDate { set; get; }

        [Required]
        public int MaxDayOff { get; set; }

        public int AbsenceDays { get; set; } = 0;


        public int SubjectId { get; set; }

        public string? SubjectName { get; set; }

        public string? IdentityUserId { set; get; }

    }
}

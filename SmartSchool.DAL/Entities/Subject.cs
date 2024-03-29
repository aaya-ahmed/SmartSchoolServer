﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.DAL.Entities
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
       
        public int GradeYearId { get; set; }

        //[InverseProperty("Subject")]
        //public virtual ICollection<ExamsResult> ExamsResults { get; set; } = new List<ExamsResult>();

        [ForeignKey("GradeYearId")]
        [InverseProperty("Subjects")]
        public virtual GradeYear GradeYear { get; set; }

        //[InverseProperty("Subject")]
        //public virtual ICollection<MaterialLibrary> MaterialLibraries { get; set; } = new List<MaterialLibrary>();

        //[InverseProperty("Subject")]
        //public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

        [InverseProperty("Subject")]
        public virtual ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();

        public virtual ICollection<Material> Materials { get; set; } = new List<Material>();
    }
}

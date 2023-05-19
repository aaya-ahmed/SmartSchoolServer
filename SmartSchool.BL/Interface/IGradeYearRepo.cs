﻿using SmartSchool.BL.ViewModel;
using SmartSchool.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.BL.Interface
{
    public interface IGradeYearRepo
    {
        public GradeYear Create(GradeYearVM obj);

        public IEnumerable<GradeYearVM> GetAll();

        public GradeYearVM GetById(int id);

        public void Delete(int id);
    }
}

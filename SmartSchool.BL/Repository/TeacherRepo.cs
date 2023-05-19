using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SmartSchool.BL.Helpers;
using SmartSchool.BL.Interface;
using SmartSchool.BL.ViewModel;
using SmartSchool.DAL.Context;
using SmartSchool.DAL.Entities;
using SmartSchool.DAL.OurEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SmartSchool.BL.Repository
{
    public class TeacherRepo : ITeacherRepo
    {
        public TeacherRepo(SmartSchoolContext db,UserManager<IdentityUser> user)
        {
            Db = db;
            User = user;
        }
        public SmartSchoolContext Db { get; }
        public UserManager<IdentityUser> User { get; }

        public string SaveInDb(TeacherVM obj, string TeacherIdentityId)
        {
            Teacher T = new Teacher()
            {
                Id = Guid.NewGuid().ToString(),
                FullName = obj.FullName,
                Gender = obj.Gender,
                Phone = obj.Phone,
                Salary = obj.Salary,
                SubjectId = obj.SubjectId,
                Address = obj.Address,
                PhotoUrl = obj.PhotoUrl,
                HireDate = obj.HireDate,
                AbsenceDays = obj.AbsenceDays,
                MaxDayOff=obj.MaxDayOff,
                IdentityUserId = TeacherIdentityId,
            };

            Db.Teachers.Add(T);
            Db.SaveChanges();

            return T.Id;
        }
        public IEnumerable<TeacherVM> GetAll()
        {
            var allTeachers = Db.Teachers.Include(t=>t.Subject).Include(t=>t.IdentityUser).Select(obj => new TeacherVM()
            {
                Id = obj.Id,
                FullName = obj.FullName,
                Gender = obj.Gender,
                Phone = obj.Phone,
                Salary = obj.Salary,
                SubjectId = obj.SubjectId,
                Address = obj.Address,
                PhotoUrl = obj.PhotoUrl,
                HireDate = obj.HireDate,
                SubjectName = obj.Subject.Name,
                AbsenceDays = obj.AbsenceDays,
                MaxDayOff=obj.MaxDayOff,
                Email=obj.IdentityUser.Email

            });

            return allTeachers;
        }
        public TeacherVM GetById(string id)
        {
            var myTeacher = Db.Teachers.Include("Subject").Where(r => r.Id == id).Select(obj => new TeacherVM()
            {
                Id = obj.Id,
                FullName = obj.FullName,
                Gender = obj.Gender,
                Phone = obj.Phone,
                Salary = obj.Salary,
                SubjectId = obj.SubjectId,
                Address = obj.Address,
                PhotoUrl = obj.PhotoUrl,
                HireDate = obj.HireDate,
                SubjectName = obj.Subject.Name,
                AbsenceDays = obj.AbsenceDays,


            }).FirstOrDefault();

            return myTeacher;
        }

        public void Delete(string id)
        {
            var myTeacher = Db.Teachers.Find(id);
            Db.Teachers.Remove(myTeacher);
            Db.SaveChanges();
        }

        public Teacher Edit(TeacherVMEdit obj)
        {
           

            Teacher t = Db.Teachers.Find(obj.Id);

            t.Id = obj.Id ;
            t.FullName = obj.FullName ;
            t.Gender = obj.Gender ;
            t.Phone = obj.Phone ;
            t.Salary = obj.Salary ;
            t.Address = obj.Address ;
            t.HireDate = obj.HireDate ;
            t.MaxDayOff=obj.MaxDayOff;
            t.AbsenceDays = obj.AbsenceDays;

            if (obj.Photo != null)
            {
                t.PhotoUrl = UploadFile.Photo(obj.Photo, "TeacherImages");
            }

            Db.SaveChanges();
            return t;


        }

        public IEnumerable<SessionVM> GetSessions(string identity)
        {
            var teacher = Db.Teachers.Where(x => x.IdentityUserId == identity).FirstOrDefault();

            var teacherSessions = Db.Sessions.Where(s => s.TeacherID == teacher.Id).Select(x => new SessionVM()
            {
                Id = x.Id,
                TeacherID=x.TeacherID,
                SubjectName=x.Teacher.Subject.Name,
                TeacherName=x.Teacher.FullName,
                SessionNo=x.SessionNo,
                ScheduleDay=x.Schedule.Day.ToString(),
                ClassName = x.Schedule.Class.Name
            }
            );
            return teacherSessions;
        }
    }
}

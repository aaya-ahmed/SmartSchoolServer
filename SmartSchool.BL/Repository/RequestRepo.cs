//using JWT.Models;
//using JWT.Services;
using SmartSchool.BL.Interface;
using SmartSchool.BL.Models;
using SmartSchool.BL.Services;
using SmartSchool.BL.ViewModel;
using SmartSchool.DAL.Context;
using SmartSchool.DAL.Entities;
using SmartSchool.DAL.OurEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;


namespace SmartSchool.BL.Repository
{
    public class RequestRepo : IRequestRepo
    {
        public SmartSchoolContext Db { get; }
        public IAuthService AuthService { get; }

        public RequestRepo(SmartSchoolContext db, IAuthService authService)
        {
            Db = db;
            AuthService = authService;
        }


        public int Create(RequestVM obj)
        {
            Request r1 = new Request()
             {
                id = obj.id,
                StudentEmail = obj.StudentEmail,
                StudentFirstName = obj.StudentFirstName,
                StudentGender = (Gender)Enum.Parse(typeof(Gender), obj.StudentGender.ToLower()),
                StudentPhone = obj.StudentPhone,
                StudentBirthDate = obj.StudentBirthDate,
                Address= obj.Address,
                ParentFullName = obj.ParentFullName,
                ParentEmail = obj.ParentEmail,
                ParentPhone = obj.ParentPhone,
                StudentPhotoUrl = obj.StudentPhotoUrl,
                StudentBirthCertPhotoUrl = obj.StudentBirthCertPhotoUrl,
                IdentityParentPhotoUrl = obj.IdentityParentPhotoUrl,
                Password = obj.Password,
                
            };
            
            Db.Requests.Add(r1);
            Db.SaveChanges();

            return r1.id;
        }

        public IEnumerable<RequestVM> GetAll()
        {
            var allRequests = Db.Requests.Select(obj => new RequestVM()
            {
                id = obj.id,
                StudentEmail = obj.StudentEmail,
                StudentFirstName = obj.StudentFirstName,

                StudentGender = obj.StudentGender.ToString(),
                StudentPhone = obj.StudentPhone,
                StudentBirthDate = obj.StudentBirthDate,
                Address = obj.Address,
                ParentFullName = obj.ParentFullName,
                ParentEmail = obj.ParentEmail,
                ParentPhone = obj.ParentPhone,
                StudentPhotoUrl = obj.StudentPhotoUrl,
                StudentBirthCertPhotoUrl = obj.StudentBirthCertPhotoUrl,
                IdentityParentPhotoUrl = obj.IdentityParentPhotoUrl,

                //momken nshelo b3d kda
                Password = obj.Password,
                
            });

            return allRequests;
        }

        public RequestVM GetById(int id)
        {
            var myRequest = Db.Requests.Where(r => r.id == id).Select(obj => new RequestVM()
            {
                id = obj.id,
                StudentEmail = obj.StudentEmail,
                StudentFirstName = obj.StudentFirstName,
                StudentGender = obj.StudentGender.ToString(),
                StudentPhone = obj.StudentPhone,
                StudentBirthDate = obj.StudentBirthDate,
                Address = obj.Address,
                ParentFullName = obj.ParentFullName,
                ParentEmail = obj.ParentEmail,
                ParentPhone = obj.ParentPhone,
                StudentPhotoUrl = obj.StudentPhotoUrl,
                StudentBirthCertPhotoUrl = obj.StudentBirthCertPhotoUrl,
                IdentityParentPhotoUrl = obj.IdentityParentPhotoUrl,
                
                //momken nshelo b3d kda
                Password = obj.Password,
            }).FirstOrDefault();

            return myRequest;
        }

        public void SaveInDb(int id,string ParentIdentityId, string StudentIdentityId)
        {
            var myRequest = GetById(id);

            Parent P = new Parent()
            {
                Id = Guid.NewGuid().ToString(),
                ParentFullName = myRequest.ParentFullName,
                IdentityParentPhotoUrl = myRequest.IdentityParentPhotoUrl,
                ParentPhone=myRequest.ParentPhone,
                IdentityUserId = ParentIdentityId,
            };
            Db.Parents.Add(P);
            Db.SaveChanges();
            string parentId = P.Id;


            Student S = new Student()
            {   Id = Guid.NewGuid().ToString(),
                StudentFirstName = myRequest.StudentFirstName +" "+ myRequest.ParentFullName,
                ParentID = parentId,
                Address = myRequest.Address,
                Gender = (Gender)Enum.Parse(typeof(Gender), myRequest.StudentGender.ToLower()),
                StudentPhone = myRequest.StudentPhone,
                StudentBirthDate = myRequest.StudentBirthDate,
                StudentPhotoUrl = myRequest.StudentPhotoUrl,
                StudentBirthCertPhotoUrl = myRequest.StudentBirthCertPhotoUrl,
                IdentityUserId = StudentIdentityId,
            };
            Db.Students.Add(S);
            Db.SaveChanges();
        }

        public void Delete(int id)
        {
            var RemoveRequest = Db.Requests.Find(id);
            Db.Requests.Remove(RemoveRequest);
            Db.SaveChanges();
        }


    }



}


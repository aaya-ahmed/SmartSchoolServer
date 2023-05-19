using Microsoft.AspNetCore.Mvc;
using SmartSchool.BL.Helpers;
using SmartSchool.BL.ViewModel;
using SmartSchool.DAL.Context;

namespace SmartSchool.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        readonly SmartSchoolContext Db;
        public MaterialsController(SmartSchoolContext db)
        {
            Db = db;
        }
        [HttpPost, DisableRequestSizeLimit]
        [Route("upload")]
        
        public async Task<IActionResult> UploadMaterial()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                if (formCollection.Files.First().Length > 0)
                {

                UploadFile.Material(formCollection,Db);
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> Get()
        {
            var materials = Db.Materials.Select(material => new MaterialVM()
            {
                SubjectId=material.SubjectId,
                SubjectName = material.Subject.Name,
                Type = material.Type,
                Path=material.Path,

            }
            );
            return Ok(materials);
        } 
        [HttpGet]
        [Route("getbysubject/{subjectid}/{type}")]
        public async Task<IActionResult> GetBySubject(int subjectid, string type)
        {

            var Material = Db.Materials.Where(m => m.SubjectId == subjectid && m.Type == type)
                .Select(material => new MaterialVM()
                {
                    SubjectId = material.SubjectId,
                    SubjectName = material.Subject.Name,
                    Type = material.Type,
                    Path = material.Path,

                });
            return Ok(Material);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewZealand.Interface;
using NewZealand.Models.Domain;
using NewZealand.Models.DTO;

namespace NewZealand.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;
        public ImagesController(IImageRepository imageRepository){
            this.imageRepository = imageRepository;
        }

       //post
       [HttpPost]
       [Route("Upload")]
       public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDTO request ){
            ValidateFileUpload(request);
            if(ModelState.IsValid){
                //Convert DTO to Domain model
                var imageDomainModel = new Image{
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription
                };
                //User repository to upload image
                await imageRepository.Upload(imageDomainModel);
                return Ok(imageDomainModel);
            }
            return BadRequest(ModelState);
       }
       private void ValidateFileUpload(ImageUploadRequestDTO request){
        var allowedExtension = new string[] {".jpg", ".jpeg", ".png"};
        if(! allowedExtension.Contains(Path.GetExtension(request.File.FileName))){
            ModelState.AddModelError("file", "Unsupported file extension");
        }
        if(request.File.Length > 10485760){
            ModelState.AddModelError("file", "File size more than 10MB, please upload smaller size file");
        }
       }
    }
}
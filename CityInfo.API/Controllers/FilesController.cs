﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CityInfo.API.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;

        public FilesController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider) { 
            _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider??
                throw new System.ArgumentException(nameof(fileExtensionContentTypeProvider));
                ;

        }

        [HttpGet("{fileId}")]
        public ActionResult GetFile(String fileId) {

            var pathToFile = "Assignment.pdf";
            if (!System.IO.File.Exists(pathToFile)) { 
                return NotFound();
            }

            if (!_fileExtensionContentTypeProvider.TryGetContentType(pathToFile, out var contentType)) {
                contentType = "application/octet-stream"; //this is defult type 
            }


            var bytes=System.IO.File.ReadAllBytes(pathToFile);
            return File(bytes, contentType, Path.GetFileName(pathToFile));
        }
    }
}

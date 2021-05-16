using CC.Yi.Common;
using CC.Yi.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CC.Yi.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        #region 上传图片 OnPostUpload
        [HttpPost]
        [Obsolete]
        public async Task<IActionResult> OnPostUploadImage([FromServices] IHostingEnvironment environment)
        {
            List<imageEdit> fileUrl = new List<imageEdit>();
            var files = Request.Form.Files;
            if (string.IsNullOrWhiteSpace(environment.WebRootPath))
            {
                environment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }

            string webRootPath = environment.WebRootPath;
            string filePath = Path.Combine(webRootPath + "\\upload\\images");
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var ext = Path.GetExtension(formFile.FileName);

                    var fileName = Guid.NewGuid().ToString() + ext;
                    var path = Path.Combine(webRootPath + "\\upload\\images", fileName);
                    using (var stream = new FileStream(path, FileMode.CreateNew))
                    {
                        await formFile.CopyToAsync(stream);
                        fileUrl.Add(new imageEdit() { url = $"#/File/ShowNoticeImg?filePath={fileName}", alt = "", href = "" });
                        stream.Close();
                    }
                }
            }
            return new JsonResult(new { errno=0,data= fileUrl });
        }
        #endregion

        #region 上传视频 OnPostUploadVideo 只支持单视频上传
        [HttpPost]
        [Obsolete]
        public async Task<IActionResult> OnPostUploadVideo([FromServices] IHostingEnvironment environment)
        {
           videoEdit fileUrl = new videoEdit();
            var files = Request.Form.Files;
            if (string.IsNullOrWhiteSpace(environment.WebRootPath))
            {
                environment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }

            string webRootPath = environment.WebRootPath;
            string filePath = Path.Combine(webRootPath + "\\upload\\videos");
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var ext = Path.GetExtension(formFile.FileName);
                    var fileName = Guid.NewGuid().ToString() + ext;
                    var path = Path.Combine(webRootPath + "\\upload\\videos", fileName);
                    using (var stream = new FileStream(path, FileMode.CreateNew))
                    {
                        await formFile.CopyToAsync(stream);
                        fileUrl.url = $"#/upload/videos/{fileName}";
                        stream.Flush();
                        stream.Close();
                    }
                }
            }

            return new JsonResult(new { errno=0,data=fileUrl});
        }
        #endregion

        [HttpGet]
        #region 获取图片流  ShowNoticeImg
        public IActionResult ShowNoticeImg(string filePath)
        {
            try
            {
                var contentTypeStr = "image/jpeg";
                string webRootPath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\upload\\images\\{filePath}");
                using (var sw = new FileStream(webRootPath, FileMode.Open))
                {
                    var bytes = new byte[sw.Length];
                    sw.Read(bytes, 0, bytes.Length);
                    sw.Close();
                    return new FileContentResult(bytes, contentTypeStr);
                }
            }
            catch
            {
                return Content("200");
            }
        }
        #endregion

        //#region 文件上传
        //[HttpPost]
        //[Obsolete]
        //public async Task<IActionResult> upLoadFile([FromServices] IHostingEnvironment environment)
        //{
        //    videoEdit fileUrl = new videoEdit();
        //    var files = Request.Form.Files;
        //    if (string.IsNullOrWhiteSpace(environment.WebRootPath))
        //    {
        //        environment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        //    }

        //    string webRootPath = environment.WebRootPath;
        //    string filePath = Path.Combine(webRootPath + "\\upload\\videos");
        //    if (!Directory.Exists(filePath))
        //    {
        //        Directory.CreateDirectory(filePath);
        //    }

        //    foreach (var formFile in files)
        //    {
        //        if (formFile.Length > 0)
        //        {
        //            var ext = Path.GetExtension(formFile.FileName);
        //            var fileName = Guid.NewGuid().ToString() + ext;
        //            var path = Path.Combine(webRootPath + "\\upload\\videos", fileName);
        //            using (var stream = new FileStream(path, FileMode.CreateNew))
        //            {
        //                await formFile.CopyToAsync(stream);
        //                fileUrl.url = $"/dev-apis/upload/videos/{fileName}";
        //                stream.Flush();
        //                stream.Close();
        //            }
        //        }
        //    }

        //    return new JsonResult(new { errno = 0, data = fileUrl });
        //}
        //#endregion
    }
}
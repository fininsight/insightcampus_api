using System;
using System.Threading.Tasks;
using insightcampus_api.Dao;
using insightcampus_api.Data;
using insightcampus_api.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace insightcampus_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassController : Controller
    {
        private readonly ClassInterface _class;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IConfiguration _config;
        private static IAmazonS3 s3Client;
        private static AWSCredentials credential;
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.APNortheast2;

        public ClassController(ClassInterface __class, IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _class = __class;
            _hostingEnvironment = hostingEnvironment;
            _config = configuration;
        }

        [HttpGet("{size:int}/{pageNumber:int}")]
        //public async Task<ActionResult<DataTableOutDto>> Get([FromQuery(Name = "f")] string f, int size, int pageNumber)
        public async Task<ActionResult<DataTableOutDto>> Get([FromQuery(Name = "f")] string f, int size, int pageNumber)
        {
            //List<Filter> filters = new List<Filter>();
            var filters = JsonConvert.DeserializeObject<List<Filter>>(f);
            DataTableInputDto dataTableInputDto = new DataTableInputDto();
            dataTableInputDto.size = size;
            dataTableInputDto.pageNumber = pageNumber;

            return await _class.Select(dataTableInputDto, filters);
        }


        [HttpGet("{class_seq}")]
        public async Task<ActionResult<ClassModel>> Get(int class_seq)
        {
            return await _class.Select(class_seq);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ClassModel classes)
        {
            await _class.Add(classes);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{seq}")]
        public async Task<ActionResult> Put(int seq, [FromBody] ClassModel classes)
        {
            classes.class_seq = seq;
            await _class.Update(classes);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{seq}")]
        public async Task<ActionResult> Delete(int seq)
        {
            ClassModel classes = new ClassModel
            {
                class_seq = seq
            };
            await _class.Delete(classes);
            return Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpPut("template")]
        public async Task<ActionResult> TemplateUpdate([FromBody]ClassModel classModel)
        {
            await _class.UpdateTemplate(classModel);
            return Ok();
        }

        
        [HttpPut("thumbnail/{seq}")]
        [Produces("application/json")]
        public async Task<ActionResult> ThumbnailUpdate(int seq)
        {
            var theFile = HttpContext.Request.Form.Files.GetFile("file");

            // Get File Extension
            // .png
            string extension = System.IO.Path.GetExtension(theFile.FileName);

            // Generate Random name.
            // 1e199075.png
            string name = Guid.NewGuid().ToString().Substring(0, 8) + extension;

            const string bucketName = "insightcampus";
            const string awsLink = "https://insightcampus.s3.ap-northeast-2.amazonaws.com";
            const string imgGroupFolder = "thumbnail_class";

            string year = DateTime.Now.ToString("yyyy");
            string month = DateTime.Now.ToString("MM");

            try
            {
                string accessKey = _config.GetValue<string>("AWS:AccessKey");
                string secretKey = _config.GetValue<string>("AWS:SecretKey");
                credential = new BasicAWSCredentials(accessKey, secretKey);
                s3Client = new AmazonS3Client(credential, bucketRegion);

                var filePath = Path.GetTempFileName();

                using (var stream = System.IO.File.Create(filePath))
                {
                    await theFile.CopyToAsync(stream);
                }

                var fileTransferUtility = new TransferUtility(s3Client);
                await fileTransferUtility.UploadAsync(filePath, bucketName, imgGroupFolder + "/" + year + "/" + month + "/" + name);
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine(
                        "Error encountered ***. Message:'{0}' when writing an object"
                        , e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(
                    "Unknown encountered on server. Message:'{0}' when writing an object"
                    , e.Message);
            }

            var fileLink = awsLink + "/" + imgGroupFolder + "/" + year + "/" + month + "/" + name;
            ClassModel classes = new ClassModel
            {
                class_seq = seq,
                thumbnail = fileLink
            };
            await _class.UpdateThumbnail(classes);

            return Ok();
        }


        [HttpPost("thumbnail")]
        [Produces("application/json")]
        public async Task<string> ThumbnailUpdate()
        {
            var theFile = HttpContext.Request.Form.Files.GetFile("file");

            // Get File Extension
            // .png
            string extension = System.IO.Path.GetExtension(theFile.FileName);

            // Generate Random name.
            // 1e199075.png
            string name = Guid.NewGuid().ToString().Substring(0, 8) + extension;

            const string bucketName = "insightcampus";
            const string awsLink = "https://insightcampus.s3.ap-northeast-2.amazonaws.com";
            const string imgGroupFolder = "thumbnail_class";

            string year = DateTime.Now.ToString("yyyy");
            string month = DateTime.Now.ToString("MM");

            try
            {
                string accessKey = _config.GetValue<string>("AWS:AccessKey");
                string secretKey = _config.GetValue<string>("AWS:SecretKey");
                credential = new BasicAWSCredentials(accessKey, secretKey);
                s3Client = new AmazonS3Client(credential, bucketRegion);

                var filePath = Path.GetTempFileName();

                using (var stream = System.IO.File.Create(filePath))
                {
                    await theFile.CopyToAsync(stream);
                }

                var fileTransferUtility = new TransferUtility(s3Client);
                await fileTransferUtility.UploadAsync(filePath, bucketName, imgGroupFolder + "/" + year + "/" + month + "/" + name);
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine(
                        "Error encountered ***. Message:'{0}' when writing an object"
                        , e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(
                    "Unknown encountered on server. Message:'{0}' when writing an object"
                    , e.Message);
            }

            var fileLink = awsLink + "/" + imgGroupFolder + "/" + year + "/" + month + "/" + name;

            return fileLink;
        }
    }
}

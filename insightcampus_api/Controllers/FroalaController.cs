using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Collections;
using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using Amazon.Runtime;
using Microsoft.Extensions.Configuration;

namespace insightcampus_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FroalaController : Controller
    {

        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IConfiguration _config;
        private static IAmazonS3 s3Client;
        private static AWSCredentials credential;


        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.APNortheast2;

        public FroalaController(IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _config = configuration;
        }

        [HttpPost("upload/community")]
        [Produces("application/json")]
        public async Task<IActionResult> PostCommunity()
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
            const string imgGroupFolder = "community";

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

            Hashtable imageUrl = new Hashtable();
            imageUrl.Add("link", fileLink);
            return Json(imageUrl);
        }

        [HttpPost("upload/class")]
        [Produces("application/json")]
        public async Task<IActionResult> PostClass()
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
            const string imgGroupFolder = "class";

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

            Hashtable imageUrl = new Hashtable();
            imageUrl.Add("link", fileLink);
            return Json(imageUrl);
        }

        [HttpPost("upload/qna")]
        [Produces("application/json")]
        public async Task<IActionResult> PostQna()
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
            const string imgGroupFolder = "qna";

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

            Hashtable imageUrl = new Hashtable();
            imageUrl.Add("link", fileLink);
            return Json(imageUrl);
        }

        [HttpPost("upload/notice")]
        [Produces("application/json")]
        public async Task<IActionResult> PostNotice()
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
            const string imgGroupFolder = "notice";

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

            Hashtable imageUrl = new Hashtable();
            imageUrl.Add("link", fileLink);
            return Json(imageUrl);
        }

        [HttpPost("upload/review")]
        [Produces("application/json")]
        public async Task<IActionResult> PostReview()
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
            const string imgGroupFolder = "review";

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

            Hashtable imageUrl = new Hashtable();
            imageUrl.Add("link", fileLink);
            return Json(imageUrl);
        }

    }
}

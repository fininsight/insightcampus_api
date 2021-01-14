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

namespace insightcampus_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FroalaController : Controller
    {

        private readonly IHostingEnvironment _hostingEnvironment;
        private static IAmazonS3 s3Client;
        private static AWSCredentials credential;


        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.APNortheast2;

        public FroalaController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("UploadFiles")]
        [Produces("application/json")]
        public async Task<IActionResult> Post()
        {
            var theFile = HttpContext.Request.Form.Files.GetFile("file");

            // Get File Extension
            // .png
            string extension = System.IO.Path.GetExtension(theFile.FileName);

            // Generate Random name.
            // 1e199075.png
            string name = Guid.NewGuid().ToString().Substring(0, 8) + extension;

            try
            {
                credential = new BasicAWSCredentials("AKIA2SJOFBZFF7O6ELWH", "zANo/mxTh17v3hMX2V1WhgO4hZf0pkhTr6++OzMZ");
                s3Client = new AmazonS3Client(credential, bucketRegion);

                var filePath = Path.GetTempFileName();

                using (var stream = System.IO.File.Create(filePath))
                {
                    await theFile.CopyToAsync(stream);
                }

                var fileTransferUtility = new TransferUtility(s3Client);                
                await fileTransferUtility.UploadAsync(filePath, "insight-community-image", "uploads/" + name);
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

            var fileLink = "https://insight-community-image.s3.ap-northeast-2.amazonaws.com/uploads/" + name;

            Hashtable imageUrl = new Hashtable();
            imageUrl.Add("link", fileLink);
            return Json(imageUrl);
        }
    }
}

using Amazon;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;

using System;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Linq;
using insightcampus_api.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using System.Collections.Generic;

namespace insightcampus_api.Dao
{
    public class EmailRepository: EmailInterface
    {
        private readonly DataContext _context;

        public EmailRepository(DataContext context)
        {
            _context = context;
        }


        public async Task SendEmail()
        {
            var serverInfo = (
                        from code in _context.CodeContext
                       where code.codegroup_id == "mailserver" && code.code_id == "common"
                      select code).FirstOrDefault();

            SmtpClient client = new SmtpClient(serverInfo.value4, int.Parse(serverInfo.value5));
            client.UseDefaultCredentials = false; // 시스템에 설정된 인증 정보를 사용하지 않는다.
            client.EnableSsl = true;  // SSL을 사용한다.
            client.DeliveryMethod = SmtpDeliveryMethod.Network; // 이걸 하지 않으면 Gmail에 인증을 받지 못한다.
            client.Credentials = new System.Net.NetworkCredential(serverInfo.value1, serverInfo.value2);

            MailAddress from = new MailAddress(serverInfo.value1, serverInfo.value3, System.Text.Encoding.UTF8);
            MailAddress to = new MailAddress("page@fininsight.co.kr");
            MailMessage message = new MailMessage(from, to);

            // requestData.ReceiverList = receiverString;

            message.Body = "인사이트캠퍼스 새로운 사이트의 메일연동 테스트입니다.";
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = "인사이트캠퍼스 메일연동테스트. 서버정보 DB에서~";
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;

            try
            {
                // 동기로 메일을 보낸다.
                client.Send(message);
                // Clean up.
                message.Dispose();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task SendEmail(string to, string subject, string body, string[] bccs)
        {
            var serverInfo = (
                        from code in _context.CodeContext
                       where code.codegroup_id == "mailserver" && code.code_id == "common"
                      select code).FirstOrDefault();            

            var credentials = new Amazon.Runtime.BasicAWSCredentials();
            var region = RegionEndpoint.APNortheast2;

            using (var client = new AmazonSimpleEmailServiceClient(credentials, region))
            {
                var sendRequest = new SendEmailRequest
                {
                    Source = serverInfo.value3 + "<" + serverInfo.value1 + ">",                    
                    Destination = new Destination
                    {
                        ToAddresses = new List<string> { to },
                        CcAddresses = new List<string> { bccs[0] }
                    },
                    Message = new Message
                    {
                        Subject = new Content(subject),
                        Body = new Body
                        {
                            Html = new Content
                            {
                                Charset = "UTF-8",
                                Data = body
                            }
                        }
                    }
                };

                try
                {
                    var response = await client.SendEmailAsync(sendRequest);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error message: " + ex.Message);
                }
            }

        }

        public async Task SendEmail(string to, string subject, string body, string file_path, string file_name, string[] bccs)
        {
            var serverInfo = (
                        from code in _context.CodeContext
                        where code.codegroup_id == "mailserver" && code.code_id == "common"
                        select code).FirstOrDefault();

            string from = serverInfo.value1;
            string _to = to;

            var credentials = new Amazon.Runtime.BasicAWSCredentials();
            var region = RegionEndpoint.APNortheast2;

            System.IO.FileStream fs = new System.IO.FileStream(file_path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            Attachment data = new Attachment(fs, file_name, "application/pdf");

            using (var client = new AmazonSimpleEmailServiceClient(credentials, region))
            {
                var sendRequest = new SendEmailRequest
                {
                    Source = from,
                    Destination = new Destination
                    {
                        ToAddresses = new List<string> { to }
                    },
                    Message = new Message
                    {
                        Subject = new Content(subject),
                        Body = new Body
                        {

                            Html = new Content
                            {
                                Charset = "UTF-8",
                                Data = body
                            }

                        }
                    }
                };

                try
                {
                    var response = client.SendEmailAsync(sendRequest);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error message: " + ex.Message);
                }
            }
        }
    }
}

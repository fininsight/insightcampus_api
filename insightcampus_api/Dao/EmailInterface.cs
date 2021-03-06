﻿using System;
using System.Threading.Tasks;

namespace insightcampus_api.Dao
{
    public interface EmailInterface
    {
        Task SendEmail();
        Task SendEmail(string to, string subject, string body, string[] bccs);
        Task SendEmail(string to, string subject, string body, string file_path, string file_name, string[] bccs);
    }
}

using System;
using System.Text;
using insightcampus_api.Model;

namespace insightcampus_api.Utility
{
    public static class CertificationGenerator
    {
        public static string GetHTMLString(ClassStudentModel classStudent)
        {
            var order_id = classStudent.order_id;
            var order_item_seq = classStudent.order_item_seq;
            var user_name = classStudent.name;
            var class_name = classStudent.class_nm;
            var start_date = classStudent.start_date;
            var end_date = classStudent.end_date;

            var sb = new StringBuilder();
            sb.AppendFormat($@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='user-name'>
                                    <h1>{user_name}</h1>
                                </div>
                                <div class='class-name'>
                                    <h2>{class_name}</h2>
                                </div>
                                <div class='class-duration'>
                                    <h2>{start_date.ToString("yyyy. MM. dd")} - {end_date.ToString("yyyy. MM. dd")}</h2>
                                </div>
                                <div class='class-end-date'>
                                    <h2>{end_date.ToString("yyyy. MM. dd")}</h2>
                                </div>
                                <div class='certification-id'>
                                    <h3>{order_id}-{order_item_seq}</h3>
                                </div>
                            </body>
                        </html>
            ");      

            return sb.ToString();
        } 
    }
}
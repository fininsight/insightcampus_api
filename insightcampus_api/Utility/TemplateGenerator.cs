using System.Text;

namespace insightcampus_api.Utility
{
    public static class TemplateGenerator
    {
        public static string GetHTMLString()
        {
            var sb = new StringBuilder();
            sb.Append(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'>
                                    <div class='title'>
                                      <h1>11월 멘토링 지급 명세서</h1>
                                    </div>
                                    <div class='explane'>
                                      <p>안녕하세요<b> 이진범님</b></p>
                                      <p><b>11월 멀티캠퍼스 혁신성장 인공지능 B반 멘토링</b>에 대한 정산내역입니다.</p>
                                      <p>감사합니다.</p>
                                    </div>
                                  </div>
                                  <br />
                                  <br />
                                  <div class='content'>
                                    <h1>[11월 교육과정 총 입금액]</h1>
                                    <table>
                                      <thead>
                                        <tr>
                                          <td>총 예산</td>
                                          <td>원천징수액</td>
                                          <td></td>
                                        </tr>
                                      </thead>
                                      <tbody>
                                        <tr>
                                          <td>8만원* 16시간</td>
                                          <td>(사업소득 3.3%)</td>
                                          <td></td>
                                        </tr>
                                        <tr>
                                          <td></td>
                                          <td></td>
                                          <td></td>
                                        </tr>
                                      </tbody>
                                    </table>
                                    <h5>* 위 금액은 강사님 계좌로 입금되는 금액입니다. 혹시 금액이 다르면 연락 주십시오.</h5>
                                  </div>
                            </body>
                        </html>");

            return sb.ToString();
        }
    }
}
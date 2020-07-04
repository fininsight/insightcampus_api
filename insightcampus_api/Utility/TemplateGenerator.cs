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
                                  <br />
                                  <br />
                                  <div class='content'>
                                    <h1>[11월 교육과정 총 입금액]</h1>
                                    <table border='3' cellpadding='30'>
                                        <tr id='bg-grey'>
                                            <td>총 예산</td>
                                            <td>원천징수액</td>
                                            <td id='bg-lightyellow' rowspan='2'>총 입금액</td>
                                        </tr>
                                        <tr id='bg-grey'>
                                            <td>8만원 * 16시간</td>
                                            <td>(사업소득 3.3%)</td>
                                        </tr>
                                        <tr class='price-line'>
                                            <td>₩1,280,000</td>
                                            <td>₩42,240</td>
                                            <td id='bg-lightyellow'>₩1,237,760</td>
                                        </tr>
                                    </table>
                                    <h5>* 위 금액은 강사님 계좌로 입금되는 금액입니다. 혹시 금액이 다르면 연락 주십시오.</h5>
                                    <br />
                                    <br />
                                    <h1><span id='bg-yellow'>[7월 멘토링비]</span></h1>
                                    <table border='3' cellpadding='30'>
                                        <tr id='bg-grey'>
                                            <td>총 멘토링</td>
                                            <td>원천징수액</td>
                                            <td id='bg-lightyellow' rowspan='2'>실 지급액</td>
                                        </tr>
                                        <tr id='bg-grey'>
                                            <td>3만원 * 16시간</td>
                                            <td>(사업소득 3.3%)</td>
                                        </tr>
                                        <tr class='price-line'>
                                            <td>₩480,000</td>
                                            <td>₩15,840</td>
                                            <td id='bg-lightyellow'>₩464,160</td>
                                        </tr>
                                    </table>
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <h1><span id='bg-yellow'>[핀인사이트로 송금해주실 금액]</span></h1>
                                    <table border='3' cellpadding='30'>
                                        <tr id='bg-grey'>
                                            <td>송금액 계산</td>
                                            <td id='bg-yellow'>송금액</td>
                                        </tr>
                                        <tr>
                                            <td>₩1,237,760 - ₩464,160</td>
                                            <td id='bg-yellow'>₩773,600</td>
                                        </tr>
                                    </table>
                                    <ul class='caution'>
                                      <li>위 송금액을 아래의 계좌로 2주 이내 입금해 주시기 바랍니다.</li>
                                      <span class='account-number'>KB국민 | 277237-04-001089 | (주)핀인사이트</span>
                                      <br />
                                      <br />
                                      <li>송금액은 소득공제를 위해 현금영수증 발급해드립니다.</li>
                                      <li>사업체가 있으신 경우 세금계산서 발급도 가능합니다. (중복 발급은 불가합니다.)</li>
                                    </ul>
                        
                                  </div>
                            </body>
                        </html>");

            return sb.ToString();
        }
    }
}
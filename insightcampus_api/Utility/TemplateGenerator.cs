using System;
using System.Text;
using insightcampus_api.Model;

namespace insightcampus_api.Utility
{
    public static class TemplateGenerator
    {
        public static string GetHTMLString(IncamAddfareModel incamAddfare)
        {   
            var name = incamAddfare.name;
            var all = (float)incamAddfare.hour_price * incamAddfare.hour;
            var all_tax = Math.Truncate(all * incamAddfare.rate / 10) * 10;
            var hour = incamAddfare.hour;
            //var hour_incen = incamAddfare.hour_incen;
            var hour_price = incamAddfare.hour_price;
            var employee_all = (float)incamAddfare.contract_price * incamAddfare.hour;
            var employee_tax = Math.Truncate(employee_all * incamAddfare.rate /10) * 10;
            var contract_price = incamAddfare.contract_price;
            var remit = (all - all_tax) - (employee_all - employee_tax);
            var class_name = incamAddfare.@class;
            var month = incamAddfare.addfare_date.Month;
            var type = incamAddfare.income_type_nm;
            var lec_wage = incamAddfare.hour_incen / 10000;
            var mnt_wage = incamAddfare.hour_price / 10000;
            var times = incamAddfare.hour;
            var lec_total = (float)lec_wage * 10000 * times;
            var mnt_total = (float)mnt_wage* 10000 * times;
            var tax = 0.033;   //incamAddfare.tax;
            var lec_calc = lec_total * (1 - tax);
            var mnt_calc = mnt_total * (1 - tax);
            // var remit = 10; //incamAddfare.remit;
            var bank = "KB국민";
            var account_num = "277237-04-001089";

            var sb = new StringBuilder();
            sb.AppendFormat(@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'>
                                    <div class='title'>
                                      <h1>{0}월 {1} 지급 명세서</h1>
                                    </div>
                                    <div class='explane'>
                                      <p>안녕하세요<b> {2}님</b></p>
                                      <p><b>{0}월 {3}</b>에 대한 정산내역입니다.</p>
                                      <p>감사합니다.</p>
                                    </div>
                                  </div>
                                  <br />
                                  <br />
                                  <br />
                                  <br />
            ", month, type, name, class_name);
            sb.AppendFormat(@"
                                  <div class='content'>
                                    <h1>[{0}월 교육과정 총 입금액]</h1>
                                    <table border='3' cellpadding='30'>
                                        <tr id='bg-grey'>
                                            <td>총 예산</td>
                                            <td>원천징수액</td>
                                            <td id='bg-lightyellow' rowspan='2'>총 입금액</td>
                                        </tr>
                                        <tr id='bg-grey'>
                                            <td>{1}만원 * {2}시간</td>
                                            <td>(사업소득 {3}%)</td>
                                        </tr>
                                        <tr class='price-line'>
                                            <td>₩{4}</td>
                                            <td>₩{5}</td>
                                            <td id='bg-lightyellow'>₩{6}</td>
                                        </tr>
                                    </table>
                                    <h5>* 위 금액은 강사님 계좌로 입금되는 금액입니다. 혹시 금액이 다르면 연락 주십시오.</h5>
                                    <br />
                                    <br />
                                    <h1><span id='bg-yellow'>[{0}월 멘토링비]</span></h1>
                                    <table border='3' cellpadding='30'>
                                        <tr id='bg-grey'>
                                            <td>총 멘토링</td>
                                            <td>원천징수액</td>
                                            <td id='bg-lightyellow' rowspan='2'>실 지급액</td>
                                        </tr>
                                        <tr id='bg-grey'>
                                            <td>{7}만원 * {2}시간</td>
                                            <td>(사업소득 {3}%)</td>
                                        </tr>
                                        <tr class='price-line'>
                                            <td>₩{8}</td>
                                            <td>₩{9}</td>
                                            <td id='bg-lightyellow'>₩{10}</td>
                                        </tr>
                                    </table>
                                    <br />
                                    <br />
                                    <br />
                                    <br />
            ", month, hour_price / 10000, hour, incamAddfare.rate * 100, ToAccounting(all), ToAccounting(all_tax), ToAccounting(all - all_tax)
            , contract_price / 10000, ToAccounting(employee_all), ToAccounting(employee_tax), ToAccounting(employee_all - employee_tax));
            sb.AppendFormat(@"
                                    <h1><span id='bg-yellow'>[핀인사이트로 송금해주실 금액]</span></h1>
                                    <table border='3' cellpadding='30'>
                                        <tr id='bg-grey'>
                                            <td>송금액 계산</td>
                                            <td id='bg-yellow'>송금액</td>
                                        </tr>
                                        <tr>
                                            <td>₩{0} - ₩{1}</td>
                                            <td id='bg-yellow'>₩{2}</td>
                                        </tr>
                                    </table>
                                    <ul class='caution'>
                                      <li>위 송금액을 아래의 계좌로 2주 이내 입금해 주시기 바랍니다.</li>
                                      <span class='account-number'>{3} | {4} | (주)핀인사이트</span>
                                      <br />
                                      <br />
                                      <li>송금액은 소득공제를 위해 현금영수증 발급해드립니다.</li>
                                      <li>사업체가 있으신 경우 세금계산서 발급도 가능합니다. (중복 발급은 불가합니다.)</li>
                                    </ul>
                        
                                  </div>
                            </body>
                        </html>", ToAccounting(all - all_tax), ToAccounting(employee_all - employee_tax),
                        ToAccounting(remit), bank, account_num);

            return sb.ToString();
        }

        public static string ToAccounting(double money)
        {
            return String.Format("{0:#,0}", money);
        }
    }
}
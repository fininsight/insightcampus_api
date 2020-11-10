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
            var all_tax = Math.Truncate(all * incamAddfare.income / 10) * 10;
            var hour = incamAddfare.hour;
            var income_type_nm = incamAddfare.income_type_nm;
            var hour_price = incamAddfare.hour_price;
            var employee_all = (float)incamAddfare.contract_price * incamAddfare.hour;
            var employee_tax = Math.Truncate(employee_all * incamAddfare.income /10) * 10;
            var contract_price = incamAddfare.contract_price;
            var remit = (all - all_tax) - (employee_all - employee_tax);
            var class_name = incamAddfare.@class;
            var month = incamAddfare.addfare_date.Month;
            var day = incamAddfare.addfare_date.Day;
            var bank = "KB국민";
            var account_num = "277237-04-001089";

            var sb = new StringBuilder();
            sb.AppendFormat($@"
                        <html>
                            <head>
                            </head>
                            <body>
                                <div class='header'>
                                    <div class='title'>
                                      <h1>강의료 지급 명세서</h1>
                                    </div>
                                  <br/>
                                  <br/>
                                  <br/>
                                    <div class='explane'>
                                      <p>안녕하세요, <b>{name}</b>님</p>
                                      <p><b>{month}월 {day}일에 정산된 {class_name}</b>에 대한 내역입니다.</p>
                                    </div>
                                  </div>
                                  <br/>
                                  <br/>
                                  <br/>
            ");


            sb.AppendFormat($@"
                                  <div class='content'>
                                    <h1>[교육과정 총 입금액]</h1>
                                    <table cellpadding='5'>
                                        <tr id='bg-grey'>
                                            <td>총 예산</td>
                                            <td>원천징수액</td>
                                            <td id='bg-lightyellow' rowspan='2'>총 입금액</td>
                                        </tr>
                                        <tr id='bg-grey'>
                                            <td>{hour_price / 10000}만원 * {hour}시간</td>
                                            <td>({income_type_nm} {incamAddfare.income * 100}%)</td>
                                        </tr>
                                        <tr class='price-line'>
                                            <td>₩{ToAccounting(all)}</td>
                                            <td>₩{ToAccounting(all_tax)}</td>
                                            <td id='bg-lightyellow'>₩{ToAccounting(all - all_tax)}</td>
                                        </tr>
                                    </table>
                                    <h5 style='padding-top:5px;'>* 위 금액은 강사님 계좌로 입금되는 금액입니다. 혹시 금액이 다르면 연락 주십시오.</h5>
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <h1><span id='bg-yellow'>[강의료]</span></h1>
                                    <table cellpadding='5'>
                                        <tr id='bg-grey'>
                                            <td>총 강의료</td>
                                            <td>원천징수액</td>
                                            <td id='bg-lightyellow' rowspan='2'>실 지급액</td>
                                        </tr>
                                        <tr id='bg-grey'>
                                            <td>{contract_price / 10000}만원 * {hour}시간</td>
                                            <td>({income_type_nm} {incamAddfare.rate * 100}%)</td>
                                        </tr>
                                        <tr class='price-line'>
                                            <td>₩{ToAccounting(employee_all)}</td>
                                            <td>₩{ToAccounting(employee_tax)}</td>
                                            <td id='bg-lightyellow'>₩{ToAccounting(employee_all - employee_tax)}</td>
                                        </tr>
                                    </table>
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                    <br />
            ");


            sb.AppendFormat($@"
                                    <h1><span id='bg-yellow'>[핀인사이트로 송금해주실 금액]</span></h1>
                                    <table cellpadding='5'>
                                        <tr id='bg-grey'>
                                            <td>송금액 계산</td>
                                            <td id='bg-yellow'>송금액</td>
                                        </tr>
                                        <tr>
                                            <td>₩{ToAccounting(all - all_tax)} - ₩{ToAccounting(employee_all - employee_tax)}</td>
                                            <td id='bg-yellow'>₩{ToAccounting(remit)}</td>
                                        </tr>
                                    </table>
                                    <ul class='caution'>
                                      <li>위 송금액을 아래의 계좌로 입금해 주시기 바랍니다.</li>
                                      <span class='account-number'>{bank} | {account_num} | (주)핀인사이트</span>
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

        public static string ToAccounting(double money)
        {
            return String.Format("{0:#,0}", money);
        }
    }
}
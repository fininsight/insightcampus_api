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

        public static string GetProofHTMLString()
        {
            var name = "test";

            var sb = new StringBuilder();
            sb.AppendFormat($@"
                        <html>
                          <head>
                           </head>
                            <body>
                            <div class='header'>
                                < br />
                                < br />
                                < br />
                                < div class='title'>
                                    <h1>재직증명서</h1>
                                </div>
                                <br />
                                <br />
                                <br />
                            </div>
            ");


            sb.AppendFormat($@"
                            <div class='content'>
                                <div class='divi-line'><span></span></div>
                                <h1>인적사항</h1>
                                <table >
                                    <tr class='br-black'>
                                        <td class='td-1'>이&nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp 름</td>
                                        <td class='td-2'>: {name}</td>
                                    </tr>
                                    <tr>
                                        <td>주민등록번호</td>
                                        <td> : </td>
                                    </tr>
                                    <tr>
                                        <td>주&nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp 소</td>
                                        <td>:</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp연&nbsp &nbsp 락&nbsp &nbsp 처</td>
                                        <td>:</td>
                                    </tr>
                                </table>
                                <br />
                                <br />
                                <br />
                                <h1>재직사항</h1>
                                <table >
                                    <tr class='br-black'>
                                        <td class='td-1'>부&nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp 서</td>
                                        < td class='td-2'>: </td>
                                    </tr>
                                    <tr>
                                        <td>주&nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp 소</td>
                                        <td>: </td>
                                    </tr>
                                    <tr>
                                        <td>직&nbsp &nbsp &nbsp &nbsp &nbsp &nbsp &nbsp 위</td>
                                        <td>: </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp재&nbsp 직&nbsp 기&nbsp 간</td>
                                        <td>:</td>
                                    </tr>
                                    <tr class='br-black-end'>
                                        <td>&nbsp발&nbsp 급&nbsp 용&nbsp 도</td>
                                        <td>:</td>
                                    </tr>
                                </table>
                                <br />
                                <br />
                                <div><p>위와 같이 재직하고 있음을 증명합니다.</p></div>
                                <br />
                                <br />
                                <br />
            ");


            sb.AppendFormat($@"
                                <div><p></p></div>
                                <br />
                                <br />
                                <br />
                                <ul class='caution'>
                                    <li>주&nbsp &nbsp 소 : </li>
                                    <li>회사명 : </li>
                                    <li>대표자 : 이민호 &nbsp &nbsp &nbsp &nbsp (인)</li>
                                </ul>
                                <div class='divi-line'><span></span></div>
                                </ div >
                                </ body >
                                </ html > ");

            return sb.ToString();
        }
  
  
  
  
        public static string GetEduCertificationHTMLString()
        {
            var number = 1;

            var name = "test";

            var start_year = "test";
            var start_month = "test";
            var start_day = "test";
            var end_year = "test";
            var end_month = "test";
            var end_day = "test";
            var term = "test";

            var education_nm = "test";

            var eduCertification_year = "test";
            var eduCertification_month = "test";
            var eduCertification_day = "test";

            var sb = new StringBuilder();
            sb.AppendFormat($@"
                <!DOCTYPE html>
                <html>
                    <head></head>
                    <body>
                        <br><br><br><br><br><br><br>
                        <div class='number'>제 {number} 호</div>
                        <br><br>
                        <h1>교육 수료증</h1>
                        <br><br>
                        <li>성명 : {name}</li>
                        <li>교육 기간 : {start_year}년 {start_month}월 {start_day}일 ~ {end_year}년 {end_month}월 {end_day} (총 {term}시간)</li>
                        <li>교육 명 : {education_nm}</li>
                        <br><br><br><br>
                        <p>위 사람은 (주)핀인사이트 인사이트 캠퍼스가<br>주관한 금융 데이터 사이언스 교육 과정을<br>성실히 수료하였으므로 이 증서를 수여합니다.</p>
                        <br><br><br><br><br><br><br><br>
                        <div class='certification_date'>{eduCertification_year}년 {eduCertification_month}월 {eduCertification_day}일</div>
                        <br><br><br>
                        <div class='bottom'>
                            <span class='bottom_start'>(주) 핀인사이트 대표</span>
                            <span class='bottom_end'>이 민 호</span>
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
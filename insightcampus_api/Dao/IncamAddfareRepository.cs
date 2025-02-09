using System;
using System.Collections.Generic;
using insightcampus_api.Data;
using System.Linq;
using insightcampus_api.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace insightcampus_api.Dao
{
    public class IncamAddfareRepository : IncamAddfareInterface
    {
        private readonly DataContext _context;

        private readonly EmailInterface _email;

        public IncamAddfareRepository(DataContext context, EmailInterface email)
        {
            _context = context;
            _email = email;
        }

        public async Task Add<T>(T entity) where T : class
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(IncamAddfareModel incamAddfareModel)
        {
            var log = await (
                 from addfare in _context.IncamAddfareContext
                where addfare.addfare_seq == incamAddfareModel.addfare_seq
               select new IncamAddfareLogModel
               {
                   log_dt = DateTime.Now,
                   log_user = incamAddfareModel.upd_user,
                   addfare_seq = addfare.addfare_seq,
                   contract_seq = addfare.contract_seq,
                   hour = addfare.hour,
                   addfare_date = addfare.addfare_date,
                   income_type = addfare.income_type,
                   income = addfare.income,
                   reg_user = addfare.reg_user,
                   reg_dt = addfare.reg_dt,
                   upd_user = addfare.upd_user,
                   upd_dt = addfare.upd_dt,
                   use_yn = addfare.use_yn
               }).SingleAsync();

            _context.Add(log);
            incamAddfareModel.addfare_date = incamAddfareModel.addfare_date.ToLocalTime();
            _context.Entry(incamAddfareModel).Property(x => x.addfare_date).IsModified = true;
            _context.Entry(incamAddfareModel).Property(x => x.contract_seq).IsModified = true;
            _context.Entry(incamAddfareModel).Property(x => x.hour).IsModified = true;
            _context.Entry(incamAddfareModel).Property(x => x.income_type).IsModified = true;
            _context.Entry(incamAddfareModel).Property(x => x.income).IsModified = true;
            _context.Entry(incamAddfareModel).Property(x => x.addfare_gubun).IsModified = true;
            _context.Entry(incamAddfareModel).Property(x => x.go_check).IsModified = true;

            await _context.SaveChangesAsync();
        }

        public async Task UpdateDeposit(List<IncamAddfareModel> incamAddfareModels)
        {
            foreach (var incamAddfareModel in incamAddfareModels)
            {
                _context.Entry(incamAddfareModel).Property(x => x.check_yn).IsModified = true;
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateEvidenceType(List<IncamAddfareModel> incamAddfareModels)
        {
            foreach (var incamAddfareModel in incamAddfareModels)
            {
                _context.Entry(incamAddfareModel).Property(x => x.evidence_type).IsModified = true;
            }

            await _context.SaveChangesAsync();
        }

        public async Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto, List<Filter> filters)
        {
            var result = (
                    from addfare in _context.IncamAddfareContext
                    join contract in _context.IncamContractContext
                      on addfare.contract_seq equals contract.contract_seq
                    join teacher in _context.TeacherContext
                      on contract.teacher_seq equals teacher.teacher_seq
                    join company in _context.CodeContext
                      on contract.original_company equals company.code_id
                    join incom in _context.CodeContext
                      on addfare.income_type equals incom.code_id
                    join evidence in _context.CodeContext
                      on new { type = addfare.evidence_type, codegroup_id = "evidence_type" } equals new { type = evidence.code_id, codegroup_id = evidence.codegroup_id } into _t
                    from evidence in _t.DefaultIfEmpty()
                    join addfareGubun in _context.CodeContext
                      on new { type = addfare.addfare_gubun, codegroup_id = "addfare_gubun" } equals new { type = addfareGubun.code_id, codegroup_id = addfareGubun.codegroup_id } into _g
                    from addfareGubun in _g.DefaultIfEmpty()
                    join goCheck in _context.CodeContext
                      on new { type = addfare.go_check, codegroup_id = "go_check" } equals new { type = goCheck.code_id, codegroup_id = goCheck.codegroup_id } into _c
                    from goCheck in _c.DefaultIfEmpty()
                    where company.codegroup_id == "cooperative"
                   where incom.codegroup_id == "incom"
                   where addfare.use_yn == 1
                 orderby addfare.addfare_seq descending
                  select new IncamAddfareModel
                    {
                        addfare_seq = addfare.addfare_seq,
                        contract_seq = addfare.contract_seq,
                        hour = addfare.hour,
                        addfare_date = addfare.addfare_date,
                        income_type = addfare.income_type,
                        evidence_type = addfare.evidence_type,
                        income_type_nm = incom.code_nm,
                        income = addfare.income,
                        original_company_nm = company.code_nm,
                        @class = contract.@class,
                        hour_price = contract.hour_price,
                        hour_incen = contract.hour_incen,
                        contract_price = contract.contract_price,
                        name = teacher.name,
                        teacher_seq = teacher.teacher_seq,
                        check_yn = addfare.check_yn,
                        evidence_type_nm = evidence.code_nm,
                        addfare_gubun = addfareGubun.code_id,
                        addfare_gubun_nm = addfareGubun.code_nm,
                        go_check = goCheck.code_id,
                        go_check_nm = goCheck.code_nm
                  });

            

            foreach (var filter in filters)
            {
                if (filter.k == "name")
                {
                    result = result.Where(w => w.name.Contains(filter.v.Replace(" ", "")));
                }

                else if (filter.k == "company")
                {
                    result = result.Where(w => w.original_company_nm.Contains(filter.v.Replace(" ", "")));
                }

                else if (filter.k == "class")
                {
                    result = result.Where(w => w.@class.Contains(filter.v.Replace(" ", "")));
                }

                else if (filter.k == "start_date")
                {
                    result = result.Where(w => w.addfare_date >= Convert.ToDateTime(filter.v));
                }

                else if (filter.k == "end_date")
                {
                    result = result.Where(w => w.addfare_date <= Convert.ToDateTime(filter.v));
                }

                else if (filter.k == "deposit")
                {
                    result = result.Where(w => w.check_yn == Convert.ToInt16(filter.v));
                }
                else if (filter.k == "evidenceType")
                {
                    if(filter.v == "00")
                    {
                        result = result.Where(w => w.evidence_type == null);
                    }
                    else
                    {
                        result = result.Where(w => w.evidence_type == filter.v);
                    }
                }
            }

            var paging = await result.Skip((dataTableInputDto.pageNumber - 1) * dataTableInputDto.size).Take(dataTableInputDto.size).ToListAsync();

            DataTableOutDto dataTableOutDto = new DataTableOutDto();

            dataTableOutDto.pageNumber = dataTableInputDto.pageNumber;
            dataTableOutDto.size = dataTableInputDto.size;
            dataTableOutDto.data = paging;
            dataTableOutDto.totalPages = (result.Count() % dataTableInputDto.size) > 0 ? result.Count() / dataTableInputDto.size + 1 : result.Count() / dataTableInputDto.size;
            dataTableOutDto.totalElements = result.Count();

            return dataTableOutDto;
        }

        public async Task<List<IncamAddfareModel>> SelectExcel(List<Filter> filters)
        {
            var result = (
                    from addfare in _context.IncamAddfareContext
                    join contract in _context.IncamContractContext
                      on addfare.contract_seq equals contract.contract_seq
                    join teacher in _context.TeacherContext
                      on contract.teacher_seq equals teacher.teacher_seq
                    join company in _context.CodeContext
                      on contract.original_company equals company.code_id
                    join incom in _context.CodeContext
                      on addfare.income_type equals incom.code_id
                    where company.codegroup_id == "cooperative"
                    where incom.codegroup_id == "incom"
                    where addfare.use_yn == 1
                    orderby addfare.addfare_seq descending
                    select new IncamAddfareModel
                    {
                        addfare_seq = addfare.addfare_seq,
                        contract_seq = addfare.contract_seq,
                        hour = addfare.hour,
                        addfare_date = addfare.addfare_date,
                        income_type = addfare.income_type,
                        income_type_nm = incom.code_nm,
                        income = addfare.income,
                        original_company_nm = company.code_nm,
                        @class = contract.@class,
                        hour_price = contract.hour_price,
                        hour_incen = contract.hour_incen,
                        contract_price = contract.contract_price,
                        name = teacher.name,
                        teacher_seq = teacher.teacher_seq
                    });



            foreach (var filter in filters)
            {
                if (filter.k == "name")
                {
                    result = result.Where(w => w.name.Contains(filter.v.Replace(" ", "")));
                }

                else if (filter.k == "company")
                {
                    result = result.Where(w => w.original_company_nm.Contains(filter.v.Replace(" ", "")));
                }

                else if (filter.k == "class")
                {
                    result = result.Where(w => w.@class.Contains(filter.v.Replace(" ", "")));
                }

                else if (filter.k == "start_date")
                {
                    result = result.Where(w => w.addfare_date >= Convert.ToDateTime(filter.v));
                }

                else if (filter.k == "end_date")
                {
                    result = result.Where(w => w.addfare_date <= Convert.ToDateTime(filter.v));
                }
            }

            return await result.ToListAsync();
        }

        public async Task<DataTableOutDto> SelectFamily(DataTableInputDto dataTableInputDto, int teacher_seq)
        {
            var result = (
                    from addfare in _context.IncamAddfareContext
                    join contract in _context.IncamContractContext
                      on addfare.contract_seq equals contract.contract_seq
                    join teacher in _context.TeacherContext
                      on contract.teacher_seq equals teacher.teacher_seq
                    join company in _context.CodeContext
                      on contract.original_company equals company.code_id
                    join incom in _context.CodeContext
                      on addfare.income_type equals incom.code_id
                   where company.codegroup_id == "cooperative"
                   where incom.codegroup_id == "incom"
                   where addfare.use_yn == 1
                   where teacher.teacher_seq == teacher_seq
                 orderby addfare.addfare_seq descending
                  select new IncamAddfareModel
                    {
                        addfare_seq = addfare.addfare_seq,
                        contract_seq = addfare.contract_seq,
                        hour = addfare.hour,
                        addfare_date = addfare.addfare_date,
                        income_type = addfare.income_type,
                        income_type_nm = incom.code_nm,
                        income = addfare.income,
                        original_company_nm = company.code_nm,
                        @class = contract.@class,
                        hour_price = contract.hour_price,
                        hour_incen = contract.hour_incen,
                        contract_price = contract.contract_price,
                        name = teacher.name,
                        teacher_seq = teacher.teacher_seq,
                        check_yn = addfare.check_yn
                  });

            var paging = await result.Skip((dataTableInputDto.pageNumber - 1) * dataTableInputDto.size).Take(dataTableInputDto.size).ToListAsync();

            DataTableOutDto dataTableOutDto = new DataTableOutDto();

            dataTableOutDto.pageNumber = dataTableInputDto.pageNumber;
            dataTableOutDto.size = dataTableInputDto.size;
            dataTableOutDto.data = paging;
            dataTableOutDto.totalPages = (result.Count() % dataTableInputDto.size) > 0 ? result.Count() / dataTableInputDto.size + 1 : result.Count() / dataTableInputDto.size;
            dataTableOutDto.totalElements = result.Count();

            return dataTableOutDto;
        }

        public async Task<IncamAddfareModel> Select(int addfare_seq)
        {
            var result = await (
                      from incam_addfare in _context.IncamAddfareContext
                      where incam_addfare.addfare_seq == addfare_seq
                      select incam_addfare).SingleAsync();

            return result;
        }

        public async Task Delete(IncamAddfareModel incamAddfareModel)
        {
            _context.Entry(incamAddfareModel).Property(x => x.use_yn).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task SendAddfare(List<IncamAddfareModel> incamAddfares)
        {
            foreach(var incamAddfare in incamAddfares)
            {
                var name = incamAddfare.name;
                var all = (float)incamAddfare.hour_price * incamAddfare.hour;
                var all_tax = Math.Truncate(all * incamAddfare.income / 10) * 10;
                var hour = incamAddfare.hour;
                var income_type_nm = incamAddfare.income_type_nm;
                var hour_price = incamAddfare.hour_price;
                var employee_all = (float)incamAddfare.contract_price * incamAddfare.hour;
                var employee_tax = Math.Truncate(employee_all * incamAddfare.income / 10) * 10;
                var contract_price = incamAddfare.contract_price;
                var remit = (all - all_tax) - (employee_all - employee_tax);
                var class_name = incamAddfare.@class;
                var month = incamAddfare.addfare_date.Month;
                var day = incamAddfare.addfare_date.Day;

                var result = await (
                              from teacher in _context.TeacherContext
                              join contract in _context.IncamContractContext
                                on teacher.teacher_seq equals contract.teacher_seq
                            //join addfare in _context.IncamAddfareContext
                              //on contract.contract_seq equals addfare.contract_seq
                              where teacher.teacher_seq == incamAddfare.teacher_seq
                             select new TeacherModel
                             {
                                 teacher_seq = teacher.teacher_seq,
                                 name = teacher.name,
                                 email = teacher.email,
                                 phone = teacher.phone,
                                 address = teacher.address,
                                 user_seq = teacher.user_seq,
                                 passwd = teacher.passwd,
                                 reg_user = teacher.reg_user,
                                 ret_dt = teacher.ret_dt,
                                 upd_user = teacher.upd_user,
                                 upd_dt = teacher.upd_dt,
                                 use_yn = teacher.use_yn,
                            //     addfare_gubun = addfare.addfare_gubun
                             }).FirstOrDefaultAsync();

                string title = $"[핀인사이트] {incamAddfare.@class} 지급명세서 {incamAddfare.addfare_date.ToString("yyyy-MM-dd")}";
                string content = "";

                if (incamAddfare.addfare_gubun == "01")
                {
                    content = $@"
                        <br/>
                        <br/>
                        안녕하세요, <span style='color:blue'>{incamAddfare.name}</span> 님<br/>
                        <br/>
                        {incamAddfare.@class} 과정 강의료 지급명세서 송부합니다.<br/>
                        명세서 확인 후 아래와 같이 송금 요청드립니다.<br/>
                        <br/>
                        - 교육과정 총 입금액 : ₩{ToAccounting(all - all_tax)}<br/>
                        - 강의료 실지급액 : <span style='color:blue;'>₩{ToAccounting(employee_all - employee_tax)}</span> <span style='color:red;'>(세전 {contract_price / 10000}만원 * {hour}시간)</span><br/>
                        - 송금요청액 : <span style='color:blue;'>₩{ToAccounting(remit)}</span><br/>
                        - 송금계좌 : KB국민 | 277237-04-001089 | (주)핀인사이트<br/>
                        <br/>
                        <br/>
                        지급명세서는 아래 링크에서 확인하실수 있습니다. (링크를 공유하지 말아주세요)<br/>
                        <a href='http://49.50.172.5:8080/admin?seq={result.teacher_seq}&password={result.passwd}' target='_blank'>지급명세서 확인</a><br/>
                    ";
                }
                else if (incamAddfare.addfare_gubun == "02")
                {
                    content = $@"
                        <br/>
                        <br/>
                        안녕하세요, <span style='color:blue'>{incamAddfare.name}</span> 님<br/>
                        <br/>
                        {incamAddfare.@class} 과정 강의료 지급명세서 송부합니다.<br/>
                        <br/>
                        - 강의료 실지급액 : <span style='color:blue;'>₩{ToAccounting(employee_all - employee_tax)}</span> <span style='color:red;'>(세전 {contract_price / 10000}만원 * {hour}시간)</span><br/>
                        <br/>
                        <br/>
                        지급명세서는 아래 링크에서 확인하실수 있습니다. (링크를 공유하지 말아주세요)<br/>
                        <a href='http://49.50.172.5:8080/admin?seq={result.teacher_seq}&password={result.passwd}' target='_blank'>지급명세서 확인</a><br/>
                    ";
                }
                else if (incamAddfare.addfare_gubun == "03")
                {
                    var employee_vat = employee_all * 0.1;

                    content = $@"
                        <br/>
                        <br/>
                        안녕하세요, <span style='color:blue'>{incamAddfare.name}</span> 님<br/>
                        <br/>
                        {incamAddfare.@class} 과정 강의료 지급명세서 송부합니다.<br/>
                        <br/>
                        - 강의료 실지급액 : <span style='color:blue;'>₩{ToAccounting(employee_all + employee_vat)}</span> <span style='color:red;'>(부가세 : {ToAccounting(employee_vat)})</span><br/>
                        <br/>
                        <br/>
                        지급명세서는 아래 링크에서 확인하실수 있습니다. (링크를 공유하지 말아주세요)<br/>
                        <a href='http://49.50.172.5:8080/admin?seq={result.teacher_seq}&password={result.passwd}' target='_blank'>지급명세서 확인</a><br/>
                    ";
                }
                else if (incamAddfare.addfare_gubun == "04")
                {
                    content = $@"
                        <br/>
                        <br/>
                        안녕하세요, <span style='color:blue'>{incamAddfare.name}</span> 님<br/>
                        <br/>
                        {incamAddfare.@class} 과정 강의료 지급명세서 송부합니다.<br/>
                        명세서 확인 후 아래와 같이 송금 요청드립니다.<br/>
                        <br/>
                        - 새싹 기준 강의료 : ₩{ToAccounting(all - all_tax)}<br/>
                        - 핀인사이트 기준 강의료 : <span style='color:blue;'>₩{ToAccounting(employee_all - employee_tax)}</span> <span style='color:red;'>(세전 {contract_price / 10000}만원 * {hour}시간)</span><br/>
                        - 송금액(회수금액) : <span style='color:blue;'>₩{ToAccounting(remit)}</span><br/>
                        - 송금계좌 : 하나은행 | 447-910038-45804 | (주)인코어<br/>
                        <br/>
                        <br/>
                        지급명세서는 아래 링크에서 확인하실수 있습니다. (링크를 공유하지 말아주세요)<br/>
                        <a href='http://49.50.172.5:8080/admin?seq={result.teacher_seq}&password={result.passwd}' target='_blank'>지급명세서 확인</a><br/>
                    ";
                }

                string[] bcc = {"bill@fins.ai"};
                await _email.SendEmail(result.email, title, content, bcc);

                EmailLogModel emailLog = new EmailLogModel
                {
                    use_yn = 1,
                    subject = title,
                    contents = content,
                    to = result.email,
                    type = "addfare"
                };

                await _context.AddAsync(emailLog);
                await _context.SaveChangesAsync();
            }
        }

        public static string ToAccounting(double money)
        {
            return String.Format("{0:#,0}", money);
        }

    }
}

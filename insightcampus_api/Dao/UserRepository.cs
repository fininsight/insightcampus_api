using System;
using System.Collections.Generic;
using insightcampus_api.Data;
using System.Linq;
using insightcampus_api.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace insightcampus_api.Dao
{
    public class UserRepository: UserInterface
    {

        private readonly DataContext _context;
        private readonly IConfiguration _config;

        public UserRepository(IConfiguration config, DataContext context)
        {
            _config = config;
            _context = context;
        }

        public async Task Add<T>(T entity) where T : class
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(UserModel userModel)
        {
            _context.Entry(userModel).Property(x => x.user_id).IsModified = true;
            _context.Entry(userModel).Property(x => x.user_pw).IsModified = true;
            _context.Entry(userModel).Property(x => x.email).IsModified = true;
            _context.Entry(userModel).Property(x => x.name).IsModified = true;
            _context.Entry(userModel).Property(x => x.status).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task<DataTableOutDto> Select(DataTableInputDto dataTableInputDto,  List<Filter> filters)
        {
            var result = (
                            from user in _context.UserContext
                            select user);

            foreach (var filter in filters)
            {
                if (filter.k == "name")
                    result = result.Where(w => w.name.Contains(filter.v.Replace(" ", "")));

                else if (filter.k == "email")
                    result = result.Where(w => w.email.Contains(filter.v.Replace(" ", "")));
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

        public async Task<UserModel> UserExists(UserModel userModel)
        {
            return await _context.UserContext.FirstOrDefaultAsync(x => x.user_id == userModel.user_id);
        }

        public async Task<TeacherModel> FamilyExists(TeacherModel teacherModel)
        {
            return await _context.TeacherContext.FirstOrDefaultAsync(x => x.teacher_seq == teacherModel.teacher_seq && x.passwd == teacherModel.passwd);
        }

        public UserModel Join(UserModel user)
        {
            byte[] passwordHash, passwordSalt;

            if (user.user_pw != null)
            {
                CreatePasswordHash(user.user_pw, out passwordHash, out passwordSalt);

                user.user_pw = Convert.ToBase64String(passwordHash);
                user.salt = Convert.ToBase64String(passwordSalt);
            }

            /*
            if (user.email != null)
            {
                user.email = AESEncrypt256(user.email);
            }
            */

            _context.Add(user);
            _context.SaveChanges();

            return user;
        }

        public UserModel PasswordCheck(UserModel userModel, UserModel userMatched)
        {
            if (VerityPasswordHash(userModel.user_pw, userMatched.user_pw, userMatched.salt))
            {
                return userMatched;
            }
            else
            {
                return null;
            }
        }

        public String AESEncrypt256(String Input)
        {
            RijndaelManaged aes = new RijndaelManaged();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Key").Value);
            aes.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] xBuff = null;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
                {
                    byte[] xXml = Encoding.UTF8.GetBytes(Input);
                    cs.Write(xXml, 0, xXml.Length);
                }

                xBuff = ms.ToArray();
            }

            String Output = Convert.ToBase64String(xBuff);
            return Output;
        }

        //AES_256 복호화
        public String AESDecrypt256(String Input)
        {
            RijndaelManaged aes = new RijndaelManaged();
            aes.KeySize = 256;
            aes.BlockSize = 128;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Key").Value);
            aes.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            var decrypt = aes.CreateDecryptor();
            byte[] xBuff = null;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
                {
                    byte[] xXml = Convert.FromBase64String(Input);
                    cs.Write(xXml, 0, xXml.Length);
                }

                xBuff = ms.ToArray();
            }

            String Output = Encoding.UTF8.GetString(xBuff);
            return Output;
        }

        private bool VerityPasswordHash(string password, string passwordHash, string salt)
        {
            byte[] saltByte = Convert.FromBase64String(salt);

            using (var hmac = new System.Security.Cryptography.HMACSHA256(saltByte))
            {
                var ComputedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

                if (Convert.ToBase64String(ComputedHash) != passwordHash)
                {
                    return false;
                }
            }
            return true;
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}

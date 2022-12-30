
using API.Demo.DAO;
using API.Demo.Infrastructure.Data;
using API.Demo.Domain.Models;
using API.Demo.ResData;
namespace API.Demo.BUS
{
    public class B_People:D_People
    {
        public new async Task<R_Data> ReadList()
        {
            return await base.ReadList();
        }
        public new async Task<R_Data> Read(int id)
        {
            R_Data res = new R_Data { result = 1, data = null, error = new error() };
            res= await base.Read(id);

            return res;
        }
        public new async Task<R_Data>ReadList(int? status)
        {
            R_Data res = new R_Data { result = 1, data = null, error = new error() };
            res=await base.ReadList(status);
            return res;
        }
        public new async Task<R_Data> ReadList(List<int?> status)
        {
            return await base.ReadList(status);
        }
        public async Task<R_Data> Create(Person item)
        {
            error errObj = new error();
            R_Data res = new R_Data() { result = 1, data = null, error = errObj };
            try
            {
                using var db = new InternshipContext();

                var idMax = await base.Max();
                if (idMax.code != 1)
                    throw new Exception();

                item.Id = idMax.data + 1;


                db.People.Add(item);
                var result = await db.SaveChangesAsync();
                if (result > 0)
                {
                    var rPerson = await this.Read(item.Id);
                    if (rPerson.result == 1)
                    {
                        res.data = rPerson.data;
                        errObj.message = "Thêm dữ liệu thành công!";
                    }
                }
                else
                {
                    res.result = 0;
                    errObj.code = 201;
                    errObj.message = "Thêm dữ liệu không thành công!";
                }
            }
            catch (Exception ex)
            {
                res.result = 0;
                res.data = null;
                res.error = new error() { code = -1, message = $"Exception: {ex.Message};" };
            }
            return res;
        }
        public async Task<R_Data>Update(Person item)
        {
            error errObj = new error();
            R_Data res = new R_Data { result = 1, data = null, error = errObj };
            try
            {
                using var db = new InternshipContext();
                var dataExist = await base.Read(item.Id);
                DateTime dtime = DateTime.Now;
                if(dataExist.result!=1 && dataExist.data==null)
                {
                    res.result = 0;
                    res.data = null;
                    errObj.message = "Không tìm thấy Grade với id được chỉ ra!";
                    return res;
                }
                Person uPerson = dataExist.data;
                uPerson.FirstName = item.FirstName;
                uPerson.LastName = item.LastName;
                uPerson.Email = item.Email;
                uPerson.AddressId = item.AddressId;
                uPerson.PhoneNumber = item.PhoneNumber;
                uPerson.UpdatedAt = dtime;

                db.People.Update(uPerson);
                var result = await db.SaveChangesAsync();
                if(result>1)
                {
                    var rPerson = await this.Read(item.Id);
                    if (rPerson.result == 1)
                    {
                        res.data = rPerson.data;
                        errObj.message = "Cập nhật dữ liệu thành công!";
                    }
                }

            }
            catch (Exception ex)
            {
                res.result = 0;
                res.data = null;
                res.error = new error() { code = -1, message = $"Exception: {ex.Message};" };
            }

            return res;
        }
        public async Task<R_Data> Delete(int id)
        {
            error errObj = new error();
            R_Data res = new R_Data { result = 1, data = null, error = errObj };
            try
            {
                using var db = new InternshipContext();
                var dataExist = await base.Read(id);
                if (dataExist.result != 1 && dataExist.data == null)
                {
                    res.result = 0;
                    res.data = null;
                    errObj.message = "Không tìm thấy person với id được chỉ ra!";
                    return res;
                }

                Person uPerson = dataExist.data;
                uPerson.Status = -1;
                uPerson.Timer = DateTime.Now;
                uPerson.UpdatedAt = DateTime.Now;
                db.People.Update(uPerson);
                var result = await db.SaveChangesAsync();
                if (result > 0)
                {
                    var rPerson = await this.Read(id);
                    if (rPerson.result == 1)
                    {
                        res.data = rPerson.data;
                        errObj.message = "Đã xóa dữ liệu thành công!";
                    }
                }
                else
                {
                    res.result = 0;
                    errObj.code = 201;
                    errObj.message = "Xóa dữ liệu không thành công!";
                }
            }
            catch (Exception ex)
            {
                res.result = 0;
                res.data = null;
                res.error = new error() { code = -1, message = $"Exception: {ex.Message};" };
            }
            return res;
        }
    }
}

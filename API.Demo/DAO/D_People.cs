using API.Demo.Domain.Models;
using API.Demo.Infrastructure.Data;
using API.Demo.ResData;
using Microsoft.EntityFrameworkCore;
namespace API.Demo.DAO
{
    public class D_People
    {
        internal async Task<internalData> Max()
        {
            using (var db = new InternshipContext())
            {

                internalData interData = new internalData();
                try
                {
                    var max = await db.People.OrderByDescending(m => m.Id).FirstOrDefaultAsync();
                    interData.data = (max == null ? 0 : max.Id);

                }
                catch (Exception ex)
                {
                    interData.code = -1;
                    interData.message = $"Exception: {ex.Message};";
                }
                return await Task.Run(() => interData);
            }
        }
        protected async Task<R_Data> ReadList()
        {
            using (var db = new InternshipContext())
            {
                error errObj = new error();
                R_Data res = new R_Data() { result = 1, data = null, error = errObj };
                var lstPerson = await Task.FromResult<List<Person>>(new List<Person>());
                try
                {
                    lstPerson = await db.People.Where(w => w.Status == 1).ToListAsync();
                    if (lstPerson == null)
                    {
                        errObj.message = "Dữ liệu rỗng";
                    }
                    else
                    {
                        res.data = lstPerson;
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
        protected async Task<R_Data> Read(int id)
        {
            using (var db = new InternshipContext())
            {
                error errObj = new error();
                R_Data res = new R_Data() { result = 1, data = null, error = errObj };
                var classObj = await Task.FromResult<Person>(new Person());
                try
                {
                    classObj = await db.People.Where(w => w.Id == id && w.Status != 1).FirstOrDefaultAsync();
                    var t=await db.People.Where(w=>w.Id==id && w.Status!=1).FirstOrDefaultAsync();

                    if (classObj == null)
                    {
                        errObj.message = "Dữ liệu rỗng";
                    }
                    else
                    {
                        res.data = classObj;
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
        protected async Task<R_Data>ReadList(int?status)
        {
            using(var db=new InternshipContext())
            {
                error errObj = new error();
                R_Data res = new R_Data { result = 1, data = null, error = errObj };

                var lstPerson = await Task.FromResult<List<Person>>(new List<Person>());
                try
                {
                    lstPerson= await db.People.Where(s => s.Status == status).ToListAsync();
                    if(lstPerson==null)
                    {
                        errObj.message = "Du lieu rong";
                    }
                    else
                    {
                        res.data = lstPerson;
                    }
                }
                catch(Exception ex)
                {
                    res.result = 0;
                    res.data = null;
                    res.error= new error() { code = -1, message = $"Exception: {ex.Message};" };
                }

                return res;
            }
        }
        protected async Task<R_Data> ReadList(List<int?> status)
        {
            using (var db = new InternshipContext())
            {
                error errObj = new error();
                R_Data res = new R_Data() { result = 1, data = null, error = errObj };
                var lstPerson = await Task.FromResult<List<Person>>(new List<Person>());
                try
                {
                    lstPerson = await db.People.Where(w => status.Contains(w.Status)).ToListAsync();
                    if (lstPerson == null)
                    {
                        errObj.message = "Dữ liệu rỗng";
                    }
                    else
                    {
                        res.data = lstPerson;
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
}

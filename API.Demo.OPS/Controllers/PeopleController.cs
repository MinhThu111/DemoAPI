using API.Demo.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Demo.OPS.Controllers
{
    [Route("[controller]/[Action]"), ApiController]
    public class PeopleController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<R_Data>> getListPeople()
        {
            R_Data res = new R_Data { result = 1, data = null, error = new error() };
            try
            {
                res = await new H_People().ReadList();
            }
            catch (Exception ex)
            {
                res.result = 0;
                res.data = null;
                res.error = new error { code = -1, message = ex.Message };
            }
            return res;
        }

        [HttpGet]
        public async Task<ActionResult<R_Data>> getPersonByID(int id)
        {
            R_Data res = new R_Data { result = 1, data = null, error = new error() };
            try
            {
                res = await new H_People().Read(id);
            }
            catch (Exception ex)
            {
                res.result = 0;
                res.data = null;
                res.error = new error { code = -1, message = ex.Message };
            }
            return res;
        }

        [HttpGet]
        public async Task<ActionResult<R_Data>>getListPersonByStatus(int? status)
        {
            R_Data res = new R_Data { result = 1, data = null, error = new error() };
            try
            {
                res=await new H_People().ReadList(status);
            }
            catch(Exception ex)
            {
                res.result = 0;
                res.data = null;
                res.error = new error { code = -1, message = ex.Message };
            }
            return res;
        }

        [HttpGet]
        public async Task<ActionResult<R_Data>>getListPersonByListStatus(string sequenceStatus)
        {
            R_Data res = new R_Data { result = 1, data = null, error = new error() };
            try
            {
                List<int?> lstStatus = new List<int?>();
                if (string.IsNullOrEmpty(sequenceStatus))
                    return new R_Data() { result = 0, data = null, error = new error() { code = 201, message = "Dãy trạng thái chưa nhập giá trị. Dãy trạng thái là ký số và cách nhau bởi dấu phẩy [,]" } };
                try
                {
                    foreach (string s in sequenceStatus.Split(","))
                        if (!string.IsNullOrEmpty(s))
                            lstStatus.Add(Convert.ToInt32(s.Replace(".", "").Replace(" ", "")));
                }
                catch (Exception) { }
                res = await new H_People().ReadList(lstStatus);

            }
            catch(Exception ex)
            {
                res.result = 0;
                res.data = null;
                res.error = new error { code = -1, message = ex.Message };
            }
            return res;
        }

        [HttpPost]
        public async Task<ActionResult<R_Data>> Create(Person item)
        {
            R_Data res = new R_Data { result = 1, data = null, error = new error() };
            try
            {
                res = await new H_People().Create(item.FirstName, item.LastName, item.PersonTypeId, item.Birthday, item.Gender, item.NationalityId, item.ReligionId, item.FolkId, item.AddressId, item.PhoneNumber, item.Email);
            }
            catch (Exception ex)
            {
                res.result = 0;
                res.data = null;
                res.error = new error { code = -1, message = ex.Message };
            }
            return res;
        }

        [HttpPut]
        public async Task<ActionResult<R_Data>>Update(Person item)
        {
            R_Data res = new R_Data { result = 1, data = null, error = new error() };
            try
            {
                res = await new H_People().Update(item.Id, item.FirstName, item.LastName, item.Gender, item.PersonTypeId, item.Timer, item.Status, item.AddressId, item.PhoneNumber);
            }
            catch(Exception ex)
            {
                res.result = 0;
                res.data = null;
                res.error = new error { code = -1, message = ex.Message };
            }
            return res;
        }

        [HttpDelete]
        public async Task<ActionResult<R_Data>> Delete(int id)
        {
            R_Data res = new R_Data { result = 1, data = null, error = new error() };
            try
            {
                res = await new H_People().Delete(id);
            }
            catch (Exception ex)
            {
                res.result = 0;
                res.data = null;
                res.error = new error() { code = -1, message = ex.Message };
            }
            return res;
        }
    }
}

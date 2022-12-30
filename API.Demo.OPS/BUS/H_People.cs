using System.Diagnostics;

namespace API.Demo.WebOps.BUS
{
    public class H_People
    {
        public async Task<R_Data> ReadList()
        {
            R_Data res = new R_Data() { result = 1, data = null, error = new error() };
            List<Dictionary<string, dynamic>> lstdict = new List<Dictionary<string, dynamic>>();

            try
            {
                res = await new B_People().ReadList();
                if (res.result == 1 && res.data != null)
                {
                    List<Person> peopleeObjs = res.data;
                    peopleeObjs.ForEach(peopleeObj =>
                    {
                        Type myType = peopleeObj.GetType();
                        IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
                        Dictionary<string, dynamic> dict = new Dictionary<string, dynamic>();
                        foreach (PropertyInfo prop in props)
                        {
                            dict.Add(prop.Name, prop.GetValue(peopleeObj));
                        }
                        lstdict.Add(dict);
                    });
                    res.data = lstdict;
                }
            }
            catch (Exception ex)
            {
                res.result = 0;
                res.data = null;
                res.error = new error() { code = -1, message = $"Exeception: {ex.Message}" };
            }
            return res;
        }
        public async Task<R_Data> Read(int id)
        {
            R_Data res = new R_Data() { result = 1, data = null, error = new error() };
            Dictionary<string, dynamic> dict = new Dictionary<string, dynamic>();
            try
            {
                res = await new B_People().Read(id);
                if (res.result == 1 && res.data != null)
                {
                    Person personObj = res.data;
                    Type myType = personObj.GetType();
                    IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
                    foreach (PropertyInfo prop in props)
                    {
                        dict.Add(prop.Name, prop.GetValue(personObj));
                    }                   

                    res.data = dict;
                }
            }
            catch (Exception ex)
            {
                res.result = 0;
                res.data = null;
                res.error = new error() { code = -1, message = $"Exeception: {ex.Message}" };
            }
            return res;
        }
        public async Task<R_Data> ReadList(int? status)
        {
            R_Data res = new R_Data() { result = 1, data = null, error = new error() };
            List<Dictionary<string, dynamic>> lstdict = new List<Dictionary<string, dynamic>>();

            try
            {
                res = await new B_People().ReadList(status);
                if (res.result == 1 && res.data != null)
                {
                    List<Person> peopleObjs = res.data;
                    peopleObjs.ForEach(peopleObj =>
                    {
                        Type myType = peopleObj.GetType();
                        IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
                        Dictionary<string, dynamic> dict = new Dictionary<string, dynamic>();
                        foreach (PropertyInfo prop in props)
                        {
                            dict.Add(prop.Name, prop.GetValue(peopleObj));
                        }
                        lstdict.Add(dict);
                    });
                    res.data = lstdict;
                }
            }
            catch (Exception ex)
            {
                res.result = 0;
                res.data = null;
                res.error = new error() { code = -1, message = $"Exeception: {ex.Message}" };
            }
            return res;
        }
        public async Task<R_Data> ReadList(List<int?> status)
        {
            R_Data res = new R_Data() { result = 1, data = null, error = new error() };
            List<Dictionary<string, dynamic>> lstdict = new List<Dictionary<string, dynamic>>();
            try
            {
                res = await new B_People().ReadList(status);
                if (res.result == 1 && res.data != null)
                {
                    List<Person> personObjs = res.data;
                    personObjs.ForEach(personObj =>
                    {
                        Type myType = personObj.GetType();
                        IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
                        Dictionary<string, dynamic> dict = new Dictionary<string, dynamic>();
                        foreach (PropertyInfo prop in props)
                        {
                            dict.Add(prop.Name, prop.GetValue(personObj));
                        }
                        lstdict.Add(dict);
                    });
                    res.data = lstdict;
                }
            }
            catch (Exception ex)
            {
                res.result = 0;
                res.data = null;
                res.error = new error() { code = -1, message = $"Exeception: {ex.Message}" };
            }
            return res;
        }
        public async Task<R_Data> Create(string fsname, string lsname, int persontypeid, DateTime? birthday, int? gender, int? nationalityid, int? regilionid, int? folkid, int? addressid, string phone, string email)
        {
            Person item = new Person()
            {
                FirstName = fsname,
                LastName = lsname,
                PersonTypeId = persontypeid,
                Birthday = birthday,
                Gender = gender,
                NationalityId = nationalityid,
                ReligionId = regilionid,
                FolkId = folkid,
                AddressId = addressid,
                PhoneNumber = phone,
                Email = email,
                Timer = DateTime.Now,
                CreatedAt = DateTime.Now
            };
            R_Data res = await new B_People().Create(item);
            Dictionary<string, dynamic> dict = new Dictionary<string, dynamic>();
            try
            {
                if (res.result == 1 && res.data != null)
                {
                    Person peopleObj = res.data;
                    Type myType = peopleObj.GetType();
                    IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
                    foreach (PropertyInfo prop in props)
                    {
                        dict.Add(prop.Name, prop.GetValue(peopleObj));
                    }
                    res.data = dict;
                }
            }
            catch (Exception ex)
            {
                res.result = 0;
                res.data = null;
                res.error = new error() { code = -1, message = $"Exeception: {ex.Message}" };
            }
            return res;
        }
        public async Task<R_Data> Update(int id, string firstname, string lastname, int? gender, int persontypeid, DateTime timer, int? status, int? address, string phone)
        {
            R_Data res = new R_Data { result = 1, data = null, error = new error() };
            res = await new B_People().Read(id);
            Person uPerson = res.data;
            if (res.result != 1)
            {
                return new R_Data() { result = 0, data = null, error = new error() { code = 201, message = "Không tìm thấy dữ liệu với điều kiện đưa ra" } };
            }
            if (uPerson == null)
            {
                res.result = 0;
                res.data = null;
                res.error = new error() { code = 201, message = "Không tìm thấy Grade với id  được chỉ ra!" };
                return res;
            }
            if (uPerson.Timer > timer)
            {
                res.result = 0;
                res.data = null;
                res.error = new error() { code = 201, message = "Thông tin đã được cập nhật lại trước đó. Vui lòng hủy thao tác và thực hiện lại để dữ liệu đồng bộ!" };
                return res;
            }

            Person item = new Person()
            {
                FirstName = firstname,
                LastName = lastname,
                Gender = gender,
                PersonTypeId = persontypeid,
                Status = status,
                AddressId = address,
                PhoneNumber = phone
            };
            res = await new B_People().Update(item);
            Dictionary<string, dynamic> dict = new Dictionary<string, dynamic>();
            try
            {
                if (res.result == 1 && res.data != null)
                {
                    Person personObj = res.data;
                    Type myType = personObj.GetType();
                    IList<PropertyInfo> props = new List<PropertyInfo>(myType.GetProperties());
                    foreach (PropertyInfo prop in props)
                    {
                        dict.Add(prop.Name, prop.GetValue(personObj));
                    }
                    res.data = dict;
                }
            }
            catch (Exception ex)
            {
                res.result = 0;
                res.data = null;
                res.error = new error() { code = -1, message = $"Exeception: {ex.Message}" };
            }
            return res;
        }
        public async Task<R_Data> Delete(int id)
        {
            R_Data res = new R_Data { result = 1, data = null, error = new error() };
            res = await new B_People().Delete(id);

            return res;
        }
    }
}

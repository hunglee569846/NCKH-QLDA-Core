using NCKH.Core.Domain.IRepository;
using NCKH.Core.Domain.IServices;
using NCKH.Core.Domain.ModelMeta;
using NCKH.Core.Domain.Models;
using NCKH.Core.Domain.ViewModel;
using NCKH.Infrastruture.Binding.Constans;
using NCKH.Infrastruture.Binding.Models;
using NCKH.Infrastruture.Binding.ViewModel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NCKH.Core.Infrastructure.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IFacultyRepository _facultyRepository;

        public DepartmentService(IDepartmentRepository departmentRepository,
                                 IFacultyRepository facultyRepository)
        {
            _departmentRepository = departmentRepository;
            _facultyRepository = facultyRepository;

        }
        public async Task<List<DepartmentViewModel>> SelectAll()
        {
            return await _departmentRepository.SelectAllAsync();
        }
        public async Task<SearchResult<DepartmentViewModel>> SelectByIdFaculty(string idfaculty)
        {
            return await _departmentRepository.SelectByIdFacultyAsync(idfaculty);
        }
        public async Task<ActionResultReponese<DepartmentViewModel>> SelectByIdAsync(string idDepartment, string nameDepartment)
        {
            var getinfoDepartment = _departmentRepository.GetInfo(idDepartment, nameDepartment);
            var result = await _departmentRepository.SelectByIdAsync(idDepartment, nameDepartment);
            //var result = await _departmentRepository.GetInfo(idDepartment, nameDepartment);
            if (result == null)
                return new ActionResultReponese<DepartmentViewModel>(-31,"Khong tim thay", "Department");
            return new ActionResultReponese<DepartmentViewModel>
            {
                Code = 1,
                Data = result,
            };
        }
        public async Task<ActionResultReponese<string>> InsertAsync(string NameFaculty, DepartmentMeta department)
        {
            var idfaculty =await _facultyRepository.CheckExitsFacult(NameFaculty);
            if (!idfaculty)
                return new ActionResultReponese<string>(-21, "khoa khong ton tai", "Faculty");
            var namedeartment = await _departmentRepository.CheckExitsDepartment(department.NameDepartment);
            if (namedeartment)
                return new ActionResultReponese<string>(-22, "Bo mon da ton tai", "Department");
            var _department = new Department
            {
                IdDepartment = Guid.NewGuid().ToString(),
                NameDepartment = department.NameDepartment?.Trim(),
                Office = department.Office?.Trim(),
                Addres = department.Addres?.Trim(),
                Email = department.Email?.Trim(),
                PhoneNumber = department.PhoneNumber?.Trim(),
                IdFaculty = department.IdFaculty?.Trim(),
                CreateDate = DateTime.Now,
                LastUpdate = null,
                IsActive = true,
                IsDelete = false,
                DeleteTime= null
            };
            var Result = await _departmentRepository.InsertAsync(_department);
            if (Result >= 0)
                return new ActionResultReponese<string>(Result, "them thanh cong", "Department", null);
            return new ActionResultReponese<string>(Result, "them that bai", "Department", null);

        }
        public async Task<ActionResultReponese<string>> UpdateAsync(string IdDepartment, DepartmentMeta department)
        {
            var idfaculty = await _facultyRepository.CheckExitsIdFacult(department.IdFaculty);
            if (!idfaculty)
                return new ActionResultReponese<string>(-21, "khoa khong ton tai", "Faculty");
            var _department = new Department
            {
                IdDepartment = IdDepartment?.Trim(),
                NameDepartment = department.NameDepartment?.Trim(),
                Office = department.Office?.Trim(),
                Addres = department.Addres?.Trim(),
                Email = department.Email?.Trim(),
                PhoneNumber = department.PhoneNumber?.Trim(),
                IdFaculty = department.IdFaculty?.Trim(),
                LastUpdate = DateTime.Now,
            };
            var Result = await _departmentRepository.UpdateAsync(_department);
            if (Result > 0)
                return new ActionResultReponese<string>(Result, "Update thanh cong", "Department", null);
            return new ActionResultReponese<string>(Result, "Update that bai", "Department", null);
        }
        public async Task<ActionResultReponese<string>> DeleteAsync(string IdDepartment, string NameDepartment)
        {
            var idfaculty = await _departmentRepository.CheckExitsDepartment(NameDepartment);
            if (!idfaculty)
                return new ActionResultReponese<string>(-21, "Bo mon khong ton tai", "Department");
            var Result = await _departmentRepository.DeleteAsync(IdDepartment, NameDepartment);
            if (Result > 0)
                return new ActionResultReponese<string>(Result, "Delete thanh cong", "Department", null);
            return new ActionResultReponese<string>(Result, "Delete that bai", "Department", null);
        }

        /// <summary>
        /// chu y duong dan lay file excel la lay tu upload cua API upFile
        /// 
        /// </summary>
        /// <param name="NameFaculty"></param>
        /// <returns></returns>
        public async Task<ActionResultReponese<string>> InsertListExcelAsync(string NameFaculty)
        {
            List<DepartmentMeta> departmentlist = new List<DepartmentMeta>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var pakage = new ExcelPackage(new FileInfo("Exceltest\\ExceltestDepartment.xlsx")))
            { 
                ExcelWorksheet worsheet = pakage.Workbook.Worksheets[0];
                for (int i = worsheet.Dimension.Start.Row + 1; i <= worsheet.Dimension.End.Row; i++)
                {
                    //StudentTest st1 = new StudentTest();
                    int j = 1;
                    string Email = worsheet.Cells[i, j++].Value.ToString();
                    string adress = worsheet.Cells[i, j++].Value.ToString();
                    string namedepartment = worsheet.Cells[i, j++].Value.ToString();
                    string office = worsheet.Cells[i, j++].Value.ToString();
                    string phonnumber = worsheet.Cells[i, j++].Value.ToString();
                    string idfacultymeta = worsheet.Cells[i, j++].Value.ToString();
                    DepartmentMeta _depart = new DepartmentMeta()
                    {
                        Email = Email,
                        Addres = adress,
                        NameDepartment = namedepartment,
                        Office = office,
                        PhoneNumber = phonnumber,
                        IdFaculty = idfacultymeta,


                    };
                    departmentlist.Add(_depart);
                }
                int dem = 0;
                foreach (var item in departmentlist)
                {
                    var idfaculty = await _facultyRepository.CheckExitsFacult(NameFaculty);
                    if (!idfaculty)
                        return new ActionResultReponese<string>(-21, "khoa khong ton tai", "Faculty");

                    var namedeartment = await _departmentRepository.CheckExitsDepartment(item.NameDepartment);
                    if (namedeartment)
                        return new ActionResultReponese<string>(-22, "Bo mon da ton tai", "Department");
                    var _department = new Department
                    {
                        IdDepartment = Guid.NewGuid().ToString(),
                        NameDepartment = item.NameDepartment?.Trim(),
                        Office = item.Office?.Trim(),
                        Addres = item.Addres?.Trim(),
                        Email = item.Email?.Trim(),
                        PhoneNumber = item.PhoneNumber?.Trim(),
                        IdFaculty = item.IdFaculty?.Trim(),
                        CreateDate = DateTime.Now,
                        LastUpdate = null,
                        IsActive = true,
                        IsDelete = false,
                        DeleteTime = null
                    };
                    var Result = await _departmentRepository.InsertAsync(_department);
                    if (Result > 0)
                        dem++;
                }
                if(dem >0)
                    return new ActionResultReponese<string>(-5, "them thanh cong", "Department", null);
                return new ActionResultReponese<string>(dem, "them that bai", "Department", null);

            }

        }
    }
}

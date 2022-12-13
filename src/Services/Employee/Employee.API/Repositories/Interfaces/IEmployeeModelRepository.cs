using Employee.API.Entities;

namespace Employee.API.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeModel>> GetEmployees (); 
        Task<EmployeeModel?> GetEmployee(string id);
        Task<bool> CreateEmployee(EmployeeModel user);
        Task<bool> UpdateEmployee(EmployeeModel user, string id);
        Task<bool> DeleteEmployee(string id);
    }
}

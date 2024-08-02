using ClassLibrary;
using Microsoft.EntityFrameworkCore;
using TaskEmployee.DBManager;
using TaskEmployee.IRepositories;

namespace TaskEmployee.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _context;

        public EmployeeRepository(EmployeeContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            try
            {
                return await _context.Employees.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching employees", ex);
            }
        }

        public async Task<Employee> GetEmployee(int id)
        {
            try
            {
                return await _context.Employees.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching employee with ID {id}", ex);
            }
        }

        public async Task<Employee> AddEmployee(Employee employee)
        {
            try
            {
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                return employee;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding employee", ex);
            }
        }

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            try
            {
                _context.Entry(employee).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return employee;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating employee", ex);
            }
        }

        public async Task<Employee> DeleteEmployee(int id)
        {
            try
            {
                var employee = await _context.Employees.FindAsync(id);
                if (employee != null)
                {
                    _context.Employees.Remove(employee);
                    await _context.SaveChangesAsync();
                }
                return employee;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting employee with ID {id}", ex);
            }
        }

    }
}

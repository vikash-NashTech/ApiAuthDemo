using ApiAuthDemo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public static Employee[] employees; 
        public EmployeeController()
        {
            employees = 
            [
            new Employee {
                EMPLOYEE_ID = 1,
                FIRST_NAME = "Donald",
                LAST_NAME = "OConnell",
                EMAIL = "DOCONNEL",
                PHONE_NUMBER = "650.507.9833",
                HIRE_DATE = "21-JUN-07",
                JOB_ID = "SH_CLERK",
                SALARY = 2600,
            },
            new Employee {
                EMPLOYEE_ID = 2,
                FIRST_NAME = "Douglas",
                LAST_NAME = "Grant",
                EMAIL = "DGRANT",
                PHONE_NUMBER = "650.507.9844",
                HIRE_DATE = "13-JAN-08",
                JOB_ID = "SH_CLERK",
                SALARY = 2600
            },
            new Employee{
                EMPLOYEE_ID = 3,
                FIRST_NAME = "Jennifer",
                LAST_NAME = "Whalen",
                EMAIL = "JWHALEN",
                PHONE_NUMBER = "515.123.4444",
                HIRE_DATE = "17-SEP-03",
                JOB_ID = "AD_ASST",
                SALARY = 4400
            },
            new Employee {
                EMPLOYEE_ID = 4,
                FIRST_NAME = "Michael",
                LAST_NAME = "Hartstein",
                EMAIL = "MHARTSTE",
                PHONE_NUMBER = "515.123.5555",
                HIRE_DATE = "17-FEB-04",
                JOB_ID = "MK_MAN",
                SALARY = 13000
            },
            new Employee {
                EMPLOYEE_ID = 5,
                FIRST_NAME = "Pat",
                LAST_NAME = "Fay",
                EMAIL = "PFAY",
                PHONE_NUMBER = "603.123.6666",
                HIRE_DATE = "17-AUG-05",
                JOB_ID = "MK_REP",
                SALARY = 6000
            },
            new Employee {
                EMPLOYEE_ID = 6,
                FIRST_NAME = "Susan",
                LAST_NAME = "Mavris",
                EMAIL = "SMAVRIS",
                PHONE_NUMBER = "515.123.7777",
                HIRE_DATE = "07-JUN-02",
                JOB_ID = "HR_REP",
                SALARY = 6500
            },
            new Employee {
                EMPLOYEE_ID = 7,
                FIRST_NAME = "Hermann",
                LAST_NAME = "Baer",
                EMAIL = "HBAER",
                PHONE_NUMBER = "515.123.8888",
                HIRE_DATE = "07-JUN-02",
                JOB_ID = "PR_REP",
                SALARY = 10000
            }
            ];
        }

        [Authorize(Policy = "ReadUsers")]
        [HttpGet("/")]
        public Employee[] GetAllEmployees()
        {
            return employees;
        }

        [Authorize]
        [Route("/{id}")]
        [HttpGet]
        public Employee GetEmployee(int id)
        {
            
            return employees.FirstOrDefault(employee => employee.EMPLOYEE_ID == id);
        }

        [HttpGet("/token-with-policy")]
        public string GenerateTokenWithPolicy()
        {
            return TokenService.GenerateToken("read");

        }
        [HttpGet("/token-without-policy")]
        public string GenerateTokenWithOutPolicy()
        {
            return TokenService.GenerateToken("");

        }
        
    }
}

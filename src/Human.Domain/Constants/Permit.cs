using System.Collections.Immutable;
using System.Reflection;

namespace Human.Domain.Constants;

public static class Permit
{
    public const string CreateUserPermission = "create:userPermission";
    public const string ReadUserPermission = "read:userPermission";
    public const string UpdateUserPermission = "update:userPermission";
    public const string DeleteUserPermission = "delete:userPermission";
    public const string CreateDepartment = "create:department";
    public const string ReadDepartment = "read:department";
    public const string UpdateDepartment = "update:department";
    public const string DeleteDepartment = "delete:department";
    public const string CreateEmployee = "create:employee";
    public const string ReadEmployee = "read:employee";
    public const string UpdateEmployee = "update:employee";
    public const string DeleteEmployee = "delete:employee";
    public const string CreateDepartmentPosition = "create:departmentPosition";
    public const string ReadDepartmentPosition = "read:departmentPosition";
    public const string UpdateDepartmentPosition = "update:departmentPosition";
    public const string DeleteDepartmentPosition = "delete:departmentPosition";
    public const string CreateEmployeePosition = "create:employeePosition";
    public const string ReadEmployeePosition = "read:employeePosition";
    public const string UpdateEmployeePosition = "update:employeePosition";
    public const string DeleteEmployeePosition = "delete:employeePosition";
    public const string CreateLeaveType = "create:leaveType";
    public const string ReadLeaveType = "read:leaveType";
    public const string UpdateLeaveType = "update:leaveType";
    public const string DeleteLeaveType = "delete:leaveType";
    public const string CreateLeaveApplication = "create:leaveApplication";
    public const string ReadLeaveApplication = "read:leaveApplication";
    public const string UpdateLeaveApplication = "update:leaveApplication";
    public const string DeleteLeaveApplication = "delete:leaveApplication";
    public const string ApplyForLeave = "apply:leaveApplication";
    public const string ProcessLeaveApplication = "process:leaveApplication";

    public static ImmutableHashSet<string> AllPermissions { get; } = typeof(Permit).GetFields(BindingFlags.Public | BindingFlags.Static).Select(x => x.GetValue(null)).Cast<string>().ToImmutableHashSet();
}

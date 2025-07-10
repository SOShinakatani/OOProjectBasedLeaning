using OOProjectBasedLeaning;
using OOProjectBasedLeaning.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public interface TimeTracker
{
    void PunchIn(int employeeId);
    void PunchIn(int employeeId, WorkLocation location);  // ←追加
    void PunchOut(int employeeId);
    void PunchOut(int employeeId, WorkLocation location); // ←追加
    bool IsAtWork(int employeeId);
}

public class TimeTrackerModel : TimeTracker
{
    private Company company = NullCompany.Instance;

    private readonly Dictionary<DateTime, Dictionary<int, List<DateTime>>> punchInHistory = new();
    private readonly Dictionary<DateTime, Dictionary<int, List<DateTime>>> punchOutHistory = new();

    public event EventHandler<string>? LogUpdated;

    protected void OnLogUpdated(string message)
    {
        LogUpdated?.Invoke(this, message);
    }

    public TimeTrackerModel(Company company)
    {
        this.company = company.AddTimeTracker(this);
    }

    // 通常の PunchIn
    public void PunchIn(int employeeId)
    {
        string employeeName = GetEmployeeName(employeeId);

        if (IsAtWork(employeeId))
        {
            string msg = $"⚠️ 従業員 {employeeName} はすでに出勤済みです。";
            OnLogUpdated(msg);
            throw new InvalidOperationException(msg);
        }

        if (!punchInHistory.ContainsKey(DateTime.Today))
        {
            punchInHistory[DateTime.Today] = new Dictionary<int, List<DateTime>>();
        }

        if (!punchInHistory[DateTime.Today].ContainsKey(employeeId))
        {
            punchInHistory[DateTime.Today][employeeId] = new List<DateTime>();
        }

        punchInHistory[DateTime.Today][employeeId].Add(DateTime.Now);

        OnLogUpdated($"従業員 {employeeName} が出勤しました: {DateTime.Now:yyyy/MM/dd HH:mm:ss}");
    }

    // 出勤（場所つき）
    public void PunchIn(int employeeId, WorkLocation location)
    {
        string employeeName = GetEmployeeName(employeeId);

        if (IsAtWork(employeeId))
        {
            string msg = $"⚠️ 従業員 {employeeName} はすでに出勤済みです。";
            OnLogUpdated(msg);
            throw new InvalidOperationException(msg);
        }

        if (!punchInHistory.ContainsKey(DateTime.Today))
        {
            punchInHistory[DateTime.Today] = new Dictionary<int, List<DateTime>>();
        }

        if (!punchInHistory[DateTime.Today].ContainsKey(employeeId))
        {
            punchInHistory[DateTime.Today][employeeId] = new List<DateTime>();
        }

        punchInHistory[DateTime.Today][employeeId].Add(DateTime.Now);

        OnLogUpdated($"従業員 {employeeName} が出勤しました: {DateTime.Now:yyyy/MM/dd HH:mm:ss}（{location}）");
    }

    // 通常の PunchOut
    public void PunchOut(int employeeId)
    {
        string employeeName = GetEmployeeName(employeeId);

        if (!IsAtWork(employeeId))
        {
            string msg = $"⚠️ 従業員 {employeeName} はすでに退勤済みです。";
            OnLogUpdated(msg);
            throw new InvalidOperationException(msg);
        }

        if (!punchOutHistory.ContainsKey(DateTime.Today))
        {
            punchOutHistory[DateTime.Today] = new Dictionary<int, List<DateTime>>();
        }

        if (!punchOutHistory[DateTime.Today].ContainsKey(employeeId))
        {
            punchOutHistory[DateTime.Today][employeeId] = new List<DateTime>();
        }

        punchOutHistory[DateTime.Today][employeeId].Add(DateTime.Now);

        OnLogUpdated($"従業員 {employeeName} が退勤しました: {DateTime.Now:yyyy/MM/dd HH:mm:ss}");
    }

    // 退勤（場所つき）
    public void PunchOut(int employeeId, WorkLocation location)
    {
        string employeeName = GetEmployeeName(employeeId);

        if (!IsAtWork(employeeId))
        {
            string msg = $"⚠️ 従業員 {employeeName} はすでに退勤済みです。";
            OnLogUpdated(msg);
            throw new InvalidOperationException(msg);
        }

        if (!punchOutHistory.ContainsKey(DateTime.Today))
        {
            punchOutHistory[DateTime.Today] = new Dictionary<int, List<DateTime>>();
        }

        if (!punchOutHistory[DateTime.Today].ContainsKey(employeeId))
        {
            punchOutHistory[DateTime.Today][employeeId] = new List<DateTime>();
        }

        punchOutHistory[DateTime.Today][employeeId].Add(DateTime.Now);

        OnLogUpdated($"従業員 {employeeName} が退勤しました: {DateTime.Now:yyyy/MM/dd HH:mm:ss}（{location}）");
    }

    public bool IsAtWork(int employeeId)
    {
        int inCount = 0;
        if (punchInHistory.ContainsKey(DateTime.Today))
        {
            if (punchInHistory[DateTime.Today].ContainsKey(employeeId))
            {
                inCount = punchInHistory[DateTime.Today][employeeId].Count;
            }
        }

        int outCount = 0;
        if (punchOutHistory.ContainsKey(DateTime.Today))
        {
            if (punchOutHistory[DateTime.Today].ContainsKey(employeeId))
            {
                outCount = punchOutHistory[DateTime.Today][employeeId].Count;
            }
        }

        return inCount > outCount;
    }

    public bool TryGetPunchInTime(int employeeId, out DateTime time)
    {
        time = default;
        if (punchInHistory.ContainsKey(DateTime.Today))
        {
            if (punchInHistory[DateTime.Today].ContainsKey(employeeId))
            {
                if (punchInHistory[DateTime.Today][employeeId].Count > 0)
                {
                    time = punchInHistory[DateTime.Today][employeeId].Last();
                    return true;
                }
            }
        }
        return false;
    }

    public bool TryGetPunchOutTime(int employeeId, out DateTime time)
    {
        time = default;
        if (punchOutHistory.ContainsKey(DateTime.Today))
        {
            if (punchOutHistory[DateTime.Today].ContainsKey(employeeId))
            {
                if (punchOutHistory[DateTime.Today][employeeId].Count > 0)
                {
                    time = punchOutHistory[DateTime.Today][employeeId].Last();
                    return true;
                }
            }
        }
        return false;
    }

    private string GetEmployeeName(int employeeId)
    {
        Employee? employee = null;

        if (company != null)
        {
            employee = company is CompanyModel model
                ? model.FindEmployeeById(employeeId)
                : null;
        }

        if (employee == null)
        {
            foreach (var emp in EmployeeRepository.GetAll())
            {
                if (emp.Id == employeeId)
                {
                    employee = emp;
                    break;
                }
            }
        }

        return employee?.Name ?? $"ID:{employeeId}";
    }
}

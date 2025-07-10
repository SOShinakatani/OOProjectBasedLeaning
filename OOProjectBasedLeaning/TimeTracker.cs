using OOProjectBasedLeaning;
using OOProjectBasedLeaning.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public interface TimeTracker
{
    void PunchIn(int employeeId);
    void PunchOut(int employeeId);
    bool IsAtWork(int employeeId);
}

public class SimpleTimeTracker : TimeTracker
{
    private readonly Dictionary<int, DateTime> punchInTimes = new();
    private readonly Dictionary<int, DateTime> punchOutTimes = new();

    public void PunchIn(int employeeId)
    {
        if (IsAtWork(employeeId))
        {
            throw new InvalidOperationException("従業員はすでに仕事中です。");
        }

        punchInTimes[employeeId] = DateTime.Now;
        punchOutTimes.Remove(employeeId); // 退勤記録をリセット
        Console.WriteLine($"従業員 {employeeId} が出勤しました: {punchInTimes[employeeId]}");
    }

    public void PunchOut(int employeeId)
    {
        if (!IsAtWork(employeeId))
        {
            throw new InvalidOperationException("従業員は仕事中ではありません。");
        }

        punchOutTimes[employeeId] = DateTime.Now;
        Console.WriteLine($"従業員 {employeeId} が退勤しました: {punchOutTimes[employeeId]}");
    }

    public bool IsAtWork(int employeeId)
    {
        return punchInTimes.ContainsKey(employeeId) && !punchOutTimes.ContainsKey(employeeId);
    }
}


public class TimeTrackerModel : TimeTracker
{
    private Company company = NullCompany.Instance;

    // 日付ごとに従業員IDごとの複数の打刻時刻を管理
    private readonly Dictionary<DateTime, Dictionary<int, List<DateTime>>> punchInHistory = new();
    private readonly Dictionary<DateTime, Dictionary<int, List<DateTime>>> punchOutHistory = new();

    private enum Mode
    {
        PunchIn,
        PunchOut
    }
    private Mode mode = Mode.PunchIn;

    public event EventHandler<string>? LogUpdated;

    protected void OnLogUpdated(string message)
    {
        LogUpdated?.Invoke(this, message);
    }

    public TimeTrackerModel(Company company)
    {
        this.company = company.AddTimeTracker(this);
    }

    public void PunchIn(int employeeId)
    {
        string employeeName = GetEmployeeName(employeeId);

        if (IsAtWork(employeeId))
        {
            string msg = "⚠️ 従業員 " + employeeName + " はすでに出勤済みです。";
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

        OnLogUpdated("従業員 " + employeeName + " が出勤しました: " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
    }

    public void PunchOut(int employeeId)
    {
        string employeeName = GetEmployeeName(employeeId);

        if (!IsAtWork(employeeId))
        {
            string msg = "⚠️ 従業員 " + employeeName + " はすでに退勤済みです。";
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

        OnLogUpdated("従業員 " + employeeName + " が退勤しました: " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
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

        foreach (var emp in EmployeeRepository.GetAll())
        {
            if (emp.Id == employeeId)
            {
                employee = emp;
                break;
            }
        }

        if (employee == null)
        {
            return "ID:" + employeeId;
        }
        else
        {
            return employee.Name;
        }
    }



    public class NullTimeTracker : TimeTracker, NullObject
    {
        private static readonly NullTimeTracker instance = new NullTimeTracker();

        private NullTimeTracker() { }

        public static TimeTracker Instance => instance;

        public void PunchIn(int employeeId)
        {
            // 何もしない
        }

        public void PunchOut(int employeeId)
        {
            // 何もしない
        }

        public bool IsAtWork(int employeeId) => false;
    }
}

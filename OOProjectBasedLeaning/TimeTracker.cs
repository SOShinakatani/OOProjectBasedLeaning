using OOProjectBasedLeaning;
using OOProjectBasedLeaning.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public enum WorkLocation
{
    Office,
    Home,
    Remote
}

public interface TimeTracker
{
    void PunchIn(int employeeId, WorkLocation location);
    void PunchOut(int employeeId, WorkLocation location);
    bool IsAtWork(int employeeId);
}

public class TimeTrackerModel : TimeTracker
{
    private Company company = NullCompany.Instance;

    private readonly Dictionary<DateTime, Dictionary<int, List<DateTime>>> punchInHistory = new();
    private readonly Dictionary<DateTime, Dictionary<int, List<DateTime>>> punchOutHistory = new();
    private readonly Dictionary<DateTime, Dictionary<int, List<WorkLocation>>> punchInLocations = new();
    private readonly Dictionary<DateTime, Dictionary<int, List<WorkLocation>>> punchOutLocations = new();

    public event EventHandler<string>? LogUpdated;

    protected void OnLogUpdated(string message)
    {
        LogUpdated?.Invoke(this, message);
    }

    public TimeTrackerModel(Company company)
    {
        this.company = company.AddTimeTracker(this);
    }

    public void PunchIn(int employeeId, WorkLocation location)
    {
        string employeeName = GetEmployeeName(employeeId);

        if (IsAtWork(employeeId))
        {
            string msg = $"⚠️ 従業員 {employeeName} はすでに出勤済みです。";
            OnLogUpdated(msg);
            throw new InvalidOperationException(msg);
        }

        DateTime now = DateTime.Now;
        DateTime today = now.Date;

        if (!punchInHistory.ContainsKey(today))
            punchInHistory[today] = new();
        if (!punchInHistory[today].ContainsKey(employeeId))
            punchInHistory[today][employeeId] = new();
        punchInHistory[today][employeeId].Add(now);

        if (!punchInLocations.ContainsKey(today))
            punchInLocations[today] = new();
        if (!punchInLocations[today].ContainsKey(employeeId))
            punchInLocations[today][employeeId] = new();
        punchInLocations[today][employeeId].Add(location);

        OnLogUpdated($"従業員 {employeeName} が出勤しました（{location}）: {now:yyyy/MM/dd HH:mm:ss}");
    }

    public void PunchOut(int employeeId, WorkLocation location)
    {
        string employeeName = GetEmployeeName(employeeId);

        if (!IsAtWork(employeeId))
        {
            string msg = $"⚠️ 従業員 {employeeName} はすでに退勤済みです。";
            OnLogUpdated(msg);
            throw new InvalidOperationException(msg);
        }

        DateTime now = DateTime.Now;
        DateTime today = now.Date;

        if (!punchOutHistory.ContainsKey(today))
            punchOutHistory[today] = new();
        if (!punchOutHistory[today].ContainsKey(employeeId))
            punchOutHistory[today][employeeId] = new();
        punchOutHistory[today][employeeId].Add(now);

        if (!punchOutLocations.ContainsKey(today))
            punchOutLocations[today] = new();
        if (!punchOutLocations[today].ContainsKey(employeeId))
            punchOutLocations[today][employeeId] = new();
        punchOutLocations[today][employeeId].Add(location);

        OnLogUpdated($"従業員 {employeeName} が退勤しました（{location}）: {now:yyyy/MM/dd HH:mm:ss}");
    }

    public bool IsAtWork(int employeeId)
    {
        DateTime today = DateTime.Today;
        int inCount = punchInHistory.ContainsKey(today) && punchInHistory[today].ContainsKey(employeeId)
            ? punchInHistory[today][employeeId].Count : 0;
        int outCount = punchOutHistory.ContainsKey(today) && punchOutHistory[today].ContainsKey(employeeId)
            ? punchOutHistory[today][employeeId].Count : 0;
        return inCount > outCount;
    }

    public bool TryGetPunchInTime(int employeeId, out DateTime time)
    {
        time = default;
        DateTime today = DateTime.Today;
        if (punchInHistory.ContainsKey(today) && punchInHistory[today].ContainsKey(employeeId) &&
            punchInHistory[today][employeeId].Count > 0)
        {
            time = punchInHistory[today][employeeId].Last();
            return true;
        }
        return false;
    }

    public bool TryGetPunchOutTime(int employeeId, out DateTime time)
    {
        time = default;
        DateTime today = DateTime.Today;
        if (punchOutHistory.ContainsKey(today) && punchOutHistory[today].ContainsKey(employeeId) &&
            punchOutHistory[today][employeeId].Count > 0)
        {
            time = punchOutHistory[today][employeeId].Last();
            return true;
        }
        return false;
    }

    private string GetEmployeeName(int employeeId)
    {
        var employee = EmployeeRepository.GetAll().FirstOrDefault(e => e.Id == employeeId);
        return employee?.Name ?? $"ID:{employeeId}";
    }

    public class NullTimeTracker : TimeTracker, NullObject
    {
        private static readonly NullTimeTracker instance = new NullTimeTracker();
        private NullTimeTracker() { }
        public static TimeTracker Instance => instance;

        public void PunchIn(int employeeId, WorkLocation location) { }
        public void PunchOut(int employeeId, WorkLocation location) { }
        public bool IsAtWork(int employeeId) => false;
    }
}

using OOProjectBasedLeaning;
using System;
using System.Collections.Generic;

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
//多分完了


public class TimeTrackerModel : TimeTracker
{
    private Company company = NullCompany.Instance;

    private Dictionary<DateTime, Dictionary<int, List<DateTime>>> punchInHistory = new Dictionary<DateTime, Dictionary<int, List<DateTime>>>();
    private Dictionary<DateTime, Dictionary<int, List<DateTime>>> punchOutHistory = new Dictionary<DateTime, Dictionary<int, List<DateTime>>>();

    private Mode mode = Mode.PunchIn;

    public event EventHandler<string>? LogUpdated;

    // --- イベント発火用メソッド ---
    protected void OnLogUpdated(string message)
    {
        LogUpdated?.Invoke(this, message);
    }

    private enum Mode
    {
        PunchIn,
        PunchOut
    }

    public TimeTrackerModel(Company company)
    {
        this.company = company.AddTimeTracker(this);
    }

    public void PunchIn(int employeeId)
    {
        if (IsAtWork(employeeId))
        {
            throw new InvalidOperationException("従業員はすでに出勤中です。");
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

        OnLogUpdated($"従業員 {employeeId} が出勤しました: {DateTime.Now:yyyy/MM/dd HH:mm:ss}");
    }

    public void PunchOut(int employeeId)
    {
        if (!IsAtWork(employeeId))
        {
            throw new InvalidOperationException("まだ出勤していません。");
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

        OnLogUpdated($"従業員 {employeeId} が退勤しました: {DateTime.Now:yyyy/MM/dd HH:mm:ss}");
    }

    public bool IsAtWork(int employeeId)
    {
        int inCount = punchInHistory.ContainsKey(DateTime.Today) && punchInHistory[DateTime.Today].ContainsKey(employeeId)
            ? punchInHistory[DateTime.Today][employeeId].Count
            : 0;

        int outCount = punchOutHistory.ContainsKey(DateTime.Today) && punchOutHistory[DateTime.Today].ContainsKey(employeeId)
            ? punchOutHistory[DateTime.Today][employeeId].Count
            : 0;

        return inCount > outCount;
    }

    public bool TryGetPunchInTime(int employeeId, out DateTime time)
    {
        time = default;
        if (punchInHistory.ContainsKey(DateTime.Today)
            && punchInHistory[DateTime.Today].ContainsKey(employeeId)
            && punchInHistory[DateTime.Today][employeeId].Count > 0)
        {
            time = punchInHistory[DateTime.Today][employeeId].Last();
            return true;
        }
        return false;
    }

    public bool TryGetPunchOutTime(int employeeId, out DateTime time)
    {
        time = default;
        if (punchOutHistory.ContainsKey(DateTime.Today)
            && punchOutHistory[DateTime.Today].ContainsKey(employeeId)
            && punchOutHistory[DateTime.Today][employeeId].Count > 0)
        {
            time = punchOutHistory[DateTime.Today][employeeId].Last();
            return true;
        }
        return false;
    }


}

//この下から

public class NullTimeTracker : TimeTracker, NullObject 
    {

        private static NullTimeTracker instance = new NullTimeTracker();

        private NullTimeTracker()
        {
            
        }

        public static TimeTracker Instance { get { return instance; } }


        public void PunchIn(int employeeId)
        {

        }

        public void PunchOut(int employeeId)
        {

        }

        public bool IsAtWork(int employeeId)
        {

            return false;

        }

    }




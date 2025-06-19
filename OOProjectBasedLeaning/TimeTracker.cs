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

    private Dictionary<DateTime, Dictionary<int, DateTime>> timestamp4PunchIn = new();
    private Dictionary<DateTime, Dictionary<int, DateTime>> timestamp4PunchOut = new();

    private Mode mode = Mode.PunchIn;

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
            throw new InvalidOperationException("従業員はすでに出勤しています");
        }

        if (!timestamp4PunchIn.ContainsKey(DateTime.Today))
        {
            timestamp4PunchIn[DateTime.Today] = new Dictionary<int, DateTime>();
        }

        timestamp4PunchIn[DateTime.Today][employeeId] = DateTime.Now;
    }

    public void PunchOut(int employeeId)
    {
        if (!IsAtWork(employeeId))
        {
            throw new InvalidOperationException("従業員はすでに退勤しています");
        }

        if (!timestamp4PunchOut.ContainsKey(DateTime.Today))
        {
            timestamp4PunchOut[DateTime.Today] = new Dictionary<int, DateTime>();
        }

        timestamp4PunchOut[DateTime.Today][employeeId] = DateTime.Now;
    }

    public bool IsAtWork(int employeeId)
    {
        return timestamp4PunchIn.ContainsKey(DateTime.Today)
            && timestamp4PunchIn[DateTime.Today].ContainsKey(employeeId)
            && (!timestamp4PunchOut.ContainsKey(DateTime.Today)
                || !timestamp4PunchOut[DateTime.Today].ContainsKey(employeeId));
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




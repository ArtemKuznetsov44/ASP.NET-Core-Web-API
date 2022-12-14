namespace MetricsAgent.Job
{
    // For keeping timeTable of strarting tasks.
    public class JobSchedule
    {
        public JobSchedule(Type jobType, string cronExpression)
        {
            JobType = jobType; 
            CronExpression = cronExpression;
        }
        public Type JobType { get; } // For save jobType
        public string CronExpression { get; } // Sepcifieg string in cron format.
    }
}

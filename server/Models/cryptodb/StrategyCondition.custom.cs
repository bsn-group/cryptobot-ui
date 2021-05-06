namespace CryptobotUi.Models.Cryptodb
{
    partial class StrategyCondition
    {
        public StrategyCondition()
        {
            this.condition_group = ConditionGroups.OPEN.ToString();
            this.last_observed = 180;
            this.time_frame = 60;
            this.created_time = System.DateTime.UtcNow;
        }
    }
}
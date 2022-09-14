using FluentMigrator; 

namespace MetricsAgent.Migrations
{
    // This is our first real migration.
    [Migration(1)]
    public class FirstMigration : Migration
    {
        public override void Down()
        {
            Delete.Table("cpumetrics");
            Delete.Table("dotnetmetrics");
            Delete.Table("hddmetrics");
            Delete.Table("networkmetrics");
            Delete.Table("rammetrics");
        }

        public override void Up()
        {
            #region Create table for CpuMetrics:

            Create.Table("cpumetrics")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("value").AsInt32()
                .WithColumn("time").AsInt64();

            #endregion

            #region Create table for DotNetMetrics:

            Create.Table("dotnetmetrics")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("value").AsInt32()
                .WithColumn("time").AsInt64();

            #endregion

            #region Create table for HddMetrics:

            Create.Table("hddmetrics")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("value").AsInt32()
                .WithColumn("time").AsInt64();

            #endregion

            #region Create table for NetworkMetrics:

            Create.Table("networkmetrics")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("value").AsInt32()
                .WithColumn("time").AsInt64();

            #endregion

            #region Create table for RamMetrics:

            Create.Table("rammetrics")
                .WithColumn("id").AsInt32().PrimaryKey().Identity()
                .WithColumn("value").AsInt32()
                .WithColumn("time").AsInt64();

            #endregion
        }
    }
}

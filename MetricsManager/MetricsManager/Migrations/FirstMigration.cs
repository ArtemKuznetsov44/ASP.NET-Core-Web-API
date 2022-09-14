using FluentMigrator;

namespace MetricsManager.Migrations
{
    [Migration(1)]
    public class FirstMigration : Migration
    {
        public override void Down() => Delete.Table("agents");

        public override void Up() => Create.Table("agents")
            .WithColumn("agentId").AsInt32().PrimaryKey().Identity()
            .WithColumn("agentUri").AsString()
            .WithColumn("enable").AsBoolean();
    }
}

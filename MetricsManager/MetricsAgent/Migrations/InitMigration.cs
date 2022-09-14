using FluentMigrator;

namespace MetricsAgent.Migrations
{
    // This migration is empty -> when we did not create our dateBase table.
    [Migration(0)] // Atribute with a migration number. 
    public class InitMigration : Migration
    {
        // This method, should be called when we what to apply our old migration.
        public override void Down() { }
       
        // This method sould be called, when we want to add or apply new migration.
        public override void Up() { }
    }
}

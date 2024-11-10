using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace proj1.Migrations
{
    /// <inheritdoc />
    public partial class addgroupedproductssequence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.Sql("CREATE SEQUENCE addgroupedproductssequence START WITH 1 INCREMENT BY 1;");
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.Sql("DROP SEQUENCE addgroupedproductssequence;");
        }
    }
}

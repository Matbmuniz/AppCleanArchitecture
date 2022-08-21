using Microsoft.EntityFrameworkCore.Migrations;

namespace AppCleanArchitecture.Infra.Data.Migrations
{
    public partial class SeedProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, Stock, Image, CategoryId) " +
                "VALUES('Caderno espiral', 'Caderno espiral 100 folhas', 7.45,50, 'caderno1.jpg',1)");

            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, Stock, Image, CategoryId) " +
                "VALUES('Borracha', 'Borracha 2 cor', 1.50,10, 'borracha.jpg',1)");

            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, Stock, Image, CategoryId) " +
                "VALUES('notebook', 'notebook dell', 45.80,5, 'notebook.jpg',2)");

            migrationBuilder.Sql("INSERT INTO Products(Name, Description, Price, Stock, Image, CategoryId) " +
                "VALUES('Relogio', 'Apple Watch', 25.00,7, 'appleWatch.jpg',3)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Products");
        }
    }
}

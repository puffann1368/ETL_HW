using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ETLProcess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    PayerAccountId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MasterAccountId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AccountName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.PayerAccountId);
                });

            migrationBuilder.CreateTable(
                name: "Productions",
                columns: table => new
                {
                    ProductionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductionName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productions", x => x.ProductionID);
                });

            migrationBuilder.CreateTable(
                name: "LineItems_01",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountPayerAccountId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductionID = table.Column<int>(type: "int", nullable: false),
                    LineItemType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UsageAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnblendedRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnblendedCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsageStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsageEndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineItems_01", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LineItems_01_Accounts_AccountPayerAccountId",
                        column: x => x.AccountPayerAccountId,
                        principalTable: "Accounts",
                        principalColumn: "PayerAccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineItems_01_Productions_ProductionID",
                        column: x => x.ProductionID,
                        principalTable: "Productions",
                        principalColumn: "ProductionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LineItems_02",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountPayerAccountId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductionID = table.Column<int>(type: "int", nullable: false),
                    LineItemType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UsageAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnblendedRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnblendedCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsageStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsageEndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineItems_02", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LineItems_02_Accounts_AccountPayerAccountId",
                        column: x => x.AccountPayerAccountId,
                        principalTable: "Accounts",
                        principalColumn: "PayerAccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineItems_02_Productions_ProductionID",
                        column: x => x.ProductionID,
                        principalTable: "Productions",
                        principalColumn: "ProductionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LineItems_03",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountPayerAccountId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductionID = table.Column<int>(type: "int", nullable: false),
                    LineItemType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UsageAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnblendedRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnblendedCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsageStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsageEndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineItems_03", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LineItems_03_Accounts_AccountPayerAccountId",
                        column: x => x.AccountPayerAccountId,
                        principalTable: "Accounts",
                        principalColumn: "PayerAccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineItems_03_Productions_ProductionID",
                        column: x => x.ProductionID,
                        principalTable: "Productions",
                        principalColumn: "ProductionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LineItems_04",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountPayerAccountId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductionID = table.Column<int>(type: "int", nullable: false),
                    LineItemType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UsageAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnblendedRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnblendedCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsageStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsageEndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineItems_04", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LineItems_04_Accounts_AccountPayerAccountId",
                        column: x => x.AccountPayerAccountId,
                        principalTable: "Accounts",
                        principalColumn: "PayerAccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineItems_04_Productions_ProductionID",
                        column: x => x.ProductionID,
                        principalTable: "Productions",
                        principalColumn: "ProductionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LineItems_05",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountPayerAccountId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductionID = table.Column<int>(type: "int", nullable: false),
                    LineItemType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UsageAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnblendedRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnblendedCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsageStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsageEndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineItems_05", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LineItems_05_Accounts_AccountPayerAccountId",
                        column: x => x.AccountPayerAccountId,
                        principalTable: "Accounts",
                        principalColumn: "PayerAccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineItems_05_Productions_ProductionID",
                        column: x => x.ProductionID,
                        principalTable: "Productions",
                        principalColumn: "ProductionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LineItems_06",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountPayerAccountId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductionID = table.Column<int>(type: "int", nullable: false),
                    LineItemType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UsageAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnblendedRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnblendedCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsageStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsageEndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineItems_06", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LineItems_06_Accounts_AccountPayerAccountId",
                        column: x => x.AccountPayerAccountId,
                        principalTable: "Accounts",
                        principalColumn: "PayerAccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineItems_06_Productions_ProductionID",
                        column: x => x.ProductionID,
                        principalTable: "Productions",
                        principalColumn: "ProductionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LineItems_07",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountPayerAccountId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductionID = table.Column<int>(type: "int", nullable: false),
                    LineItemType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UsageAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnblendedRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnblendedCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsageStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsageEndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineItems_07", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LineItems_07_Accounts_AccountPayerAccountId",
                        column: x => x.AccountPayerAccountId,
                        principalTable: "Accounts",
                        principalColumn: "PayerAccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineItems_07_Productions_ProductionID",
                        column: x => x.ProductionID,
                        principalTable: "Productions",
                        principalColumn: "ProductionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LineItems_08",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountPayerAccountId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductionID = table.Column<int>(type: "int", nullable: false),
                    LineItemType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UsageAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnblendedRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnblendedCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsageStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsageEndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineItems_08", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LineItems_08_Accounts_AccountPayerAccountId",
                        column: x => x.AccountPayerAccountId,
                        principalTable: "Accounts",
                        principalColumn: "PayerAccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineItems_08_Productions_ProductionID",
                        column: x => x.ProductionID,
                        principalTable: "Productions",
                        principalColumn: "ProductionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LineItems_09",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountPayerAccountId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductionID = table.Column<int>(type: "int", nullable: false),
                    LineItemType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UsageAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnblendedRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnblendedCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsageStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsageEndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineItems_09", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LineItems_09_Accounts_AccountPayerAccountId",
                        column: x => x.AccountPayerAccountId,
                        principalTable: "Accounts",
                        principalColumn: "PayerAccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineItems_09_Productions_ProductionID",
                        column: x => x.ProductionID,
                        principalTable: "Productions",
                        principalColumn: "ProductionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LineItems_10",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountPayerAccountId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductionID = table.Column<int>(type: "int", nullable: false),
                    LineItemType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UsageAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnblendedRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnblendedCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsageStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsageEndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineItems_10", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LineItems_10_Accounts_AccountPayerAccountId",
                        column: x => x.AccountPayerAccountId,
                        principalTable: "Accounts",
                        principalColumn: "PayerAccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineItems_10_Productions_ProductionID",
                        column: x => x.ProductionID,
                        principalTable: "Productions",
                        principalColumn: "ProductionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LineItems_11",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountPayerAccountId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductionID = table.Column<int>(type: "int", nullable: false),
                    LineItemType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UsageAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnblendedRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnblendedCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsageStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsageEndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineItems_11", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LineItems_11_Accounts_AccountPayerAccountId",
                        column: x => x.AccountPayerAccountId,
                        principalTable: "Accounts",
                        principalColumn: "PayerAccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineItems_11_Productions_ProductionID",
                        column: x => x.ProductionID,
                        principalTable: "Productions",
                        principalColumn: "ProductionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LineItems_12",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountPayerAccountId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProductionID = table.Column<int>(type: "int", nullable: false),
                    LineItemType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UsageAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnblendedRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnblendedCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsageStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsageEndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineItems_12", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LineItems_12_Accounts_AccountPayerAccountId",
                        column: x => x.AccountPayerAccountId,
                        principalTable: "Accounts",
                        principalColumn: "PayerAccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LineItems_12_Productions_ProductionID",
                        column: x => x.ProductionID,
                        principalTable: "Productions",
                        principalColumn: "ProductionID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_01_AccountPayerAccountId",
                table: "LineItems_01",
                column: "AccountPayerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_01_ProductionID",
                table: "LineItems_01",
                column: "ProductionID");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_02_AccountPayerAccountId",
                table: "LineItems_02",
                column: "AccountPayerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_02_ProductionID",
                table: "LineItems_02",
                column: "ProductionID");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_03_AccountPayerAccountId",
                table: "LineItems_03",
                column: "AccountPayerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_03_ProductionID",
                table: "LineItems_03",
                column: "ProductionID");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_04_AccountPayerAccountId",
                table: "LineItems_04",
                column: "AccountPayerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_04_ProductionID",
                table: "LineItems_04",
                column: "ProductionID");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_05_AccountPayerAccountId",
                table: "LineItems_05",
                column: "AccountPayerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_05_ProductionID",
                table: "LineItems_05",
                column: "ProductionID");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_06_AccountPayerAccountId",
                table: "LineItems_06",
                column: "AccountPayerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_06_ProductionID",
                table: "LineItems_06",
                column: "ProductionID");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_07_AccountPayerAccountId",
                table: "LineItems_07",
                column: "AccountPayerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_07_ProductionID",
                table: "LineItems_07",
                column: "ProductionID");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_08_AccountPayerAccountId",
                table: "LineItems_08",
                column: "AccountPayerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_08_ProductionID",
                table: "LineItems_08",
                column: "ProductionID");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_09_AccountPayerAccountId",
                table: "LineItems_09",
                column: "AccountPayerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_09_ProductionID",
                table: "LineItems_09",
                column: "ProductionID");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_10_AccountPayerAccountId",
                table: "LineItems_10",
                column: "AccountPayerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_10_ProductionID",
                table: "LineItems_10",
                column: "ProductionID");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_11_AccountPayerAccountId",
                table: "LineItems_11",
                column: "AccountPayerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_11_ProductionID",
                table: "LineItems_11",
                column: "ProductionID");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_12_AccountPayerAccountId",
                table: "LineItems_12",
                column: "AccountPayerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_LineItems_12_ProductionID",
                table: "LineItems_12",
                column: "ProductionID");

            for(int i = 1;i<=12;i++){

                string index = i.ToString().PadLeft(2,'0');
                migrationBuilder.Sql($@"CREATE NONCLUSTERED INDEX [{"IXV_LineItems_"+index}]
                ON [dbo].[LineItems_{index.PadLeft(2, '0')}] ([AccountPayerAccountId],[Date])
                INCLUDE ([ProductionID],[UsageAmount],[UnblendedCost])");
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LineItems_01");

            migrationBuilder.DropTable(
                name: "LineItems_02");

            migrationBuilder.DropTable(
                name: "LineItems_03");

            migrationBuilder.DropTable(
                name: "LineItems_04");

            migrationBuilder.DropTable(
                name: "LineItems_05");

            migrationBuilder.DropTable(
                name: "LineItems_06");

            migrationBuilder.DropTable(
                name: "LineItems_07");

            migrationBuilder.DropTable(
                name: "LineItems_08");

            migrationBuilder.DropTable(
                name: "LineItems_09");

            migrationBuilder.DropTable(
                name: "LineItems_10");

            migrationBuilder.DropTable(
                name: "LineItems_11");

            migrationBuilder.DropTable(
                name: "LineItems_12");

            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Productions");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace mappa.Migrations
{
    /// <inheritdoc />
    public partial class MinorClassRelationsAndRelationsBetweenPersons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EthnicityId",
                table: "UnordinaryPersons",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GenderId",
                table: "UnordinaryPersons",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfessionId",
                table: "UnordinaryPersons",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReligionId",
                table: "UnordinaryPersons",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EthnicityId",
                table: "OrdinaryPersons",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GenderId",
                table: "OrdinaryPersons",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProfessionId",
                table: "OrdinaryPersons",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RelationId",
                table: "OrdinaryPersons",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReligionId",
                table: "OrdinaryPersons",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Ethnicities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ethnicities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IntraOrdinary",
                columns: table => new
                {
                    PersonIdA = table.Column<int>(type: "integer", nullable: false),
                    PersonIdB = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntraOrdinary", x => new { x.PersonIdA, x.PersonIdB });
                    table.ForeignKey(
                        name: "FK_IntraOrdinary_OrdinaryPersons_PersonIdA",
                        column: x => x.PersonIdA,
                        principalTable: "OrdinaryPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IntraOrdinary_OrdinaryPersons_PersonIdB",
                        column: x => x.PersonIdB,
                        principalTable: "OrdinaryPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IntraUnordinary",
                columns: table => new
                {
                    PersonIdA = table.Column<int>(type: "integer", nullable: false),
                    PersonIdB = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntraUnordinary", x => new { x.PersonIdA, x.PersonIdB });
                    table.ForeignKey(
                        name: "FK_IntraUnordinary_UnordinaryPersons_PersonIdA",
                        column: x => x.PersonIdA,
                        principalTable: "UnordinaryPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IntraUnordinary_UnordinaryPersons_PersonIdB",
                        column: x => x.PersonIdB,
                        principalTable: "UnordinaryPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdinaryPersonUnordinaryPerson",
                columns: table => new
                {
                    InteractionsWithOrdinaryId = table.Column<int>(type: "integer", nullable: false),
                    InteractionsWithUnordinaryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdinaryPersonUnordinaryPerson", x => new { x.InteractionsWithOrdinaryId, x.InteractionsWithUnordinaryId });
                    table.ForeignKey(
                        name: "FK_OrdinaryPersonUnordinaryPerson_OrdinaryPersons_Interactions~",
                        column: x => x.InteractionsWithOrdinaryId,
                        principalTable: "OrdinaryPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdinaryPersonUnordinaryPerson_UnordinaryPersons_Interactio~",
                        column: x => x.InteractionsWithUnordinaryId,
                        principalTable: "UnordinaryPersons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Professions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Relations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Religions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Religions", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnordinaryPersons_EthnicityId",
                table: "UnordinaryPersons",
                column: "EthnicityId");

            migrationBuilder.CreateIndex(
                name: "IX_UnordinaryPersons_GenderId",
                table: "UnordinaryPersons",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_UnordinaryPersons_ProfessionId",
                table: "UnordinaryPersons",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_UnordinaryPersons_ReligionId",
                table: "UnordinaryPersons",
                column: "ReligionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdinaryPersons_EthnicityId",
                table: "OrdinaryPersons",
                column: "EthnicityId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdinaryPersons_GenderId",
                table: "OrdinaryPersons",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdinaryPersons_ProfessionId",
                table: "OrdinaryPersons",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdinaryPersons_RelationId",
                table: "OrdinaryPersons",
                column: "RelationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdinaryPersons_ReligionId",
                table: "OrdinaryPersons",
                column: "ReligionId");

            migrationBuilder.CreateIndex(
                name: "IX_IntraOrdinary_PersonIdB",
                table: "IntraOrdinary",
                column: "PersonIdB");

            migrationBuilder.CreateIndex(
                name: "IX_IntraUnordinary_PersonIdB",
                table: "IntraUnordinary",
                column: "PersonIdB");

            migrationBuilder.CreateIndex(
                name: "IX_OrdinaryPersonUnordinaryPerson_InteractionsWithUnordinaryId",
                table: "OrdinaryPersonUnordinaryPerson",
                column: "InteractionsWithUnordinaryId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdinaryPersons_Ethnicities_EthnicityId",
                table: "OrdinaryPersons",
                column: "EthnicityId",
                principalTable: "Ethnicities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdinaryPersons_Genders_GenderId",
                table: "OrdinaryPersons",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdinaryPersons_Professions_ProfessionId",
                table: "OrdinaryPersons",
                column: "ProfessionId",
                principalTable: "Professions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdinaryPersons_Relations_RelationId",
                table: "OrdinaryPersons",
                column: "RelationId",
                principalTable: "Relations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdinaryPersons_Religions_ReligionId",
                table: "OrdinaryPersons",
                column: "ReligionId",
                principalTable: "Religions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UnordinaryPersons_Ethnicities_EthnicityId",
                table: "UnordinaryPersons",
                column: "EthnicityId",
                principalTable: "Ethnicities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UnordinaryPersons_Genders_GenderId",
                table: "UnordinaryPersons",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UnordinaryPersons_Professions_ProfessionId",
                table: "UnordinaryPersons",
                column: "ProfessionId",
                principalTable: "Professions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UnordinaryPersons_Religions_ReligionId",
                table: "UnordinaryPersons",
                column: "ReligionId",
                principalTable: "Religions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdinaryPersons_Ethnicities_EthnicityId",
                table: "OrdinaryPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdinaryPersons_Genders_GenderId",
                table: "OrdinaryPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdinaryPersons_Professions_ProfessionId",
                table: "OrdinaryPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdinaryPersons_Relations_RelationId",
                table: "OrdinaryPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdinaryPersons_Religions_ReligionId",
                table: "OrdinaryPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_UnordinaryPersons_Ethnicities_EthnicityId",
                table: "UnordinaryPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_UnordinaryPersons_Genders_GenderId",
                table: "UnordinaryPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_UnordinaryPersons_Professions_ProfessionId",
                table: "UnordinaryPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_UnordinaryPersons_Religions_ReligionId",
                table: "UnordinaryPersons");

            migrationBuilder.DropTable(
                name: "Ethnicities");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "IntraOrdinary");

            migrationBuilder.DropTable(
                name: "IntraUnordinary");

            migrationBuilder.DropTable(
                name: "OrdinaryPersonUnordinaryPerson");

            migrationBuilder.DropTable(
                name: "Professions");

            migrationBuilder.DropTable(
                name: "Relations");

            migrationBuilder.DropTable(
                name: "Religions");

            migrationBuilder.DropIndex(
                name: "IX_UnordinaryPersons_EthnicityId",
                table: "UnordinaryPersons");

            migrationBuilder.DropIndex(
                name: "IX_UnordinaryPersons_GenderId",
                table: "UnordinaryPersons");

            migrationBuilder.DropIndex(
                name: "IX_UnordinaryPersons_ProfessionId",
                table: "UnordinaryPersons");

            migrationBuilder.DropIndex(
                name: "IX_UnordinaryPersons_ReligionId",
                table: "UnordinaryPersons");

            migrationBuilder.DropIndex(
                name: "IX_OrdinaryPersons_EthnicityId",
                table: "OrdinaryPersons");

            migrationBuilder.DropIndex(
                name: "IX_OrdinaryPersons_GenderId",
                table: "OrdinaryPersons");

            migrationBuilder.DropIndex(
                name: "IX_OrdinaryPersons_ProfessionId",
                table: "OrdinaryPersons");

            migrationBuilder.DropIndex(
                name: "IX_OrdinaryPersons_RelationId",
                table: "OrdinaryPersons");

            migrationBuilder.DropIndex(
                name: "IX_OrdinaryPersons_ReligionId",
                table: "OrdinaryPersons");

            migrationBuilder.DropColumn(
                name: "EthnicityId",
                table: "UnordinaryPersons");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "UnordinaryPersons");

            migrationBuilder.DropColumn(
                name: "ProfessionId",
                table: "UnordinaryPersons");

            migrationBuilder.DropColumn(
                name: "ReligionId",
                table: "UnordinaryPersons");

            migrationBuilder.DropColumn(
                name: "EthnicityId",
                table: "OrdinaryPersons");

            migrationBuilder.DropColumn(
                name: "GenderId",
                table: "OrdinaryPersons");

            migrationBuilder.DropColumn(
                name: "ProfessionId",
                table: "OrdinaryPersons");

            migrationBuilder.DropColumn(
                name: "RelationId",
                table: "OrdinaryPersons");

            migrationBuilder.DropColumn(
                name: "ReligionId",
                table: "OrdinaryPersons");
        }
    }
}

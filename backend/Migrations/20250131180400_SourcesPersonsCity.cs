using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace mappa.Migrations
{
    /// <inheritdoc />
    public partial class SourcesPersonsCity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdinaryPersons_Relations_RelationId",
                table: "OrdinaryPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdinaryPersons_Religions_FormerReligionId",
                table: "OrdinaryPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_SecondarySources_Languages_LanguageId",
                table: "SecondarySources");

            migrationBuilder.DropForeignKey(
                name: "FK_SecondarySources_Types_TypeId",
                table: "SecondarySources");

            migrationBuilder.DropForeignKey(
                name: "FK_UnordinaryPersons_Religions_FormerReligionId",
                table: "UnordinaryPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_WrittenSources_Genres_GenreId",
                table: "WrittenSources");

            migrationBuilder.DropForeignKey(
                name: "FK_WrittenSources_Languages_LanguageId",
                table: "WrittenSources");

            migrationBuilder.DropTable(
                name: "Relations");

            migrationBuilder.DropIndex(
                name: "IX_UnordinaryPersons_FormerReligionId",
                table: "UnordinaryPersons");

            migrationBuilder.DropIndex(
                name: "IX_OrdinaryPersons_FormerReligionId",
                table: "OrdinaryPersons");

            migrationBuilder.DropIndex(
                name: "IX_OrdinaryPersons_RelationId",
                table: "OrdinaryPersons");

            migrationBuilder.DropColumn(
                name: "Patronage",
                table: "WrittenSources");

            migrationBuilder.DropColumn(
                name: "Depiction",
                table: "UnordinaryPersons");

            migrationBuilder.DropColumn(
                name: "Depiction",
                table: "OrdinaryPersons");

            migrationBuilder.DropColumn(
                name: "YearWritten",
                table: "WrittenSources");

            migrationBuilder.DropColumn(
                name: "BirthYear",
                table: "UnordinaryPersons");

            migrationBuilder.DropColumn(
                name: "BirthYear",
                table: "OrdinaryPersons");

            migrationBuilder.DropColumn(
                name: "DeathYear",
                table: "UnordinaryPersons");

            migrationBuilder.DropColumn(
                name: "DeathYear",
                table: "OrdinaryPersons");

            migrationBuilder.RenameColumn(
                name: "FormerReligionId",
                table: "UnordinaryPersons",
                newName: "ProbableDeathYear");

            migrationBuilder.RenameColumn(
                name: "RelationId",
                table: "OrdinaryPersons",
                newName: "ProbableDeathYear");

            migrationBuilder.RenameColumn(
                name: "FormerReligionId",
                table: "OrdinaryPersons",
                newName: "ProbableBirthYear");

            // migrationBuilder.AlterColumn<List<int>>(
            //     name: "YearWritten",
            //     table: "WrittenSources",
            //     type: "integer[]",
            //     nullable: true,
            //     oldClrType: typeof(DateOnly),
            //     oldType: "date",
            //     oldNullable: true);

            migrationBuilder.AlterColumn<List<string>>(
                name: "SurvivedCopies",
                table: "WrittenSources",
                type: "text[]",
                nullable: true,
                oldClrType: typeof(string[]),
                oldType: "text[]");

            migrationBuilder.AlterColumn<int>(
                name: "LanguageId",
                table: "WrittenSources",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<List<string>>(
                name: "KnownCopies",
                table: "WrittenSources",
                type: "text[]",
                nullable: true,
                oldClrType: typeof(string[]),
                oldType: "text[]");

            migrationBuilder.AlterColumn<int>(
                name: "GenreId",
                table: "WrittenSources",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<List<int>>(
                name: "YearWritten",
                table: "WrittenSources",
                type: "integer[]",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrdinaryPersonId",
                table: "WrittenSources",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnordinaryPersonId",
                table: "WrittenSources",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<List<int>>(
                name: "BirthYear",
                table: "UnordinaryPersons",
                type: "integer[]",
                nullable: true);

            // migrationBuilder.AlterColumn<List<int>>(
            //     name: "DeathYear",
            //     table: "UnordinaryPersons",
            //     type: "integer[]",
            //     nullable: true,
            //     oldClrType: typeof(DateOnly),
            //     oldType: "date",
            //     oldNullable: true);

            // migrationBuilder.AlterColumn<List<int>>(
            //     name: "BirthYear",
            //     table: "UnordinaryPersons",
            //     type: "integer[]",
            //     nullable: true,
            //     oldClrType: typeof(DateOnly),
            //     oldType: "date",
            //     oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BirthPlaceId",
                table: "UnordinaryPersons",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeathPlaceId",
                table: "UnordinaryPersons",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProbableBirthYear",
                table: "UnordinaryPersons",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "SecondarySources",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "LanguageId",
                table: "SecondarySources",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "OrdinaryPersonId",
                table: "Religions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnordinaryPersonId",
                table: "Religions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<List<int>>(
                name: "DeathYear",
                table: "UnordinaryPersons",
                type: "integer[]",
                nullable: true);

            migrationBuilder.AddColumn<List<int>>(
                name: "DeathYear",
                table: "OrdinaryPersons",
                type: "integer[]",
                nullable: true);

            // migrationBuilder.AlterColumn<List<int>>(
            //     name: "DeathYear",
            //     table: "OrdinaryPersons",
            //     type: "integer[]",
            //     nullable: true,
            //     oldClrType: typeof(DateOnly),
            //     oldType: "date",
            //     oldNullable: true);

            // migrationBuilder.AlterColumn<List<int>>(
            //     name: "BirthYear",
            //     table: "OrdinaryPersons",
            //     type: "integer[]",
            //     nullable: true,
            //     oldClrType: typeof(DateOnly),
            //     oldType: "date",
            //     oldNullable: true);

            migrationBuilder.AddColumn<List<int>>(
                name: "BirthYear",
                table: "OrdinaryPersons",
                type: "integer[]",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BackgroundCityId",
                table: "OrdinaryPersons",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "OrdinaryPersons",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AsciiName = table.Column<string>(type: "text", nullable: false),
                    GeoNamesId = table.Column<string>(type: "text", nullable: true),
                    AlternateNames = table.Column<List<string>>(type: "text[]", nullable: true),
                    CountryCode = table.Column<string>(type: "text", nullable: true),
                    WrittenSourceId = table.Column<int>(type: "integer", nullable: true),
                    WrittenSourceId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cities_WrittenSources_WrittenSourceId",
                        column: x => x.WrittenSourceId,
                        principalTable: "WrittenSources",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cities_WrittenSources_WrittenSourceId1",
                        column: x => x.WrittenSourceId1,
                        principalTable: "WrittenSources",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WrittenSources_OrdinaryPersonId",
                table: "WrittenSources",
                column: "OrdinaryPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_WrittenSources_UnordinaryPersonId",
                table: "WrittenSources",
                column: "UnordinaryPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_UnordinaryPersons_BirthPlaceId",
                table: "UnordinaryPersons",
                column: "BirthPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_UnordinaryPersons_DeathPlaceId",
                table: "UnordinaryPersons",
                column: "DeathPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Religions_OrdinaryPersonId",
                table: "Religions",
                column: "OrdinaryPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Religions_UnordinaryPersonId",
                table: "Religions",
                column: "UnordinaryPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdinaryPersons_BackgroundCityId",
                table: "OrdinaryPersons",
                column: "BackgroundCityId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdinaryPersons_LocationId",
                table: "OrdinaryPersons",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_WrittenSourceId",
                table: "Cities",
                column: "WrittenSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_WrittenSourceId1",
                table: "Cities",
                column: "WrittenSourceId1");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdinaryPersons_Cities_BackgroundCityId",
                table: "OrdinaryPersons",
                column: "BackgroundCityId",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdinaryPersons_Cities_LocationId",
                table: "OrdinaryPersons",
                column: "LocationId",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Religions_OrdinaryPersons_OrdinaryPersonId",
                table: "Religions",
                column: "OrdinaryPersonId",
                principalTable: "OrdinaryPersons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Religions_UnordinaryPersons_UnordinaryPersonId",
                table: "Religions",
                column: "UnordinaryPersonId",
                principalTable: "UnordinaryPersons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SecondarySources_Languages_LanguageId",
                table: "SecondarySources",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SecondarySources_Types_TypeId",
                table: "SecondarySources",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UnordinaryPersons_Cities_BirthPlaceId",
                table: "UnordinaryPersons",
                column: "BirthPlaceId",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UnordinaryPersons_Cities_DeathPlaceId",
                table: "UnordinaryPersons",
                column: "DeathPlaceId",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WrittenSources_Genres_GenreId",
                table: "WrittenSources",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WrittenSources_Languages_LanguageId",
                table: "WrittenSources",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WrittenSources_OrdinaryPersons_OrdinaryPersonId",
                table: "WrittenSources",
                column: "OrdinaryPersonId",
                principalTable: "OrdinaryPersons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WrittenSources_UnordinaryPersons_UnordinaryPersonId",
                table: "WrittenSources",
                column: "UnordinaryPersonId",
                principalTable: "UnordinaryPersons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdinaryPersons_Cities_BackgroundCityId",
                table: "OrdinaryPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdinaryPersons_Cities_LocationId",
                table: "OrdinaryPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_Religions_OrdinaryPersons_OrdinaryPersonId",
                table: "Religions");

            migrationBuilder.DropForeignKey(
                name: "FK_Religions_UnordinaryPersons_UnordinaryPersonId",
                table: "Religions");

            migrationBuilder.DropForeignKey(
                name: "FK_SecondarySources_Languages_LanguageId",
                table: "SecondarySources");

            migrationBuilder.DropForeignKey(
                name: "FK_SecondarySources_Types_TypeId",
                table: "SecondarySources");

            migrationBuilder.DropForeignKey(
                name: "FK_UnordinaryPersons_Cities_BirthPlaceId",
                table: "UnordinaryPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_UnordinaryPersons_Cities_DeathPlaceId",
                table: "UnordinaryPersons");

            migrationBuilder.DropForeignKey(
                name: "FK_WrittenSources_Genres_GenreId",
                table: "WrittenSources");

            migrationBuilder.DropForeignKey(
                name: "FK_WrittenSources_Languages_LanguageId",
                table: "WrittenSources");

            migrationBuilder.DropForeignKey(
                name: "FK_WrittenSources_OrdinaryPersons_OrdinaryPersonId",
                table: "WrittenSources");

            migrationBuilder.DropForeignKey(
                name: "FK_WrittenSources_UnordinaryPersons_UnordinaryPersonId",
                table: "WrittenSources");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_WrittenSources_OrdinaryPersonId",
                table: "WrittenSources");

            migrationBuilder.DropIndex(
                name: "IX_WrittenSources_UnordinaryPersonId",
                table: "WrittenSources");

            migrationBuilder.DropIndex(
                name: "IX_UnordinaryPersons_BirthPlaceId",
                table: "UnordinaryPersons");

            migrationBuilder.DropIndex(
                name: "IX_UnordinaryPersons_DeathPlaceId",
                table: "UnordinaryPersons");

            migrationBuilder.DropIndex(
                name: "IX_Religions_OrdinaryPersonId",
                table: "Religions");

            migrationBuilder.DropIndex(
                name: "IX_Religions_UnordinaryPersonId",
                table: "Religions");

            migrationBuilder.DropIndex(
                name: "IX_OrdinaryPersons_BackgroundCityId",
                table: "OrdinaryPersons");

            migrationBuilder.DropIndex(
                name: "IX_OrdinaryPersons_LocationId",
                table: "OrdinaryPersons");

            migrationBuilder.DropColumn(
                name: "OrdinaryPersonId",
                table: "WrittenSources");

            migrationBuilder.DropColumn(
                name: "UnordinaryPersonId",
                table: "WrittenSources");

            migrationBuilder.DropColumn(
                name: "BirthPlaceId",
                table: "UnordinaryPersons");

            migrationBuilder.DropColumn(
                name: "DeathPlaceId",
                table: "UnordinaryPersons");

            migrationBuilder.DropColumn(
                name: "ProbableBirthYear",
                table: "UnordinaryPersons");

            migrationBuilder.DropColumn(
                name: "OrdinaryPersonId",
                table: "Religions");

            migrationBuilder.DropColumn(
                name: "UnordinaryPersonId",
                table: "Religions");

            migrationBuilder.DropColumn(
                name: "BackgroundCityId",
                table: "OrdinaryPersons");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "OrdinaryPersons");

            migrationBuilder.DropColumn(
                name: "YearWritten",
                table: "WrittenSources");

            migrationBuilder.DropColumn(
                name: "BirthYear",
                table: "UnordinaryPersons");

            migrationBuilder.DropColumn(
                name: "BirthYear",
                table: "OrdinaryPersons");

            migrationBuilder.DropColumn(
                name: "DeathYear",
                table: "UnordinaryPersons");

            migrationBuilder.DropColumn(
                name: "DeathYear",
                table: "OrdinaryPersons");

            migrationBuilder.RenameColumn(
                name: "ProbableDeathYear",
                table: "UnordinaryPersons",
                newName: "FormerReligionId");

            migrationBuilder.RenameColumn(
                name: "ProbableDeathYear",
                table: "OrdinaryPersons",
                newName: "RelationId");

            migrationBuilder.RenameColumn(
                name: "ProbableBirthYear",
                table: "OrdinaryPersons",
                newName: "FormerReligionId");

            // migrationBuilder.AlterColumn<DateOnly>(
            //     name: "YearWritten",
            //     table: "WrittenSources",
            //     type: "date",
            //     nullable: true,
            //     oldClrType: typeof(List<int>),
            //     oldType: "integer[]",
            //     oldNullable: true);

            migrationBuilder.AlterColumn<string[]>(
                name: "SurvivedCopies",
                table: "WrittenSources",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0],
                oldClrType: typeof(List<string>),
                oldType: "text[]",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LanguageId",
                table: "WrittenSources",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string[]>(
                name: "KnownCopies",
                table: "WrittenSources",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0],
                oldClrType: typeof(List<string>),
                oldType: "text[]",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "GenreId",
                table: "WrittenSources",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<List<int>>(
                name: "YearWritten",
                table: "WrittenSources",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Patronage",
                table: "WrittenSources",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            // migrationBuilder.AlterColumn<DateOnly>(
            //     name: "DeathYear",
            //     table: "UnordinaryPersons",
            //     type: "date",
            //     nullable: true,
            //     oldClrType: typeof(List<int>),
            //     oldType: "integer[]",
            //     oldNullable: true);

            // migrationBuilder.AlterColumn<DateOnly>(
            //     name: "BirthYear",
            //     table: "UnordinaryPersons",
            //     type: "date",
            //     nullable: true,
            //     oldClrType: typeof(List<int>),
            //     oldType: "integer[]",
            //     oldNullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DeathYear",
                table: "UnordinaryPersons",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DeathYear",
                table: "OrdinaryPersons",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "BirthYear",
                table: "UnordinaryPersons",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Depiction",
                table: "UnordinaryPersons",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TypeId",
                table: "SecondarySources",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LanguageId",
                table: "SecondarySources",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            // migrationBuilder.AlterColumn<DateOnly>(
            //     name: "DeathYear",
            //     table: "OrdinaryPersons",
            //     type: "date",
            //     nullable: true,
            //     oldClrType: typeof(List<int>),
            //     oldType: "integer[]",
            //     oldNullable: true);

            // migrationBuilder.AlterColumn<DateOnly>(
            //     name: "BirthYear",
            //     table: "OrdinaryPersons",
            //     type: "date",
            //     nullable: true,
            //     oldClrType: typeof(List<int>),
            //     oldType: "integer[]",
            //     oldNullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "BirthYear",
                table: "OrdinaryPersons",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Depiction",
                table: "OrdinaryPersons",
                type: "text",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_UnordinaryPersons_FormerReligionId",
                table: "UnordinaryPersons",
                column: "FormerReligionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdinaryPersons_FormerReligionId",
                table: "OrdinaryPersons",
                column: "FormerReligionId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdinaryPersons_RelationId",
                table: "OrdinaryPersons",
                column: "RelationId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdinaryPersons_Relations_RelationId",
                table: "OrdinaryPersons",
                column: "RelationId",
                principalTable: "Relations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdinaryPersons_Religions_FormerReligionId",
                table: "OrdinaryPersons",
                column: "FormerReligionId",
                principalTable: "Religions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SecondarySources_Languages_LanguageId",
                table: "SecondarySources",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SecondarySources_Types_TypeId",
                table: "SecondarySources",
                column: "TypeId",
                principalTable: "Types",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UnordinaryPersons_Religions_FormerReligionId",
                table: "UnordinaryPersons",
                column: "FormerReligionId",
                principalTable: "Religions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WrittenSources_Genres_GenreId",
                table: "WrittenSources",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WrittenSources_Languages_LanguageId",
                table: "WrittenSources",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

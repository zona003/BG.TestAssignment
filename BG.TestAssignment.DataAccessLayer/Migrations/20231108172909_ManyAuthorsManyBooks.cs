﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BG.TestAssignment.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ManyAuthorsManyBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthorBooks",
                columns: table => new
                {
                    AuthorsId = table.Column<int>(type: "integer", nullable: false),
                    BooksId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorsBooks", x => new { x.AuthorsId, x.BooksId });
                    table.ForeignKey(
                        name: "FK_AuthorsBooks_Authors_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorsBooks_Books_BooksId",
                        column: x => x.BooksId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.Sql("INSERT INTO public.\"AuthorBooks\" " +
                                 "SELECT" +
                                 "  \"AuthorId\" AS \"AuthorsId\"\r" +
                                 "  \"Id\" AS \"BooksId\", " +
                                 " FROM public.\"Books\"");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_AuthorId",
                table: "Books");

            migrationBuilder.CreateIndex(
                name: "IX_AuthorsBooks_BooksId",
                table: "AuthorsBooks",
                column: "BooksId");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.Sql(@"
            UPDATE public.""Books"" b
            SET ""AuthorId"" = ab.""AuthorId""
        FROM public.""AuthorBooks"" ab
            WHERE b.""Id"" = ab.""BooksId"";
        ");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.DropTable(
                name: "AuthorsBooks");
        }
    }
}

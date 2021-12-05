using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CadastroDeUsuarios.Migrations
{
    public partial class FixDatabase2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);


            migrationBuilder.AlterColumn<DateTime>(
                name: "LastLogin",
                table: "AspNetUsers",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "AspNetUsers",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);



            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            /*    migrationBuilder.AlterColumn<Guid>(
                    name: "RoleId",
                    table: "AspNetUserRoles",
                    type: "uuid",
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "text");

                migrationBuilder.AlterColumn<Guid>(
                    name: "UserId",
                    table: "AspNetUserRoles",
                    type: "uuid",
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "text");

                migrationBuilder.AlterColumn<Guid>(
                    name: "UserId",
                    table: "AspNetUserLogins",
                    type: "uuid",
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "text");

                migrationBuilder.AlterColumn<string>(
                    name: "ProviderKey",
                    table: "AspNetUserLogins",
                    type: "text",
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "character varying(128)",
                    oldMaxLength: 128);

                migrationBuilder.AlterColumn<string>(
                    name: "LoginProvider",
                    table: "AspNetUserLogins",
                    type: "text",
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "character varying(128)",
                    oldMaxLength: 128);

                migrationBuilder.AlterColumn<Guid>(
                    name: "UserId",
                    table: "AspNetUserClaims",
                    type: "uuid",
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "text");

                migrationBuilder.AlterColumn<Guid>(
                    name: "Id",
                    table: "AspNetRoles",
                    type: "uuid",
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "text");

                migrationBuilder.AlterColumn<Guid>(
                    name: "RoleId",
                    table: "AspNetRoleClaims",
                    type: "uuid",
                    nullable: false,
                    oldClrType: typeof(string),
                    oldType: "text");
            */
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");


            migrationBuilder.AlterColumn<DateTime>(
                name: "LastLogin",
                table: "AspNetUsers",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "AspNetUsers",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");



            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: "");

            /*   migrationBuilder.AlterColumn<string>(
                   name: "RoleId",
                   table: "AspNetUserRoles",
                   type: "text",
                   nullable: false,
                   oldClrType: typeof(Guid),
                   oldType: "uuid");

               migrationBuilder.AlterColumn<string>(
                   name: "UserId",
                   table: "AspNetUserRoles",
                   type: "text",
                   nullable: false,
                   oldClrType: typeof(Guid),
                   oldType: "uuid");

               migrationBuilder.AlterColumn<string>(
                   name: "UserId",
                   table: "AspNetUserLogins",
                   type: "text",
                   nullable: false,
                   oldClrType: typeof(Guid),
                   oldType: "uuid");

               migrationBuilder.AlterColumn<string>(
                   name: "ProviderKey",
                   table: "AspNetUserLogins",
                   type: "character varying(128)",
                   maxLength: 128,
                   nullable: false,
                   oldClrType: typeof(string),
                   oldType: "text");

               migrationBuilder.AlterColumn<string>(
                   name: "LoginProvider",
                   table: "AspNetUserLogins",
                   type: "character varying(128)",
                   maxLength: 128,
                   nullable: false,
                   oldClrType: typeof(string),
                   oldType: "text");

               migrationBuilder.AlterColumn<string>(
                   name: "UserId",
                   table: "AspNetUserClaims",
                   type: "text",
                   nullable: false,
                   oldClrType: typeof(Guid),
                   oldType: "uuid");

               migrationBuilder.AlterColumn<string>(
                   name: "Id",
                   table: "AspNetRoles",
                   type: "text",
                   nullable: false,
                   oldClrType: typeof(Guid),
                   oldType: "uuid");

               migrationBuilder.AlterColumn<string>(
                   name: "RoleId",
                   table: "AspNetRoleClaims",
                   type: "text",
                   nullable: false,
                   oldClrType: typeof(Guid),
                   oldType: "uuid");
            */
        }
    }
}

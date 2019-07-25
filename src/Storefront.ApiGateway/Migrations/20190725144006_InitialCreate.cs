using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Storefront.ApiGateway.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "identity");

            migrationBuilder.CreateTable(
                name: "tenant",
                schema: "identity",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    created_at = table.Column<DateTime>(nullable: false),
                    business_name = table.Column<string>(maxLength: 80, nullable: false),
                    billing_email = table.Column<string>(maxLength: 80, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role",
                schema: "identity",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    tenant_id = table.Column<long>(nullable: false),
                    name = table.Column<string>(maxLength: 80, nullable: true),
                    normalized_name = table.Column<string>(maxLength: 80, nullable: true),
                    concurrency_stamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role", x => new { x.id, x.tenant_id });
                    table.ForeignKey(
                        name: "fk_role__tenant",
                        column: x => x.tenant_id,
                        principalSchema: "identity",
                        principalTable: "tenant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "user",
                schema: "identity",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    tenant_id = table.Column<long>(nullable: false),
                    user_name = table.Column<string>(maxLength: 50, nullable: false),
                    normalized_user_name = table.Column<string>(maxLength: 50, nullable: false),
                    email = table.Column<string>(maxLength: 80, nullable: false),
                    normalized_email = table.Column<string>(maxLength: 80, nullable: false),
                    email_confirmed = table.Column<bool>(nullable: false),
                    password_hash = table.Column<string>(maxLength: 1024, nullable: false),
                    security_stamp = table.Column<string>(nullable: true),
                    concurrency_stamp = table.Column<string>(nullable: true),
                    phone_number = table.Column<string>(maxLength: 20, nullable: true),
                    phone_number_confirmed = table.Column<bool>(nullable: false),
                    two_factor_enabled = table.Column<bool>(nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(nullable: true),
                    lockout_enabled = table.Column<bool>(nullable: false),
                    access_failed_count = table.Column<int>(nullable: false),
                    created_at = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => new { x.id, x.tenant_id });
                    table.ForeignKey(
                        name: "fk_user__tenant",
                        column: x => x.tenant_id,
                        principalSchema: "identity",
                        principalTable: "tenant",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "role_claim",
                schema: "identity",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false),
                    tenant_id = table.Column<long>(nullable: false),
                    role_id = table.Column<long>(nullable: false),
                    claim_type = table.Column<string>(maxLength: 255, nullable: true),
                    claim_value = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_claim", x => new { x.id, x.tenant_id });
                    table.ForeignKey(
                        name: "fk_role_claim__role",
                        columns: x => new { x.role_id, x.tenant_id },
                        principalSchema: "identity",
                        principalTable: "role",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_claim",
                schema: "identity",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    tenant_id = table.Column<long>(nullable: false),
                    user_id = table.Column<long>(nullable: false),
                    claim_type = table.Column<string>(maxLength: 255, nullable: true),
                    claim_value = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_claim", x => new { x.id, x.tenant_id });
                    table.ForeignKey(
                        name: "fk_user_claim__user",
                        columns: x => new { x.user_id, x.tenant_id },
                        principalSchema: "identity",
                        principalTable: "user",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_login",
                schema: "identity",
                columns: table => new
                {
                    login_provider = table.Column<string>(maxLength: 128, nullable: false),
                    provider_key = table.Column<string>(maxLength: 128, nullable: false),
                    tenant_id = table.Column<long>(nullable: false),
                    provider_display_name = table.Column<string>(maxLength: 128, nullable: true),
                    user_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_login", x => new { x.login_provider, x.provider_key, x.tenant_id });
                    table.ForeignKey(
                        name: "fk_user_login__user",
                        columns: x => new { x.user_id, x.tenant_id },
                        principalSchema: "identity",
                        principalTable: "user",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_role",
                schema: "identity",
                columns: table => new
                {
                    user_id = table.Column<long>(nullable: false),
                    role_id = table.Column<long>(nullable: false),
                    tenant_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_role", x => new { x.user_id, x.role_id, x.tenant_id });
                    table.ForeignKey(
                        name: "fk_user_role__role",
                        columns: x => new { x.role_id, x.tenant_id },
                        principalSchema: "identity",
                        principalTable: "role",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_role__user",
                        columns: x => new { x.user_id, x.tenant_id },
                        principalSchema: "identity",
                        principalTable: "user",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_token",
                schema: "identity",
                columns: table => new
                {
                    user_id = table.Column<long>(nullable: false),
                    login_provider = table.Column<string>(maxLength: 50, nullable: false),
                    name = table.Column<string>(maxLength: 80, nullable: false),
                    tenant_id = table.Column<long>(nullable: false),
                    value = table.Column<string>(maxLength: 1024, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_token", x => new { x.user_id, x.login_provider, x.name, x.tenant_id });
                    table.ForeignKey(
                        name: "fk_user_token__user",
                        columns: x => new { x.user_id, x.tenant_id },
                        principalSchema: "identity",
                        principalTable: "user",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_role_name",
                schema: "identity",
                table: "role",
                column: "normalized_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_role_tenant_id",
                schema: "identity",
                table: "role",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_claim_role_id_tenant_id",
                schema: "identity",
                table: "role_claim",
                columns: new[] { "role_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "ix_email",
                schema: "identity",
                table: "user",
                column: "normalized_email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_name",
                schema: "identity",
                table: "user",
                column: "normalized_user_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_tenant_id",
                schema: "identity",
                table: "user",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_claim_user_id_tenant_id",
                schema: "identity",
                table: "user_claim",
                columns: new[] { "user_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_user_login_user_id_tenant_id",
                schema: "identity",
                table: "user_login",
                columns: new[] { "user_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_user_role_role_id_tenant_id",
                schema: "identity",
                table: "user_role",
                columns: new[] { "role_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_user_role_user_id_tenant_id",
                schema: "identity",
                table: "user_role",
                columns: new[] { "user_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_user_token_user_id_tenant_id",
                schema: "identity",
                table: "user_token",
                columns: new[] { "user_id", "tenant_id" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "role_claim",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "user_claim",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "user_login",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "user_role",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "user_token",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "role",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "user",
                schema: "identity");

            migrationBuilder.DropTable(
                name: "tenant",
                schema: "identity");
        }
    }
}

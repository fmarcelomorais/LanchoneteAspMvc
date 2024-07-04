using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanchoneteAspMvc.Migrations
{
    /// <inheritdoc />
    public partial class PedidoDetalhes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "Categorias",
            //    columns: table => new
            //    {
            //        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            //        Nome = table.Column<string>(type: "VARCHAR(100)", nullable: false),
            //        Descricao = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Categorias", x => x.Id);
            //    });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Sobrenome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Emai = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PedidoTotal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalItensPedido = table.Column<int>(type: "int", nullable: false),
                    PedidoEnviado = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    PedidoEntregueEm = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                });


            migrationBuilder.CreateTable(
                name: "PedidosDetalhes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    LancheId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PedidoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PedidosDetalhes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PedidosDetalhes_Lanches_LancheId",
                        column: x => x.LancheId,
                        principalTable: "Lanches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PedidosDetalhes_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Itens_LancheId",
                table: "Itens",
                column: "LancheId");

         

            migrationBuilder.CreateIndex(
                name: "IX_PedidosDetalhes_LancheId",
                table: "PedidosDetalhes",
                column: "LancheId");

            migrationBuilder.CreateIndex(
                name: "IX_PedidosDetalhes_PedidoId",
                table: "PedidosDetalhes",
                column: "PedidoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Itens");

            migrationBuilder.DropTable(
                name: "PedidosDetalhes");

            migrationBuilder.DropTable(
                name: "Lanches");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}

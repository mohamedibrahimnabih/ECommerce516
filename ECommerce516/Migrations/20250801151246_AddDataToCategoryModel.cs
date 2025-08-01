using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerce516.Migrations
{
    /// <inheritdoc />
    public partial class AddDataToCategoryModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into Categories (Name, Description, Status) values ('Mobiles', 'Suspendisse potenti. In eleifend quam a odio. In hac habitasse platea dictumst.Maecenas ut massa quis augue luctus tincidunt. Nulla mollis molestie lorem. Quisque ut erat.', 1);insert into Categories (Name, Description, Status) values ('Laptops', 'Maecenas tristique, est et tempus semper, est quam pharetra magna, ac consequat metus sapien ut nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Mauris viverra diam vitae quam. Suspendisse potenti.', 1);insert into Categories (Name, Description, Status) values ('Cameras', 'In quis justo. Maecenas rhoncus aliquam lacus. Morbi quis tortor id nulla ultrices aliquet.', 1);insert into Categories (Name, Description, Status) values ('Accessories', 'Duis consequat dui nec nisi volutpat eleifend. Donec ut dolor. Morbi vel lectus in quam fringilla rhoncus.', 0);insert into Categories (Name, Description, Status) values ('Comouters', 'Quisque porta volutpat erat. Quisque erat eros, viverra eget, congue eget, semper rutrum, nulla. Nunc purus.Phasellus in felis. Donec semper sapien a libero. Nam dui.', 1);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("TRUNCATE TABLE Categories");
        }
    }
}

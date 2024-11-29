using CSParking.Models.Database.CsParking;
using CSParking.Models.Database.CsParking.Context;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSParking.Migrations.Main
{
    public class CreateTrigger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
                create trigger PreventPlacesMultipleRows
                on dbo.{nameof(MainContext.Places)}
                instead of insert
                as 
                begin
                        if (select count(*) from dbo.{nameof(MainContext.Places)}) = 0
                        begin
                            insert into dbo.{nameof(MainContext.Places)} ({nameof(Places.Id)}, {nameof(Places.CardCount)}, {nameof(Places.TicketCount)})
                            select {nameof(Places.Id)}, {nameof(Places.CardCount)}, {nameof(Places.TicketCount)} from inserted;
                        end
                        else
                        begin
                            raiserror('There is only one row available in this table.', 16, 1);
                        end		
                end
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"drop trigger PreventPlacesMultipleRows");
        }
    }
}

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
            var placesTable = nameof(MainContext.Places);

            migrationBuilder.Sql($@"
                create trigger PreventPlacesMultipleRows
                on dbo.{placesTable}
                instead of insert
                as 
                begin
                        if (select count(*) from dbo.{placesTable}) = 0
                        begin
                            insert into dbo.{placesTable} ({nameof(Places.Id)}, {nameof(Places.CardCount)}, {nameof(Places.TicketCount)})
                            select {nameof(Places.Id)}, {nameof(Places.CardCount)}, {nameof(Places.TicketCount)} from inserted;
                        end
                        else
                        begin
                            raiserror('There is only one row available in this table.', 16, 1);
                        end		
                end;
                IF NOT EXISTS (SELECT * FROM {placesTable})
                BEGIN
                    INSERT INTO Places ({nameof(Places.TicketCount)}, {nameof(Places.CardCount)}) VALUES (0,0);
                END
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"drop trigger PreventPlacesMultipleRows");
        }
    }
}

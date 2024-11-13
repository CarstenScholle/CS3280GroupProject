using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group10ProjectSearchBar.Search
{
    internal class clsSearchSQL
    {
        /*
        - SELECT * FROM Invoices
        - SELECT * FROM Invoices WHERE InvoiceNum = 5000
        - SELECT * FROM Invoices WHERE InvoiceNum = 5000 AND InvoiceDate = #4/13/2018#
        - SELECT * FROM Invoices WHERE InvoiceNum = 5000 AND InvoiceDate = #4/13/2018# AND TotalCost = 120
        - SELECT * FROM Invoices WHERE TotalCost = 1200
        - SELECT * FROM Invoices WHERE TotalCost = 1300 and InvoiceDate = #4/13/2018# 
        - SELECT * FROM Invoices WHERE InvoiceDate = #4/13/2018#
        - SELECT DISTINCT(InvoiceNum) From Invoices order by InvoiceNum
        - SELECT DISTINCT(InvoiceDate) From Invoices order by InvoiceDate
        - SELECT DISTINCT(TotalCost) From Invoices order by TotalCost
        */
    }
}

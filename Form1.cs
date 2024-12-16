using Microsoft.Data.SqlClient;
using System.Data;

namespace DBMS.SQL.Order
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter da;

        private void connectDB()
        {
            string server = @" ÿ√«ÿ≤‘\SQLEXPRESS";
            string db = "Northwind";
            string strCon = string.Format(@"Data Source={0}; Initial Catalog={1};"
                      + "Integrated Security=True;Encrypt=False", server, db);
            conn = new SqlConnection(strCon);
            conn.Open();
        }
        private void disconnectDB()
        {
            conn.Close();
        }
        private void showdata(string sql, DataGridView dgv)
        {
            da = new SqlDataAdapter(sql, conn);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dgv.DataSource = ds.Tables[0];
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            connectDB();
            string sqlQuarry = " select o.OrderID,Format(o.OrderDate,'dd-MM-yy') as Order_date," +
                " FORMAT(o.ShippedDate,'dd-MM-yy'),o.ShipVia," +
                " TitleOfCourtesy+FirstName+ ' ' + LastName EmployeeName,c.CompanyName,c.Phone," +
                " round(SUM(OD.Quantity * OD.UnitPrice * (1 - OD.Discount)), 2) as ¬Õ¥‡ß‘π√«¡   " +
                " from Orders o join [Order Details] od on o.OrderID = od.OrderID " +
                " join Shippers s on o.ShipVia = s.ShipperID" +
                " join Employees e on o.EmployeeID = e.EmployeeID " +
                " join Customers c on o.CustomerID = c.CustomerID" +
                " group by  o.OrderID, o.OrderDate, o.ShippedDate,o.ShipVia," +
                " TitleOfCourtesy+FirstName+ ' ' + LastName ,c.CompanyName,c.Phone";
            showdata(sqlQuarry, dgvOrders);
        }

        private void dgvOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                {
                    int id = Convert.ToInt32(dgvOrders.CurrentRow.Cells[0].Value);
                    string sqlQuarry = "select p.ProductID, p.ProductName, od.Quantity, od.UnitPrice, od.Discount," +
                        " od.Quantity * od.UnitPrice ¬Õ¥‡ß‘π‡µÁ¡, od.Quantity * od.UnitPrice * od.Discount  Ë«π≈¥," +
                        " od.Quantity * od.UnitPrice * (1-od.Discount) ¬Õ¥‡ß‘π∑’ËÀ—° Ë«π≈¥·≈È«" +
                        " from Orders o join [Order Details] od on o.OrderID = od.OrderID " +
                        " join Products p on od.ProductID = p.ProductID\r\nwhere o.OrderID = @id";
                    cmd = new SqlCommand(sqlQuarry, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    dgvDetails.DataSource = ds.Tables[0];
                }
            }
        }
    }
}
